pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

interface IPoStorage
{
    function configure(string calldata nameOfEternalStorage) external;

    // PO number range
    function incrementNonce(address a) external;
    function getCurrentNonce(address a) external view returns (uint nonce);
    
    function incrementPoNumber() external;
    function getCurrentPoNumber() external view returns (uint poNumber);

    // PO documents
    function setPo(IPoTypes.Po calldata po) external;
    function getPo(uint poNumber) external view returns (IPoTypes.Po memory po);
    function getPoNumberByBuyerAddressAndNonce(address a, uint nonce) external view returns (uint poNumber);
}

