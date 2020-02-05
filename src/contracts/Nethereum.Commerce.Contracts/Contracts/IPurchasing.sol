pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

interface IPurchasing
{
    //---------------------------------------------------------
    // Events
    //---------------------------------------------------------
    // Events Header level
    event PurchaseOrderCreateRequestLog(bytes32 indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.Po po);
    event PurchaseOrderCreatedLog(bytes32 indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.Po po);
    event PurchaseOrderNotCreatedLog(bytes32 indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.Po po);
    
    // Events Item level (matching 1:1 with PoItemStatus)
    event PurchaseItemCreatedLog(bytes32 indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemAcceptedLog(bytes32 indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemRejectedLog(bytes32 indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemReadyForGoodsIssueLog(bytes32 indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemGoodsIssuedLog(bytes32 indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemReceivedLog(bytes32 indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemCompletedLog(bytes32 indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemCancelledLog(bytes32 indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);

    // Events Item level (for escrow release)
    event PurchaseItemEscrowReleasedLog(bytes32 indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemEscrowFailedLog(bytes32 indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);

    //---------------------------------------------------------
    // Functions
    //---------------------------------------------------------
    // Contract setup
    function configure(string calldata nameOfPoStorage, string calldata nameOfBusinessPartnerStorage, string calldata nameOfFundingContract) external;
    
    // Purchasing
    function getPo(uint poNumber) external view returns (IPoTypes.Po memory po);
    function getPoNumberBySellerAndQuote(bytes32 sellerId, uint quoteId) external view returns (uint poNumber);
    
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
}