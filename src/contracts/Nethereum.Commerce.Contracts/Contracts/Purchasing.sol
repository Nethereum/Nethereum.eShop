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
    
    // Only from Buyer Wallet
    function createPurchaseOrder(IPoTypes.Po calldata po) override external
    {}
    
    function cancelPurchaseOrderItem(uint poNumber, uint8 poItemNumber) override external
    {}
    
    function setPoItemGoodsReceivedBuyer(uint poNumber, uint8 poItemNumber) override external
    {}
    
    // Only from Seller Wallet
    function setPoItemAccepted(uint poNumber, uint8 poItemNumber, bytes32 soNumber, bytes32 soItemNumber) override external
    {}
    
    function setPoItemRejected(uint poNumber, uint8 poItemNumber) override external
    {}
    
    function setPoItemReadyForGoodsIssue(uint poNumber, uint8 poItemNumber) override external
    {}
    
    function setPoItemGoodsIssued(uint poNumber, uint8 poItemNumber) override external
    {}
    
    function setPoItemGoodsReceivedSeller(uint poNumber, uint8 poItemNumber) override external
    {}
    
}