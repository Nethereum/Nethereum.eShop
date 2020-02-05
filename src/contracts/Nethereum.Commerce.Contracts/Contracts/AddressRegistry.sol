pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2; 

import "./IAddressRegistry.sol";
import "./Ownable.sol";
import "./StringConvertible.sol";

/// @title Address Registry
/// @notice Central repository of all contract addresses
/// @notice Store is key value pair of free text identifier, address eg: "Dai", 0x12890D2cce102216644c59daE5baed380d84830c.
/// @notice Address value of 0x0 means key not used yet. Address value of 0x1 means key was used then de-registered.
contract AddressRegistry is IAddressRegistry, Ownable, StringConvertible
{
    event ContractAddressRegistered(bytes32 indexed contractName, address indexed contractAddress);
    event ContractAddressChanged(bytes32 indexed contractName, address indexed oldContractAddress, address indexed newContractAddress);
    
    /// @dev mapping of bytes32 key to an address
    mapping (bytes32 => address) public addressMap;

    /// @dev internal list of bytes32 keys that have been used
    bytes32[] private addressList;

    /// @dev count of addresses in addressList
    uint private addressCount = 0;
    
    struct addressPair  
    {
        string contractName;
        address a;
    }  
    
    /// @dev Main entry point for address registration
    /// @param a the address to register must be > 0x0. Set address to 0x1 to de-register an address
    function registerAddress(bytes32 contractName, address a) override public onlyOwner()
    {
        require(a != address(0), "Address cannot be 0x0, use 0x1 to de-register an address");
        
        address existingAddress = addressMap[contractName];
        if (existingAddress == address(0))
        {
            // address is new
            addressMap[contractName] = a;
            addressList.push(contractName);
            addressCount++;
            emit ContractAddressRegistered(contractName, a);
        }
        else
        {
            // address exists already
            addressMap[contractName] = a;
            emit ContractAddressChanged(contractName, existingAddress, a);
        }
    }
    
    function registerAddressString(string memory contractName, address a) override public onlyOwner()
    {
        bytes32 contractNameAsBytes32 = stringToBytes32(contractName);
        registerAddress(contractNameAsBytes32, a);
    }
    
    function getAddress(bytes32 contractName) override public view returns (address a)
    {
        return addressMap[contractName];
    }
    
    function getAllAddresses() override public view returns (string[] memory contractNames, address[] memory contractAddresses)
    {
        contractNames = new string[](addressCount);
        contractAddresses = new address[](addressCount);
        for (uint i = 0; i < addressCount; i++)
        {
            contractNames[i] = bytes32ToString(addressList[i], 0);
            contractAddresses[i] = addressMap[addressList[i]];
        }
    }

    // @dev helper fn to allow passing strings which will be truncated to 32
    function getAddressString(string memory contractName) override public view returns (address a)
    {
        bytes32 contractNameAsBytes32 = stringToBytes32(contractName);
        return getAddress(contractNameAsBytes32);
    }
}