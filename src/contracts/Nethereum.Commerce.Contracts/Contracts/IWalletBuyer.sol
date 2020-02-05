pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

interface IWalletBuyer
{
    // Contract setup
    function configure(string calldata nameOfPurchasing, string calldata nameOfFunding) external;
    
    // Purchasing
    function getPo(uint poNumber) external view returns (IPoTypes.Po memory po);
    function createPurchaseOrder(IPoTypes.Po calldata po) external;
    function cancelPurchaseOrderItem(uint poNumber, uint8 poItemNumber) external;
    function setPoItemGoodsReceived(uint poNumber, uint8 poItemNumber) external;
}