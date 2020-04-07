pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./ISellerAdmin.sol";
import "./IAddressRegistry.sol";
import "./IBusinessPartnerStorage.sol";
import "./IPurchasing.sol";
import "./IFunding.sol";
import "./Ownable.sol";
import "./Bindable.sol";
import "./StringConvertible.sol";

/// @title SellerAdmin
contract SellerAdmin is ISellerAdmin, Ownable, Bindable, StringConvertible
{
    // Global data (shared by all eShops)
    IBusinessPartnerStorage public businessPartnerStorageGlobal;
    
    // Local data (for this Seller)
    bytes32 public sellerId;

    constructor (address businessPartnerStorageAddressGlobal, string memory sellerIdString) public
    {
        businessPartnerStorageGlobal = IBusinessPartnerStorage(businessPartnerStorageAddressGlobal);
        sellerId = stringToBytes32(sellerIdString);
    }
    
    function reconfigure(address businessPartnerStorageAddressGlobal) onlyOwner() override external
    {
         businessPartnerStorageGlobal = IBusinessPartnerStorage(businessPartnerStorageAddressGlobal);
    }
    
    // Purchasing
    /// @notice This function can only be called by the eShop (Purchasing.sol) for the given PO 
    function emitEventForNewPo(IPoTypes.Po calldata po) override external
    {
        // Don't trust the PO being passed, use the eShopId to lookup the Purchasing.sol address in the global store
        IPoTypes.Eshop memory eShop = getAndValidateEshop(po.eShopId);
        require(eShop.purchasingContractAddress == msg.sender, "Function can only be called by eShop");
        emit QuoteConvertedToPoLog(po.eShopId, po.quoteId, po.buyerWalletAddress);
    }
    
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
    
    function setPoItemAccepted(string calldata eShopIdString, uint poNumber, uint8 poItemNumber, bytes32 soNumber, bytes32 soItemNumber) onlyOwner() override external
    {   
        // Get and validate eShop
        bytes32 eShopId = stringToBytes32(eShopIdString);
        IPoTypes.Eshop memory eShop = getAndValidateEshop(eShopId);
        IPurchasing purchasing = IPurchasing(eShop.purchasingContractAddress);
        
        purchasing.setPoItemAccepted(poNumber, poItemNumber, soNumber, soItemNumber);
    }
    
    function setPoItemRejected(string calldata eShopIdString, uint poNumber, uint8 poItemNumber) onlyOwner() override external
    {
        // Get and validate eShop
        bytes32 eShopId = stringToBytes32(eShopIdString);
        IPoTypes.Eshop memory eShop = getAndValidateEshop(eShopId);
        IPurchasing purchasing = IPurchasing(eShop.purchasingContractAddress);
        
        purchasing.setPoItemRejected(poNumber, poItemNumber);
    }
    
    function setPoItemReadyForGoodsIssue(string calldata eShopIdString, uint poNumber, uint8 poItemNumber) onlyOwner() override external
    {
        // Get and validate eShop
        bytes32 eShopId = stringToBytes32(eShopIdString);
        IPoTypes.Eshop memory eShop = getAndValidateEshop(eShopId);
        IPurchasing purchasing = IPurchasing(eShop.purchasingContractAddress);
        
        purchasing.setPoItemReadyForGoodsIssue(poNumber, poItemNumber);
    }
    
    function setPoItemGoodsIssued(string calldata eShopIdString, uint poNumber, uint8 poItemNumber) onlyOwner() override external
    {
        // Get and validate eShop
        bytes32 eShopId = stringToBytes32(eShopIdString);
        IPoTypes.Eshop memory eShop = getAndValidateEshop(eShopId);
        IPurchasing purchasing = IPurchasing(eShop.purchasingContractAddress);
        
        purchasing.setPoItemGoodsIssued(poNumber, poItemNumber);
    }
    
    function setPoItemGoodsReceived(string calldata eShopIdString, uint poNumber, uint8 poItemNumber) onlyOwner() override external
    {
        // Get and validate eShop
        bytes32 eShopId = stringToBytes32(eShopIdString);
        IPoTypes.Eshop memory eShop = getAndValidateEshop(eShopId);
        IPurchasing purchasing = IPurchasing(eShop.purchasingContractAddress);
        
        purchasing.setPoItemGoodsReceivedSeller(poNumber, poItemNumber);
    }
    
    function setPoItemCompleted(string calldata eShopIdString, uint poNumber, uint8 poItemNumber) onlyOwner() override external
    {
        // Get and validate eShop
        bytes32 eShopId = stringToBytes32(eShopIdString);
        IPoTypes.Eshop memory eShop = getAndValidateEshop(eShopId);
        IPurchasing purchasing = IPurchasing(eShop.purchasingContractAddress);
        
        purchasing.setPoItemCompleted(poNumber, poItemNumber);
    }
    
    function getAndValidateEshop(bytes32 eShopId) private view returns (IPoTypes.Eshop memory validShop)
    {
        IPoTypes.Eshop memory eShop = businessPartnerStorageGlobal.getEshop(eShopId);
        require(eShop.purchasingContractAddress != address(0), "eShop has no purchasing address");
        require(eShop.quoteSignerCount > 0, "No quote signers found for eShop");
        return eShop;
    }
}

