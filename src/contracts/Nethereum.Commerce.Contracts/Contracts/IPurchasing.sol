pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";
import "./IFunding.sol";

interface IPurchasing
{
    //---------------------------------------------------------
    // Events
    //---------------------------------------------------------
    // Events Header level
    event PurchaseOrderCreateRequestLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.Po po);
    event PurchaseOrderCreatedLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.Po po);
        
    // Events Item level (matching 1:1 with PoItemStatus)
    event PurchaseItemAcceptedLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemRejectedLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemReadyForGoodsIssueLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemGoodsIssuedLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemGoodsReceivedLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemCompletedLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemCancelledLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);

    // Events Item level (for escrow refund or release)
    event PurchaseItemEscrowRefundedLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    event PurchaseItemEscrowReleasedLog(address indexed buyerAddress, bytes32 indexed sellerId, uint indexed poNumber, IPoTypes.PoItem poItem);
    
    //---------------------------------------------------------
    // Functions
    //---------------------------------------------------------
    // Contract setup
    function configure(address businessPartnerStorageAddressGlobal, string calldata nameOfPoStorageLocal, string calldata nameOfFundingLocal) external;
    function getFunding() external view returns (IFunding);
    
    // Purchasing
    function getPo(uint poNumber) external view returns (IPoTypes.Po memory po);
    function getPoByQuote(uint quoteId) external view returns (IPoTypes.Po memory po);
    function getSignerAddressFromPoAndSignature(IPoTypes.Po calldata po, bytes calldata signature) external pure returns (address);
    function getFeeBasisPoints() external pure returns (uint);
    function getEscrowTimeoutDays() external pure returns (uint);
    
    // Only from Buyer Wallet
    function createPurchaseOrder(IPoTypes.Po calldata po, bytes calldata signature) external;
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