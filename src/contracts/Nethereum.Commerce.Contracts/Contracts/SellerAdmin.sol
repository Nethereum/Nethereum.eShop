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
    bool public isConfigured;
    
    constructor (string memory sellerIdString) public
    {
        sellerId = stringToBytes32(sellerIdString);
        isConfigured = false;
    }
    
    modifier onlyConfigured
    {
        require(isConfigured == true, "Contract needs configured first");
        _;
    }
    
    function configure(address businessPartnerStorageAddressGlobal) override external onlyOwner
    {
        businessPartnerStorageGlobal = IBusinessPartnerStorage(businessPartnerStorageAddressGlobal);
         
        // Check master data is correct
        
        isConfigured = true;
    }
    
    // Purchasing
    /// @notice This function can only be called by the eShop (Purchasing.sol) for the given PO, which means
    /// @notice the SellerAdmin owner must have bound (whitelisted) the Purchasing.sol contract when they
    /// @notice registered with the eShop.
    function emitEventForNewPo(IPoTypes.Po calldata po) override external onlyConfigured onlyRegisteredCaller
    {
        emit QuoteConvertedToPoLog(po.eShopId, po.quoteId, po.buyerWalletAddress);
    }
    
    function getPo(string calldata eShopIdString, uint poNumber)
        override external view onlyConfigured returns (IPoTypes.Po memory po)
    {
        // Get and validate eShop
        bytes32 eShopId = stringToBytes32(eShopIdString);
        IPoTypes.Eshop memory eShop = getAndValidateEshop(eShopId);
        IPurchasing purchasing = IPurchasing(eShop.purchasingContractAddress);
        return purchasing.getPo(poNumber);
    }
    
    function getPoByEshopIdAndQuote(string calldata eShopIdString, uint quoteId) 
        override external view onlyConfigured returns (IPoTypes.Po memory po)
    {
        // Get and validate eShop
        bytes32 eShopId = stringToBytes32(eShopIdString);
        IPoTypes.Eshop memory eShop = getAndValidateEshop(eShopId);
        IPurchasing purchasing = IPurchasing(eShop.purchasingContractAddress);
        
        return purchasing.getPoByQuote(quoteId);
    }
    
    function setPoItemAccepted(string calldata eShopIdString, uint poNumber, uint8 poItemNumber, bytes32 soNumber, bytes32 soItemNumber)
        override external onlyConfigured onlyRegisteredCaller
    {   
        // Get and validate eShop
        bytes32 eShopId = stringToBytes32(eShopIdString);
        IPoTypes.Eshop memory eShop = getAndValidateEshop(eShopId);
        IPurchasing purchasing = IPurchasing(eShop.purchasingContractAddress);
        
        purchasing.setPoItemAccepted(poNumber, poItemNumber, soNumber, soItemNumber);
    }
    
    function setPoItemRejected(string calldata eShopIdString, uint poNumber, uint8 poItemNumber) 
        override external onlyConfigured onlyRegisteredCaller
    {
        // Get and validate eShop
        bytes32 eShopId = stringToBytes32(eShopIdString);
        IPoTypes.Eshop memory eShop = getAndValidateEshop(eShopId);
        IPurchasing purchasing = IPurchasing(eShop.purchasingContractAddress);
        
        purchasing.setPoItemRejected(poNumber, poItemNumber);
    }
    
    function setPoItemReadyForGoodsIssue(string calldata eShopIdString, uint poNumber, uint8 poItemNumber)
        override external onlyConfigured onlyRegisteredCaller
    {
        // Get and validate eShop
        bytes32 eShopId = stringToBytes32(eShopIdString);
        IPoTypes.Eshop memory eShop = getAndValidateEshop(eShopId);
        IPurchasing purchasing = IPurchasing(eShop.purchasingContractAddress);
        
        purchasing.setPoItemReadyForGoodsIssue(poNumber, poItemNumber);
    }
    
    function setPoItemGoodsIssued(string calldata eShopIdString, uint poNumber, uint8 poItemNumber)
        override external onlyConfigured onlyRegisteredCaller
    {
        // Get and validate eShop
        bytes32 eShopId = stringToBytes32(eShopIdString);
        IPoTypes.Eshop memory eShop = getAndValidateEshop(eShopId);
        IPurchasing purchasing = IPurchasing(eShop.purchasingContractAddress);
        
        purchasing.setPoItemGoodsIssued(poNumber, poItemNumber);
    }
    
    function setPoItemGoodsReceived(string calldata eShopIdString, uint poNumber, uint8 poItemNumber)
        override external onlyConfigured onlyRegisteredCaller
    {
        // Get and validate eShop
        bytes32 eShopId = stringToBytes32(eShopIdString);
        IPoTypes.Eshop memory eShop = getAndValidateEshop(eShopId);
        IPurchasing purchasing = IPurchasing(eShop.purchasingContractAddress);
        
        purchasing.setPoItemGoodsReceivedSeller(poNumber, poItemNumber);
    }
    
    function setPoItemCompleted(string calldata eShopIdString, uint poNumber, uint8 poItemNumber)
        override external onlyConfigured onlyRegisteredCaller
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

