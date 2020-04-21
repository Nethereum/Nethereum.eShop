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
    function configure(address businessPartnerStorageAddressGlobal) external;
    
    // Purchasing
    // Functions callable only by the PO's Purchasing.sol/eShop contract
    function emitEventForNewPo(IPoTypes.Po calldata po) external;
    
    // Functions callable by anyone and anything
    function getPo(string calldata eShopIdString, uint poNumber) external view returns (IPoTypes.Po memory po);
    function getPoByEshopIdAndQuote(string calldata sellerIdString, uint quoteId) external view returns (IPoTypes.Po memory po);
    
    // functions callable only by SellerAdmin.sol owner and any other whitelisted callers
    function setPoItemAccepted(string calldata eShopIdString, uint poNumber, uint8 poItemNumber, bytes32 soNumber, bytes32 soItemNumber) external;
    function setPoItemRejected(string calldata eShopIdString, uint poNumber, uint8 poItemNumber) external;
    function setPoItemReadyForGoodsIssue(string calldata eShopIdString, uint poNumber, uint8 poItemNumber) external;
    function setPoItemGoodsIssued(string calldata eShopIdString, uint poNumber, uint8 poItemNumber) external;
    function setPoItemGoodsReceived(string calldata eShopIdString, uint poNumber, uint8 poItemNumber) external;
    function setPoItemCompleted(string calldata eShopIdString, uint poNumber, uint8 poItemNumber) external;
}