pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

interface IWalletBuyer
{
    // ##Revised
    event PurchaseCreateRequestReceivedLog(bytes32 indexed buyerAddress, bytes32 indexed sellerSysId, uint indexed poNumber, IPoTypes.Po po);
    event PurchaseCreatedOkLog(bytes32 indexed buyerAddress, bytes32 indexed sellerSysId, uint indexed poNumber, IPoTypes.Po po);
    event PurchaseCreateRequestFailedLog(bytes32 indexed buyerAddress, bytes32 indexed sellerSysId, uint indexed poNumber, IPoTypes.Po po);
    
    // Buyer Wallet <= PoMain (event pass through)
    event PurchaseItemApprovedOkLog(bytes32 indexed buyerAddress, bytes32 indexed sellerSysId, uint indexed poNumber, IPoTypes.Po po);
    event PurchaseItemRejectedLog(bytes32 indexed buyerAddress, bytes32 indexed sellerSysId, uint indexed poNumber, IPoTypes.Po po);
  
    
    
    
    // ##For reference
    event PurchasePaymentMadeOkLog(bytes32 indexed buyerAddress, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event PurchasePaymentFailedLog(bytes32 indexed buyerAddress, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event PurchaseRefundMadeOkLog(bytes32 indexed buyerAddress, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event PurchaseRefundFailedLog(bytes32 indexed buyerAddress, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    
    // Buyer Wallet => PoMain
    function requestPoCreate(IPoTypes.Po calldata po) external;
    function requestPoItemCancel(uint poNumber, uint8 poItemNumber) external;
    
    // Buyer Wallet <= PoMain (event pass through)
    function onPurchaseUpdatedWithSalesOrder(IPoTypes.Po calldata po) external;
    function onPurchasePaymentMadeOk(IPoTypes.Po calldata po) external;
    function onPurchasePaymentFailed(IPoTypes.Po calldata po) external;
    function onPurchaseRefundMadeOk(IPoTypes.Po calldata po) external;
    function onPurchaseRefundFailed(IPoTypes.Po calldata po) external;
}