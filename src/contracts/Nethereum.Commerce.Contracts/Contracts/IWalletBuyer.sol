pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

interface IWalletBuyer
{
    // Contract setup
    function configure(string calldata nameOfPurchasing, string calldata nameOfFunding) external;
    
    // Purchasing
    function getPo(uint poNumber) external view returns (IPoTypes.Po memory po);
    function getPoBySellerAndQuote(string calldata sellerIdString, uint quoteId) external view returns (IPoTypes.Po memory po);
    function createPurchaseOrder(IPoTypes.Po calldata po, bytes calldata signature) external;
    function cancelPurchaseOrderItem(uint poNumber, uint8 poItemNumber) external;
    function setPoItemGoodsReceived(uint poNumber, uint8 poItemNumber) external;
}