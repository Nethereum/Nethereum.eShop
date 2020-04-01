pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPurchasing.sol";
import "./IPoTypes.sol";
import "./IErc20.sol";
import "./IPoStorage.sol";
import "./IBusinessPartnerStorage.sol";
import "./IFunding.sol";
import "./IAddressRegistry.sol";
import "./Ownable.sol";
import "./Bindable.sol";
import "./StringConvertible.sol";

/// @title Purchasing
contract Purchasing is IPurchasing, Ownable, Bindable, StringConvertible
{
    bytes32 public eShopId;
    IAddressRegistry public addressRegistry;
    IPoStorage public poStorage;
    IBusinessPartnerStorage public bpStorage;
    IFunding public funding;
    
    // TODO define where these config values should be held, eternal storage?
    uint constant private FEE_BASIS_POINTS = 100;  // 100 basis points = 1%
    uint constant private ESCROW_TIMEOUT_DAYS = 30;
     
    /// @notice Specify eShopId at point of contract creation, then it is fixed forever.
    constructor (address contractAddressOfRegistry, string memory eShopIdString) public
    {
        addressRegistry = IAddressRegistry(contractAddressOfRegistry);
        eShopId = stringToBytes32(eShopIdString);
    }
    
    // Contract setup
    function configure(
        string calldata nameOfPoStorage, 
        string calldata nameOfBusinessPartnerStorage,
        string calldata nameOfFunding) onlyOwner() override external
    {
        // PO Storage contract
        poStorage = IPoStorage(addressRegistry.getAddressString(nameOfPoStorage));
        require(address(poStorage) != address(0), "Could not find Purchasing contract address in registry");
        
        // Business Partner Storage contract
        bpStorage = IBusinessPartnerStorage(addressRegistry.getAddressString(nameOfBusinessPartnerStorage));
        require(address(bpStorage) != address(0), "Could not find Business Partner Storage contract address in registry");
        
        // Funding contract
        funding = IFunding(addressRegistry.getAddressString(nameOfFunding));
        require(address(funding) != address(0), "Could not find Funding contract address in registry");
        
        // Check that the eShop master data purchasing contract points to this contract's address
        IPoTypes.Eshop memory eShop = bpStorage.getEshop(eShopId);
        require(eShop.purchasingContractAddress == address(this), "eShop master data points to wrong Purchasing address");
    }
    
    function getFunding() override external view returns (IFunding)
    {
        return funding;
    }
    
    // Purchasing
    function getPo(uint poNumber) override external view returns (IPoTypes.Po memory po)
    {
        return poStorage.getPo(poNumber);
    }
    
    function getPoByQuote(uint quoteId) override public view returns (IPoTypes.Po memory po)
    {
        uint poNumber = poStorage.getPoNumberByEshopIdAndQuote(eShopId, quoteId);
        return poStorage.getPo(poNumber);
    }
    
    function createPurchaseOrder(IPoTypes.Po memory po, bytes memory signature) onlyRegisteredCaller() override public
    {
        // Record the create request, emitting po exactly as we received it
        emit PurchaseOrderCreateRequestLog(po.buyerWalletAddress, po.sellerId, 0, po);
        
        //-------------------------------------------------------------------------
        // Po Validation (before new fields added)
        //-------------------------------------------------------------------------
        // Ensure buyer chose a valid eshop
        require(po.eShopId.length > 0, "eShopId must be specified");
        IPoTypes.Eshop memory eShop = bpStorage.getEshop(po.eShopId);
        require(eShop.purchasingContractAddress != address(0), "eShop has no purchasing address");
        require(eShop.quoteSignerCount > 0, "No quote signers found for eShop");
        require(po.eShopId == eShopId, "eShopId is not correct for this contract");  // must be "our" eShop
        require(eShop.isActive == true, "eShop is inactive");
        
        // Ensure buyer chose a valid seller
        require(po.sellerId.length > 0, "SellerId must be specified");
        IPoTypes.Seller memory seller = bpStorage.getSeller(po.sellerId);
        require(seller.sellerId.length > 0, "Seller has no master data");
        require(seller.adminContractAddress != address(0), "Seller has no admin address");
        require(seller.isActive == true, "Seller is inactive");
        
        // Validate quote and quote signer
        address expectedSignerAddress = getSignerAddressFromPoAndSignature(po, signature);
        bool isSignerFound = false;
        address matchingSignerAddress = address(0);
        for (uint i = 0; i < eShop.quoteSignerCount; i++)
        {
            if (eShop.quoteSigners[i] == expectedSignerAddress)
            {
                isSignerFound = true;
                break;
            }
        }
        require(isSignerFound == true, "Signature for quote does not match any expected signatures");
        require(po.quoteExpiryDate >= now, "Quote expiry date has passed");
        
        // Quote should not already have been used
        IPoTypes.Po memory poExisting = getPoByQuote(po.quoteId);
        require(poExisting.poNumber == 0, "Quote already in use");
        
        //-------------------------------------------------------------------------
        // Add fields that contract owns
        //-------------------------------------------------------------------------
        poStorage.incrementPoNumber();
        po.poNumber = poStorage.getCurrentPoNumber();
        po.quoteSignerAddress = matchingSignerAddress;
        po.poCreateDate = now;
        uint lenItems = po.poItems.length;
        po.poItemCount = uint8(lenItems);
        for (uint i = 0; i < po.poItemCount; i++)
        {
            po.poItems[i].poNumber = po.poNumber;
            po.poItems[i].poItemNumber = (uint8)(i + 1);
            // shop fee calculation - arbitrary 1% for now
            po.poItems[i].currencyValueFee = ( po.poItems[i].currencyValue * FEE_BASIS_POINTS ) / 10000; 
            po.poItems[i].status = IPoTypes.PoItemStatus.Created;
            po.poItems[i].goodsIssuedDate = 0;
            po.poItems[i].goodsReceivedDate = 0;
            po.poItems[i].plannedEscrowReleaseDate = 0;
            po.poItems[i].actualEscrowReleaseDate = 0;
            po.poItems[i].isEscrowReleased = false;
            po.poItems[i].cancelStatus = IPoTypes.PoItemCancelStatus.Initial;
        }
        uint lenRules = po.rules.length;
        po.rulesCount = uint8(lenRules);
        
        //-------------------------------------------------------------------------
        // Store Po details in eternal storage
        //-------------------------------------------------------------------------
        poStorage.setPo(po);
        
        //-------------------------------------------------------------------------
        // Funding. Here, the Funding contract attempts to pull in funds from buyer wallet
        //-------------------------------------------------------------------------
        funding.transferInFundsForPoFromBuyerWallet(po.poNumber);
        
        // Record the new PO as it was stored
        IPoTypes.Po memory poAsStored = poStorage.getPo(po.poNumber);
        emit PurchaseOrderCreatedLog(poAsStored.buyerWalletAddress, poAsStored.sellerId, poAsStored.poNumber, poAsStored);
    }
    
    function getFeeBasisPoints() override external pure returns (uint)
    {
        return FEE_BASIS_POINTS;
    }
    
    function getEscrowTimeoutDays() override external pure returns (uint)
    {
        return ESCROW_TIMEOUT_DAYS;
    }
    
    function cancelPurchaseOrderItem(uint poNumber, uint8 poItemNumber) onlyRegisteredCaller() override external
    {
        revert("Not implemented yet");
    }
    
    function setPoItemGoodsReceivedBuyer(uint poNumber, uint8 poItemNumber) onlyRegisteredCaller() override external // TODO only buyer and admin
    {
        // Common Validations
        IPoTypes.Po memory po = poStorage.getPo(poNumber);
        validatePoItem(po, poItemNumber, IPoTypes.PoItemStatus.GoodsIssued);
        
        // Updates
        uint poItemIndex = poItemNumber - 1;
        po.poItems[poItemIndex].status = IPoTypes.PoItemStatus.GoodsReceived;
        po.poItems[poItemIndex].goodsReceivedDate = now;
    
        // Write to storage
        poStorage.setPo(po);
        emit PurchaseItemGoodsReceivedLog(po.buyerWalletAddress, po.sellerId, po.poNumber, po.poItems[poItemIndex]);   
    }
    
    // Only from Seller Wallet
    function setPoItemAccepted(uint poNumber, uint8 poItemNumber, bytes32 soNumber, bytes32 soItemNumber) onlyRegisteredCaller() override external
    {
        // Common Validations
        IPoTypes.Po memory po = poStorage.getPo(poNumber);
        validatePoItem(po, poItemNumber, IPoTypes.PoItemStatus.Created);
        
        // Updates
        uint poItemIndex = poItemNumber - 1;
        po.poItems[poItemIndex].soNumber = soNumber;
        po.poItems[poItemIndex].soItemNumber = soItemNumber;
        po.poItems[poItemIndex].status = IPoTypes.PoItemStatus.Accepted;
    
        // Write to storage
        poStorage.setPo(po);
        emit PurchaseItemAcceptedLog(po.buyerWalletAddress, po.sellerId, po.poNumber, po.poItems[poItemIndex]);
    }
    
    function setPoItemRejected(uint poNumber, uint8 poItemNumber) onlyRegisteredCaller() override external
    {
        // Common Validations
        IPoTypes.Po memory po = poStorage.getPo(poNumber);
        validatePoItem(po, poItemNumber, IPoTypes.PoItemStatus.Created);
        
        // Escrow refund, which could revert
        funding.transferOutFundsForPoItemToBuyer(poNumber, poItemNumber);
        uint poItemIndex = poItemNumber - 1;
        emit PurchaseItemEscrowRefundedLog(po.buyerWalletAddress, po.sellerId, po.poNumber, po.poItems[poItemIndex]);
        
        // Updates
        po.poItems[poItemIndex].status = IPoTypes.PoItemStatus.Rejected;
    
        // Write to storage
        poStorage.setPo(po);
        emit PurchaseItemRejectedLog(po.buyerWalletAddress, po.sellerId, po.poNumber, po.poItems[poItemIndex]);
    }
    
    function setPoItemReadyForGoodsIssue(uint poNumber, uint8 poItemNumber) onlyRegisteredCaller() override external
    {
        // Common Validations
        IPoTypes.Po memory po = poStorage.getPo(poNumber);
        validatePoItem(po, poItemNumber, IPoTypes.PoItemStatus.Accepted);
        
        // Updates
        uint poItemIndex = poItemNumber - 1;
        po.poItems[poItemIndex].status = IPoTypes.PoItemStatus.ReadyForGoodsIssue;
    
        // Write to storage
        poStorage.setPo(po);
        emit PurchaseItemReadyForGoodsIssueLog(po.buyerWalletAddress, po.sellerId, po.poNumber, po.poItems[poItemIndex]);
    }
    
    function setPoItemGoodsIssued(uint poNumber, uint8 poItemNumber) onlyRegisteredCaller() override external
    {
        // Common Validations
        IPoTypes.Po memory po = poStorage.getPo(poNumber);
        validatePoItem(po, poItemNumber, IPoTypes.PoItemStatus.ReadyForGoodsIssue);
        
        // Updates
        uint poItemIndex = poItemNumber - 1;
        po.poItems[poItemIndex].status = IPoTypes.PoItemStatus.GoodsIssued;
        po.poItems[poItemIndex].goodsIssuedDate = now;
        po.poItems[poItemIndex].plannedEscrowReleaseDate = now + ESCROW_TIMEOUT_DAYS; // eg escrow times out after 30 days
    
        // Write to storage
        poStorage.setPo(po);
        emit PurchaseItemGoodsIssuedLog(po.buyerWalletAddress, po.sellerId, po.poNumber, po.poItems[poItemIndex]);
    }
    
    function setPoItemGoodsReceivedSeller(uint poNumber, uint8 poItemNumber) onlyRegisteredCaller()  override external // TODO only seller and admin
    {
        // Common Validations
        IPoTypes.Po memory po = poStorage.getPo(poNumber);
        validatePoItem(po, poItemNumber, IPoTypes.PoItemStatus.GoodsIssued);
        
        // Additional Validations
        uint poItemIndex = poItemNumber - 1;
        // Seller cannot say goods received unless enough days have passed
        require(po.poItems[poItemIndex].plannedEscrowReleaseDate <= now, "Seller cannot set goods received: insufficient days passed");
        
        // Updates
        po.poItems[poItemIndex].status = IPoTypes.PoItemStatus.GoodsReceived;
        po.poItems[poItemIndex].goodsReceivedDate = now;
    
        // Write to storage
        poStorage.setPo(po);
        emit PurchaseItemGoodsReceivedLog(po.buyerWalletAddress, po.sellerId, po.poNumber, po.poItems[poItemIndex]);
    }
    
    function setPoItemCompleted(uint poNumber, uint8 poItemNumber) onlyRegisteredCaller() override external
    {
        // Common Validations
        IPoTypes.Po memory po = poStorage.getPo(poNumber);
        validatePoItem(po, poItemNumber, IPoTypes.PoItemStatus.GoodsReceived);
        
        // Escrow release, which could revert
        funding.transferOutFundsForPoItemToSeller(poNumber, poItemNumber);
        uint poItemIndex = poItemNumber - 1;
        emit PurchaseItemEscrowReleasedLog(po.buyerWalletAddress, po.sellerId, po.poNumber, po.poItems[poItemIndex]);

        // Updates        
        po.poItems[poItemIndex].status = IPoTypes.PoItemStatus.Completed;
        po.poItems[poItemIndex].actualEscrowReleaseDate = now;
        po.poItems[poItemIndex].isEscrowReleased = true;
    
        // Write to storage
        poStorage.setPo(po);
        emit PurchaseItemCompletedLog(po.buyerWalletAddress, po.sellerId, po.poNumber, po.poItems[poItemIndex]);
    }
    
    function validatePoItem(IPoTypes.Po memory po, uint8 poItemNumber, IPoTypes.PoItemStatus expectedOldPoStatus) private pure
    {
        // TODO could change this to be fn modifier instead
        
        // PO header
        require(po.poNumber > 0, "PO does not exist");
        
        // PO Item
        // poItemNumber numbering starts at 1
        require(poItemNumber <= po.poItemCount, "PO item does not exist (too large)");
        require(poItemNumber >= 1, "PO item does not exist (min is 1)");
        uint poItemIndex = poItemNumber - 1;
        
        // Status
        require(po.poItems[poItemIndex].status == expectedOldPoStatus, "Existing PO item status incorrect");
    }
    
    //-------------------------------------------------------------------------
    // Signature functions
    //-------------------------------------------------------------------------
    function getSignerAddressFromPoAndSignature(IPoTypes.Po memory po, bytes memory signature) override public pure returns (address)
    {
        // Recreate the message that was signed on the client
        bytes32 messageAsClient = prefixed(keccak256(abi.encode(po)));
        
        // Recover the signer's address
        return recoverSigner(messageAsClient, signature);
    }
    
    function splitSignature(bytes memory sig) private pure returns (uint8 v, bytes32 r, bytes32 s)
    {
        require(sig.length == 65);
        assembly {
            // first 32 bytes, after the length prefix.
            r := mload(add(sig, 32))
            // second 32 bytes.
            s := mload(add(sig, 64))
            // final byte (first byte of the next 32 bytes).
            v := byte(0, mload(add(sig, 96)))
        }
        return (v, r, s);
    }

    function recoverSigner(bytes32 message, bytes memory sig) private pure returns (address)
    {
        (uint8 v, bytes32 r, bytes32 s) = splitSignature(sig);
        return ecrecover(message, v, r, s);
    }

    // builds a prefixed hash to mimic the behavior of eth_sign.
    function prefixed(bytes32 hash) private pure returns (bytes32)
    {
        return keccak256(abi.encodePacked("\x19Ethereum Signed Message:\n32", hash));
    }
}