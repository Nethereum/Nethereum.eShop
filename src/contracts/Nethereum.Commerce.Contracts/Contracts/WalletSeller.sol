pragma solidity ^0.6.1;
//pragma experimental ABIEncoderV2;

import "./IWalletSeller.sol";
import "./Ownable.sol";
import "./Bindable.sol";

/// @title WalletSeller
contract WalletSeller is IWalletSeller, Ownable, Bindable
{
    // Contract setup
    function configure(string calldata sellerId, string calldata nameOfPurchasing, string calldata nameOfFunding) override external
    {}
    
    // Purchasing <= Seller Wallet
    function setPoItemAccepted(uint poNumber, uint8 poItemNumber, bytes32 soNumber, bytes32 soItemNumber) override external
    {}
    
    function setPoItemRejected(uint poNumber, uint8 poItemNumber) override external
    {}
    
    function setPoItemReadyForGoodsIssue(uint poNumber, uint8 poItemNumber) override external
    {}
    
    function setPoItemGoodsIssued(uint poNumber, uint8 poItemNumber) override external
    {}
    
    function setPoItemGoodsReceived(uint poNumber, uint8 poItemNumber) override external
    {}
}

