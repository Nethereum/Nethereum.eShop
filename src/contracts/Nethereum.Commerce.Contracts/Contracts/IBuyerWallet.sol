pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

interface IBuyerWallet
{
    // Events
    // Emitted when quote was successfully converted to PO. (Then use fn getPoByEshopIdAndQuote to get latest view of PO)
    event QuoteConvertedToPoLog(bytes32 indexed eShopId, uint indexed quoteId, bytes32 indexed sellerId);
    
    // Contract setup
    function reconfigure(address businessPartnerStorageAddressGlobal) external;
    
    // Purchasing
    function getPo(string calldata eShopIdString, uint poNumber) external view returns (IPoTypes.Po memory po);
    function getPoByEshopIdAndQuote(string calldata eShopIdString, uint quoteId) external view returns (IPoTypes.Po memory po);
    function createPurchaseOrder(IPoTypes.Po calldata po, bytes calldata signature) external;
    function cancelPurchaseOrderItem(string calldata eShopIdString, uint poNumber, uint8 poItemNumber) external;
    function setPoItemGoodsReceived(string calldata eShopIdString, uint poNumber, uint8 poItemNumber) external;
}