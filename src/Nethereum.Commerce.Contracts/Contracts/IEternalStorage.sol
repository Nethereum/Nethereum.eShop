pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

interface IEternalStorage
{
    // Base Types
    function getInt256Value(bytes32 key) external view returns (int256);
    function setInt256Value(bytes32 key, int256 value) external;

    function getUint256Value(bytes32 key) external view returns (uint256);
    function setUint256Value(bytes32 key, uint256 value) external;

    function getStringValue(bytes32 key) external view returns (string memory);
    function setStringValue(bytes32 key, string calldata value) external;

    function getAddressValue(bytes32 key) external view returns (address);
    function setAddressValue(bytes32 key, address value) external;

    function getBytes32Value(bytes32 key) external view returns (bytes32);
    function setBytes32Value(bytes32 key, bytes32 value) external;

    function getBooleanValue(bytes32 key) external view returns (bool);
    function setBooleanValue(bytes32 key, bool value) external;

    // Mappings
    function getMappingBytes32ToUint256Value(bytes32 storageKey, bytes32 mappingKey) external view returns (uint256);
    function setMappingBytes32ToUint256Value(bytes32 storageKey, bytes32 mappingKey, uint256 value) external;

    function getMappingBytes32ToBytes32Value(bytes32 storageKey, bytes32 mappingKey) external view returns (bytes32);
    function setMappingBytes32ToBytes32Value(bytes32 storageKey, bytes32 mappingKey, bytes32 value) external;

    function getMappingBytes32ToAddressValue(bytes32 storageKey, bytes32 mappingKey) external view returns (address);
    function setMappingBytes32ToAddressValue(bytes32 storageKey, bytes32 mappingKey, address value) external;

    function getMappingBytes32ToBoolValue(bytes32 storageKey, bytes32 mappingKey) external view returns (bool);
    function setMappingBytes32ToBoolValue(bytes32 storageKey, bytes32 mappingKey, bool value) external;
}