pragma solidity ^0.5.3;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

contract IPoStorage
{
    function configure(string memory nameOfEternalStorage) public;

    // PO number range
    function getNextPoNumber() public returns (uint64 poNumber);
    function getCurrentPoNumber() public view returns (uint64 poNumber);

    // PO documents
    function setPo(IPoTypes.Po memory po) public;
    function getPoByEthPoNumber(uint64 ethPoNumber) public view returns (IPoTypes.Po memory po);
    function getPoByBuyerPoNumber(bytes32 buyerSystemId, bytes32 buyerPoNumber) public view returns (IPoTypes.Po memory po);
}