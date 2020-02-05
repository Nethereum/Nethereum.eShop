pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IWalletBuyer.sol";
import "./IAddressRegistry.sol";
import "./Ownable.sol";
import "./Bindable.sol";

/// @title WalletBuyer
contract WalletBuyer is IWalletBuyer, Ownable, Bindable
{
    IAddressRegistry public addressRegistry;
    //IPurchasing public purchasing;
    //IFunding public fundingContract;

    constructor (address contractAddressOfRegistry) public
    {
        addressRegistry = IAddressRegistry(contractAddressOfRegistry);
    }
    
    // Contract setup
    function configure(string calldata nameOfPurchasing, string calldata nameOfFunding) override external
    {
        
    }
    
    // Purchasing
    function getPo(uint poNumber) override external view returns (IPoTypes.Po memory po)
    {
        
    }
    
    function getPoNumberBySellerAndQuote(string calldata sellerIdString, uint quoteId) override external view returns (uint poNumber)
    {}
    
    function createPurchaseOrder(IPoTypes.Po calldata po) override external
    {}
    
    function cancelPurchaseOrderItem(uint poNumber, uint8 poItemNumber) override external
    {}
    
    function setPoItemGoodsReceived(uint poNumber, uint8 poItemNumber) override external
    {}

}

