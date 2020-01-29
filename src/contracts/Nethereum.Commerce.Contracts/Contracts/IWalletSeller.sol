pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

interface IWalletSeller
{
    // ##Revised
    event PurchaseCreatedOkLog(bytes32 indexed buyerAddress, bytes32 indexed sellerSysId, uint indexed poNumber, IPoTypes.Po po);
    event PurchaseCreateRequestFailedLog(bytes32 indexed buyerAddress, bytes32 indexed sellerSysId, uint indexed poNumber, IPoTypes.Po po);
    
    event PurchaseItemApprovedOkLog(bytes32 indexed buyerAddress, bytes32 indexed sellerSysId, uint indexed poNumber, IPoTypes.Po po);
    event PurchaseItemRejectedLog(bytes32 indexed buyerAddress, bytes32 indexed sellerSysId, uint indexed poNumber, IPoTypes.Po po);
  
    // PoMain <= Seller Wallet
    function setPoItemAddSalesOrderItem(uint poNumber, uint8 poItemNumber, bytes32 soNumber, bytes32 soItemNumber) external;
    function setPoItemRejected(uint poNumber, uint8 poItemNumber) external;
    
    
    
    
    // ##For reference
    // PoMain => Seller Wallet (event pass through)
    function onCreatePurchaseOrderRequested(IPoTypes.Po calldata po) external;
    function onCancelPurchaseOrderItemRequested(uint poNumber, uint8 poItemNumber) external;
    
    function setPoItemGoodsIssued(uint poNumber, uint8 poItemNumber) external;
    function requestPoItemEscrowRelease(uint poNumber, uint8 poItemNumber) external;
    
}