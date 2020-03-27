pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

interface IBuyerWallet
{
    // Contract setup
    function configure(string calldata nameOfBusinessPartnerStorage) external;
    
    // Purchasing
    function getPo(string calldata eShopIdString, uint poNumber) external view returns (IPoTypes.Po memory po);
    function getPoByEshopIdAndQuote(string calldata eShopIdString, uint quoteId) external view returns (IPoTypes.Po memory po);
    function createPurchaseOrder(IPoTypes.Po calldata po, bytes calldata signature) external;
    function cancelPurchaseOrderItem(string calldata eShopIdString, uint poNumber, uint8 poItemNumber) external;
    function setPoItemGoodsReceived(string calldata eShopIdString, uint poNumber, uint8 poItemNumber) external;
}