pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

interface IPoStorage
{
    function configure(string calldata nameOfEternalStorage) external;

    // PO number range
    function incrementPoNumber() external;
    function getCurrentPoNumber() external view returns (uint poNumber);

    // PO documents
    function setPo(IPoTypes.Po calldata po) external;
    function getPo(uint poNumber) external view returns (IPoTypes.Po memory po);
    function getPoNumberByApproverAndQuote(address approverAddress, uint quoteId) external view returns (uint poNumber);
}