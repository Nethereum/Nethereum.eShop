pragma solidity ^0.5.3;
pragma experimental ABIEncoderV2;

contract IEternalStorage
{
    // Base Types
    function getInt256Value(bytes32 key) public view returns (int256);
    function setInt256Value(bytes32 key, int256 value) public;

    function getUint256Value(bytes32 key) public view returns (uint256);
    function setUint256Value(bytes32 key, uint256 value) public;

    function getStringValue(bytes32 key) public view returns (string memory);
    function setStringValue(bytes32 key, string memory value) public;

    function getAddressValue(bytes32 key) public view returns (address);
    function setAddressValue(bytes32 key, address value) public;

    function getBytes32Value(bytes32 key) public view returns (bytes32);
    function setBytes32Value(bytes32 key, bytes32 value) public;

    function getBooleanValue(bytes32 key) public view returns (bool);
    function setBooleanValue(bytes32 key, bool value) public;

    // Mappings
    function getMappingBytes32ToUint256Value(bytes32 storageKey, bytes32 mappingKey) public view returns (uint256);
    function setMappingBytes32ToUint256Value(bytes32 storageKey, bytes32 mappingKey, uint256 value) public;

    function getMappingBytes32ToBytes32Value(bytes32 storageKey, bytes32 mappingKey) public view returns (bytes32);
    function setMappingBytes32ToBytes32Value(bytes32 storageKey, bytes32 mappingKey, bytes32 value) public;

    function getMappingBytes32ToAddressValue(bytes32 storageKey, bytes32 mappingKey) public view returns (address);
    function setMappingBytes32ToAddressValue(bytes32 storageKey, bytes32 mappingKey, address value) public;

    function getMappingBytes32ToBoolValue(bytes32 storageKey, bytes32 mappingKey) public view returns (bool);
    function setMappingBytes32ToBoolValue(bytes32 storageKey, bytes32 mappingKey, bool value) public;
}