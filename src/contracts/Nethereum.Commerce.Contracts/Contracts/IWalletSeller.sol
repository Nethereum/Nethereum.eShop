pragma solidity ^0.5.3;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

interface IWalletSeller
{
    // PoMain => Seller Wallet (event pass through)
    function onCreatePurchaseOrderRequested(IPoTypes.Po calldata po) external;
    function onCancelPurchaseOrderRequested(IPoTypes.Po calldata po) external;
    
    // PoMain <= Seller Wallet
    function setSalesOrderNumberByEthPoNumber(uint64 ethPoNumber, bytes32 sellerSalesOrderNumber) external;
    function refundPoToBuyer(uint64 ethPoNumber) external;
    function releasePoFundsToSeller(uint64 ethPoNumber) external;
    function reportSalesOrderNotApproved(uint64 ethPoNumber) external;
    function reportSalesOrderCancelFailure(uint64 ethPoNumber) external;
    function reportSalesOrderInvoiceFault(uint64 ethPoNumber) external;
}