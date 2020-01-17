pragma solidity ^0.5.3;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

interface IWalletBuyer
{
    // Buyer Wallet => PoMain
    function createPurchaseOrder(IPoTypes.Po calldata po) external;
    function cancelPurchaseOrder(uint64 ethPoNumber) external;
    
    // Buyer Wallet <= PoMain (event pass through)
    function onPurchaseUpdatedWithSalesOrder(IPoTypes.Po calldata po) external;
    function onPurchasePaymentMadeOk(IPoTypes.Po calldata po) external;
    function onPurchasePaymentFailed(IPoTypes.Po calldata po) external;
    function onPurchaseRefundMadeOk(IPoTypes.Po calldata po) external;
    function onPurchaseRefundFailed(IPoTypes.Po calldata po) external;
    function onSalesOrderNotApproved(IPoTypes.Po calldata po) external;
    function onSalesOrderCancelFailure(IPoTypes.Po calldata po) external;
    function onSalesOrderInvoiceFault(IPoTypes.Po calldata po) external;
}