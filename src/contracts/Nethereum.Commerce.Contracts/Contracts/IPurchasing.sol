pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

interface IPurchasing
{
    //---------------------------------------------------------
    // Events
    //---------------------------------------------------------
    // Events Header level
    event PurchaseOrderCreateRequestLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.Po po);
    event PurchaseOrderCreatedLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.Po po);
    event PurchaseOrderNotCreatedLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.Po po);
    
    // Events Item level (matching 1:1 with PoItemStatus)
    event PurchaseItemAcceptedLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemRejectedLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemReadyForGoodsIssueLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemGoodsIssuedLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemGoodsReceivedLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemCompletedLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemCancelledLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);

    // Events Item level (for escrow release)
    event PurchaseItemEscrowReleasedLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemEscrowFailedLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);

    //---------------------------------------------------------
    // Functions
    //---------------------------------------------------------
    // Contract setup
    function configure(string calldata nameOfPoStorage, string calldata nameOfBusinessPartnerStorage, string calldata nameOfFunding) external;
    
    // Purchasing
    function getPo(uint poNumber) external view returns (IPoTypes.Po memory po);
    function getPoBySellerAndQuote(string calldata sellerIdString, uint quoteId) external view returns (IPoTypes.Po memory po);
    
    // Only from Buyer Wallet
    function createPurchaseOrder(IPoTypes.Po calldata po) external;
    function cancelPurchaseOrderItem(uint poNumber, uint8 poItemNumber) external;
    function setPoItemGoodsReceivedBuyer(uint poNumber, uint8 poItemNumber) external;
    
    // Only from Seller Wallet
    function setPoItemAccepted(uint poNumber, uint8 poItemNumber, bytes32 soNumber, bytes32 soItemNumber) external;
    function setPoItemRejected(uint poNumber, uint8 poItemNumber) external;
    function setPoItemReadyForGoodsIssue(uint poNumber, uint8 poItemNumber) external;
    function setPoItemGoodsIssued(uint poNumber, uint8 poItemNumber) external;
    function setPoItemGoodsReceivedSeller(uint poNumber, uint8 poItemNumber) external;
    function setPoItemCompleted(uint poNumber, uint8 poItemNumber) external;
}