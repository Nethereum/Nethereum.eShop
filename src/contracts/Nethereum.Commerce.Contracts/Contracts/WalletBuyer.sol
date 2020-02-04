pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IWalletBuyer.sol";
import "./Ownable.sol";
import "./Bindable.sol";

/// @title WalletBuyer
contract WalletBuyer is IWalletBuyer, Ownable, Bindable
{
    // Contract setup
    function configure(string calldata nameOfPurchasing, string calldata nameOfFunding) override external
    {}
    
    // Buyer Wallet => Purchasing
    function createPurchaseOrder(IPoTypes.Po calldata po) override external
    {}
    
    function cancelPurchaseOrderItem(uint poNumber, uint8 poItemNumber) override external
    {}
    
    function setPoItemGoodsReceived(uint poNumber, uint8 poItemNumber) override external
    {}

}

