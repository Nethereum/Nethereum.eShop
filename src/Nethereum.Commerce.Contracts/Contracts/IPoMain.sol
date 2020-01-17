pragma solidity ^0.5.3;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

interface IPoMain
{
    function configure(string calldata nameOfPoStorage, string calldata nameOfProductStorage,
        string calldata nameOfBusinessPartnerStorage, string calldata nameOfFundingContract) external;

    function createPurchaseOrder(IPoTypes.Po calldata po) external returns (bool requestSuccessful);
    function cancelPurchaseOrder(uint64 ethPoNumber) external returns (bool requestSuccessful);
    function setSalesOrderNumberByEthPoNumber(uint64 ethPoNumber, bytes32 sellerSysId, bytes32 sellerSalesOrderNumber) external;
    function writePoToEventLog(uint64 ethPoNumber) external;

    function getSalesOrderNumberByEthPoNumber(uint64 ethPoNumber) external view returns (bytes32 sellerSysId, bytes32 sellerSalesOrderNumber);
    function getSalesOrderNumberByBuyerPoNumber(bytes32 buyerSystemId, bytes32 buyerPoNumber) external view returns (bytes32 sellerSysId, bytes32 sellerSalesOrderNumber);    
    function getPoByEthPoNumber(uint64 ethPoNumber) external view returns (IPoTypes.Po memory po);
    function getPoByBuyerPoNumber(bytes32 buyerSystemId, bytes32 buyerPoNumber) external view returns (IPoTypes.Po memory po);
    function getLatestPoNumber() external view returns (uint64 poNumber);

    function refundPoToBuyer(uint64 ethPoNumber) external;
    function releasePoFundsToSeller(uint64 ethPoNumber) external;
    function reportSalesOrderNotApproved(uint64 ethPoNumber) external;
    function reportSalesOrderCancelFailure(uint64 ethPoNumber) external;
    function reportSalesOrderInvoiceFault(uint64 ethPoNumber) external;
}