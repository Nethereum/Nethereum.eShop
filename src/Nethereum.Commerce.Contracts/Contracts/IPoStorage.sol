pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

interface IPoStorage
{
    function configure(string calldata nameOfEternalStorage) external;

    // PO number range
    function getNextPoNumber() external returns (uint64 poNumber);
    function getCurrentPoNumber() external view returns (uint64 poNumber);

    // PO documents
    function setPo(IPoTypes.Po calldata po) external;
    function getPoByEthPoNumber(uint64 ethPoNumber) external view returns (IPoTypes.Po memory po);
    function getPoByBuyerPoNumber(bytes32 buyerSystemId, bytes32 buyerPoNumber) external view returns (IPoTypes.Po memory po);
}

