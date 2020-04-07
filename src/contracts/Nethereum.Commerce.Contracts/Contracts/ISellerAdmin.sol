pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

interface ISellerAdmin
{
    // Events
    // Emitted when quote was successfully converted to PO. Always call fn getPoByEshopIdAndQuote afterwards to 
    // get latest view of PO and ensure it is genuine.
    event QuoteConvertedToPoLog(bytes32 indexed eShopId, uint indexed quoteId, address indexed buyerWalletAddress);
   
    // Contract setup
    function reconfigure(address businessPartnerStorageAddressGlobal) external;
    
    // Purchasing
    function emitEventForNewPo(IPoTypes.Po calldata po) external;
    function getPo(string calldata eShopIdString, uint poNumber) external view returns (IPoTypes.Po memory po);
    function getPoByEshopIdAndQuote(string calldata sellerIdString, uint quoteId) external view returns (IPoTypes.Po memory po);
    function setPoItemAccepted(string calldata eShopIdString, uint poNumber, uint8 poItemNumber, bytes32 soNumber, bytes32 soItemNumber) external;
    function setPoItemRejected(string calldata eShopIdString, uint poNumber, uint8 poItemNumber) external;
    function setPoItemReadyForGoodsIssue(string calldata eShopIdString, uint poNumber, uint8 poItemNumber) external;
    function setPoItemGoodsIssued(string calldata eShopIdString, uint poNumber, uint8 poItemNumber) external;
    function setPoItemGoodsReceived(string calldata eShopIdString, uint poNumber, uint8 poItemNumber) external;
    function setPoItemCompleted(string calldata eShopIdString, uint poNumber, uint8 poItemNumber) external;
}