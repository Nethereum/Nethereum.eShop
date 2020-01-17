pragma solidity ^0.5.3;
pragma experimental ABIEncoderV2;

/// @dev nethereum code gen doesn't (yet) support overloaded functions, hence postfix String on some names
/// @dev see https://github.com/Nethereum/Nethereum/issues/532
contract IAddressRegistry
{
    function getAddress(bytes32 contractName) public view returns (address a);
    function getAddressString(string memory contractName) public view returns (address a);
    function registerAddress(bytes32 contractName, address a) public;
    function registerAddressString(string memory contractName, address a) public;
    function getAllAddresses() public view returns (string[] memory contractNames, address[] memory contractAddresses);
}