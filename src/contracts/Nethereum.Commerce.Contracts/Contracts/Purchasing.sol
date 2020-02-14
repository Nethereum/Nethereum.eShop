pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPurchasing.sol";
import "./IPoTypes.sol";
import "./IErc20.sol";
import "./IPoStorage.sol";
import "./IBusinessPartnerStorage.sol";
import "./IFunding.sol";
import "./IWalletBuyer.sol";
import "./IWalletSeller.sol";
import "./IAddressRegistry.sol";
import "./Ownable.sol";
import "./Bindable.sol";
import "./StringConvertible.sol";

/// @title Purchasing
contract Purchasing is IPurchasing, Ownable, Bindable, StringConvertible
{
    IAddressRegistry public addressRegistry;
    IPoStorage public poStorage;
    IBusinessPartnerStorage public bpStorage;
    IFunding public funding;
   
    constructor (address contractAddressOfRegistry) public
    {
        addressRegistry = IAddressRegistry(contractAddressOfRegistry);
    }
    
    // Contract setup
    function configure(
        string calldata nameOfPoStorage, 
        string calldata nameOfBusinessPartnerStorage,
        string calldata nameOfFunding) override external
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
    }
    
    // Purchasing
    function getPo(uint poNumber) override external view returns (IPoTypes.Po memory po)
    {
        return poStorage.getPo(poNumber);
    }
    
    function getPoBySellerAndQuote(string calldata sellerIdString, uint quoteId) override external view returns (IPoTypes.Po memory po)
    {
        bytes32 sellerId = stringToBytes32(sellerIdString);
        uint poNumber = poStorage.getPoNumberBySellerAndQuote(sellerId, quoteId);
        return poStorage.getPo(poNumber);
    }
    
    function createPurchaseOrder(IPoTypes.Po memory po) override public
    {
        // Record the create request, emitting po exactly as we received it
        emit PurchaseOrderCreateRequestLog(po.buyerAddress, po.sellerId, 0, po);
        
        //-------------------------------------------------------------------------
        // Po Validation (before new fields added)
        //-------------------------------------------------------------------------
        // Ensure buyer chose a seller
        require(po.sellerId.length > 0, "Seller Id must be specified");
        // Ensure seller has a master data entry with approver address
        IPoTypes.Seller memory seller = bpStorage.getSeller(po.sellerId);
        require(seller.approverAddress != address(0), "Seller Id has no approver address");
        // TODO validate quote and quote signer here
        
        //-------------------------------------------------------------------------
        // Add fields that contract owns
        //-------------------------------------------------------------------------
        // Po header
        poStorage.incrementPoNumber();
        po.poNumber = poStorage.getCurrentPoNumber();
        po.approverAddress = seller.approverAddress;
        po.poCreateDate = now;
        uint len = po.poItems.length;
        po.poItemCount = uint8(len);
        for (uint i = 0; i < po.poItemCount; i++)
        {
            po.poItems[i].poNumber = po.poNumber;
            po.poItems[i].poItemNumber = (uint8)(i + 1);
            po.poItems[i].status = IPoTypes.PoItemStatus.Created;
            po.poItems[i].goodsIssuedDate = 0;
            po.poItems[i].goodsReceivedDate = 0;
            po.poItems[i].plannedEscrowReleaseDate = 0;
            po.poItems[i].actualEscrowReleaseDate = 0;
            po.poItems[i].isEscrowReleased = false;
            po.poItems[i].cancelStatus = IPoTypes.PoItemCancelStatus.Initial;
        }
        
        //-------------------------------------------------------------------------
        // Store Po details in eternal storage
        //-------------------------------------------------------------------------
        poStorage.setPo(po);
        
        //-------------------------------------------------------------------------
        // Funding. Here, the Funding contract attempts to pull in funds from buyer wallet
        //-------------------------------------------------------------------------
        // TODO
        //fundingContract.transferInFundsForPoFromBuyer(po.ethPurchaseOrderNumber);
        //bool isFunded = fundingContract.getPoFundingStatus(po.ethPurchaseOrderNumber);
        //require(isFunded == true, "Insufficient funding for PO");
        //if (!isFunded)
        //{ could emit create failed?
        //} else

        // Record the new PO as it was stored
        IPoTypes.Po memory poAsStored = poStorage.getPo(po.poNumber);
        emit PurchaseOrderCreatedLog(poAsStored.buyerAddress, poAsStored.sellerId, poAsStored.poNumber, poAsStored);
    }
    
    function cancelPurchaseOrderItem(uint poNumber, uint8 poItemNumber) override external
    {}
    
    function setPoItemGoodsReceivedBuyer(uint poNumber, uint8 poItemNumber) override external
    {}
    
    // Only from Seller Wallet
    function setPoItemAccepted(uint poNumber, uint8 poItemNumber, bytes32 soNumber, bytes32 soItemNumber) override external
    {
        // Validations
        IPoTypes.Po memory po = poStorage.getPo(poNumber);
        validatePoItem(po, poItemNumber, IPoTypes.PoItemStatus.Created);
        
        // Update sales order, item status
        uint poItemIndex = poItemNumber - 1;
        po.poItems[poItemIndex].soNumber = soNumber;
        po.poItems[poItemIndex].soItemNumber = soItemNumber;
        po.poItems[poItemIndex].status = IPoTypes.PoItemStatus.Accepted;
    
        // Write to storage
        poStorage.setPo(po);
        emit PurchaseItemAcceptedLog(po.buyerAddress, po.sellerId, po.poNumber, po.poItems[poItemIndex]);
    }
    
    function setPoItemRejected(uint poNumber, uint8 poItemNumber) override external
    {}
    
    function setPoItemReadyForGoodsIssue(uint poNumber, uint8 poItemNumber) override external
    {
        // Validations
        IPoTypes.Po memory po = poStorage.getPo(poNumber);
        validatePoItem(po, poItemNumber, IPoTypes.PoItemStatus.Accepted);
        
        // Update item status
        uint poItemIndex = poItemNumber - 1;
        po.poItems[poItemIndex].status = IPoTypes.PoItemStatus.ReadyForGoodsIssue;
    
        // Write to storage
        poStorage.setPo(po);
        emit PurchaseItemReadyForGoodsIssueLog(po.buyerAddress, po.sellerId, po.poNumber, po.poItems[poItemIndex]);
    }
    
    function setPoItemGoodsIssued(uint poNumber, uint8 poItemNumber) override external
    {
        // Validations
        IPoTypes.Po memory po = poStorage.getPo(poNumber);
        validatePoItem(po, poItemNumber, IPoTypes.PoItemStatus.ReadyForGoodsIssue);
        
        // Update item status
        uint poItemIndex = poItemNumber - 1;
        po.poItems[poItemIndex].status = IPoTypes.PoItemStatus.GoodsIssued;
    
        // Write to storage
        poStorage.setPo(po);
        emit PurchaseItemGoodsIssuedLog(po.buyerAddress, po.sellerId, po.poNumber, po.poItems[poItemIndex]);
    }
    
    function setPoItemGoodsReceivedSeller(uint poNumber, uint8 poItemNumber) override external
    {}
    
    function validatePoItem(IPoTypes.Po memory po, uint8 poItemNumber, IPoTypes.PoItemStatus expectedPoStatus) private pure
    {
        // PO header
        require(po.poNumber > 0, "PO does not exist");
        
        // PO Item
        // poItemNumber numbering starts at 1
        require(poItemNumber <= po.poItemCount, "PO item does not exist (too large)");
        require(poItemNumber >= 1, "PO item does not exist (min is 1)");
        uint poItemIndex = poItemNumber - 1;
        
        // Status
        require(po.poItems[poItemIndex].status == expectedPoStatus, "Existing PO item status incorrect");
    }
    
    
}