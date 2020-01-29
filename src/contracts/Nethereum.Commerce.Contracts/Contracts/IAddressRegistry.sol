pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2; 

/// @dev nethereum code gen doesn't yet support overloaded functions, hence postfix String on some names
/// @dev see https://github.com/Nethereum/Nethereum/issues/532
interface IAddressRegistry
{
    function getAddress(bytes32 contractName) external view returns (address a);
    function getAddressString(string calldata contractName) external view returns (address a);
    function registerAddress(bytes32 contractName, address a) external;
    function registerAddressString(string calldata contractName, address a) external;
    function getAllAddresses() external view returns (string[] memory contractNames, address[] memory contractAddresses);
}