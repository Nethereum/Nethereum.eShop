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
    //IFunding public fundingContract;
   
    constructor (address contractAddressOfRegistry) public
    {
        addressRegistry = IAddressRegistry(contractAddressOfRegistry);
    }
    
    // Contract setup
    function configure(
        string calldata nameOfPoStorage, 
        string calldata nameOfBusinessPartnerStorage,
        string calldata nameOfFundingContract) override external
    {
        
    }
    
    // Purchasing
    function getPo(uint poNumber) override external view returns (IPoTypes.Po memory po)
    {
        // call po storage
    }
    
    function getPoNumberBySellerAndQuote(bytes32 sellerId, uint quoteId) override external view returns (uint poNumber)
    {
        // call po storage
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