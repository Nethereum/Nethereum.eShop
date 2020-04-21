pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IBuyerWallet.sol";
import "./IAddressRegistry.sol";
import "./IBusinessPartnerStorage.sol";
import "./IPurchasing.sol";
import "./IFunding.sol";
import "./IErc20.sol";
import "./Ownable.sol";
import "./Bindable.sol";
import "./StringConvertible.sol";

/// @title BuyerWallet
contract BuyerWallet is IBuyerWallet, Ownable, Bindable, StringConvertible
{
    // Global data
    IBusinessPartnerStorage public businessPartnerStorageGlobal;
    
    constructor (address businessPartnerStorageAddressGlobal) public
    {
        businessPartnerStorageGlobal = IBusinessPartnerStorage(businessPartnerStorageAddressGlobal);
    } 
    
    function reconfigure(address businessPartnerStorageAddressGlobal) override external onlyOwner
    {
         businessPartnerStorageGlobal = IBusinessPartnerStorage(businessPartnerStorageAddressGlobal);
    }
    
    // Purchasing
    function getPo(string calldata eShopIdString, uint poNumber) override external view returns (IPoTypes.Po memory po)
    {
        // Get and validate eShop
        bytes32 eShopId = stringToBytes32(eShopIdString);
        IPoTypes.Eshop memory eShop = getAndValidateEshop(eShopId);
        IPurchasing purchasing = IPurchasing(eShop.purchasingContractAddress);
        return purchasing.getPo(poNumber);
    }
    
    function getPoByEshopIdAndQuote(string calldata eShopIdString, uint quoteId) override external view returns (IPoTypes.Po memory po)
    {
        // Get and validate eShop
        bytes32 eShopId = stringToBytes32(eShopIdString);
        IPoTypes.Eshop memory eShop = getAndValidateEshop(eShopId);
        IPurchasing purchasing = IPurchasing(eShop.purchasingContractAddress);
        return purchasing.getPoByQuote(quoteId);
    }
    
    function createPurchaseOrder(IPoTypes.Po calldata po, bytes calldata signature)
        override external onlyRegisteredCaller
    {
        // Get correct contracts for Purchasing and Funding
        IPoTypes.Eshop memory eShop = getAndValidateEshop(po.eShopId);
        IPurchasing purchasing = IPurchasing(eShop.purchasingContractAddress);
        IFunding funding = purchasing.getFunding();
        
        // Calculate total value
        uint     totalValue = 0;
        uint itemCount = po.poItems.length;
        for (uint i = 0; i< itemCount; i++)
        {
            totalValue += po.poItems[i].currencyValue;
        }
        
        // Allow Funding contract to withdraw funds
        IErc20 tokenContract = IErc20(po.currencyAddress);
        // NB: erc20.approve() is approving from THIS Wallet contract (not msg.sender) into the Funding contract
        // Depending on token implementation, this might return false if approval failed
        bool result = tokenContract.approve(address(funding), totalValue);
        require(result == true, "Token value could not be approved for spend.");
        
        // Purchasing contract does the creation
        purchasing.createPurchaseOrder(po, signature);
        
        // Creation was successful
        emit QuoteConvertedToPoLog(po.eShopId, po.quoteId, po.sellerId);
    }
    
    function cancelPurchaseOrderItem(string calldata eShopIdString, uint poNumber, uint8 poItemNumber)
        override external onlyRegisteredCaller
    {
        // Get correct contract for Purchasing
        bytes32 eShopId = stringToBytes32(eShopIdString);
        IPoTypes.Eshop memory eShop = getAndValidateEshop(eShopId);
        IPurchasing purchasing = IPurchasing(eShop.purchasingContractAddress);
        
        // Only the PO owner (BuyerUserAddress) can request PO item cancellation
        IPoTypes.Po memory po = purchasing.getPo(poNumber);
        require(msg.sender == po.buyerUserAddress, "Only PO owner (BuyerUserAddress) can request item cancellation");
        
        revert("Not implemented yet");
    }
    
    function setPoItemGoodsReceived(string calldata eShopIdString, uint poNumber, uint8 poItemNumber)
        override external onlyRegisteredCaller
    {
        // Get correct contract for Purchasing
        bytes32 eShopId = stringToBytes32(eShopIdString);
        IPoTypes.Eshop memory eShop = getAndValidateEshop(eShopId);
        IPurchasing purchasing = IPurchasing(eShop.purchasingContractAddress);
        
        // Only the PO owner (BuyerUserAddress) can mark a PO as goods received. If they don't, eventually PO will time out 
        // and the Seller Admin will be able to mark PO as goods received instead.
        IPoTypes.Po memory po = purchasing.getPo(poNumber);
        require(msg.sender == po.buyerUserAddress, "Only PO owner (BuyerUserAddress) can say Goods Received");
        purchasing.setPoItemGoodsReceivedBuyer(poNumber, poItemNumber);
    }
    
    function getAndValidateEshop(bytes32 eShopId) private view returns (IPoTypes.Eshop memory validShop)
    {
        IPoTypes.Eshop memory eShop = businessPartnerStorageGlobal.getEshop(eShopId);
        require(eShop.purchasingContractAddress != address(0), "eShop has no purchasing address");
        require(eShop.quoteSignerCount > 0, "No quote signers found for eShop");
        return eShop;
    }
}