pragma solidity ^0.5.3;
pragma experimental ABIEncoderV2; 

import "./IAddressRegistry.sol";
import "./Ownable.sol";
import "./StringLib.sol";

/// @title Address Registry
/// @notice Central repository of all contract addresses
/// @notice Store is key value pair of free text identifier, address eg: "Dai", 0x12890D2cce102216644c59daE5baed380d84830c.
/// @notice Address value of 0x0 means key not used yet. Address value of 0x1 means key was used then de-registered.
/// @dev //TODO Ideally would like ANYONE to be able to add new, but only owner to change existing 
contract AddressRegistry is IAddressRegistry, Ownable
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
    function registerAddress(bytes32 contractName, address a) public //onlyOwner()
    {
        require(a != address(0), "Address must be > 0x0, use 0x1 to de-register an address");
        
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
    
    // @dev Helper fn to allow passing strings which will be truncated to 32
    function registerAddressString(string memory contractName, address a) public //onlyOwner()
    {
        bytes32 contractNameAsBytes32 = StringLib.stringToBytes32(contractName);
        registerAddress(contractNameAsBytes32, a);
    }
    
    function getAddress(bytes32 contractName) public view returns (address a)
    {
        return addressMap[contractName];
    }
    
    function getAllAddresses() public view returns (string[] memory contractNames, address[] memory contractAddresses)
    {
        contractNames = new string[](addressCount);
        contractAddresses = new address[](addressCount);
        for (uint i = 0; i < addressCount; i++)
        {
            contractNames[i] = StringLib.bytes32ToString(addressList[i], 0);
            contractAddresses[i] = addressMap[addressList[i]];
        }
    }
    
    /* nethereum code gen doesn't like below
    function getAllAddressesPaired() public view returns (addressPair[] memory addressPairs)
    {
        addressPairs = new addressPair[](addressCount);
        for (uint i = 0; i < addressCount; i++)
        {
            addressPair memory ap;
            ap.contractName = StringLib.bytes32ToString(addressList[i], 0); 
            ap.a = addressMap[addressList[i]];
            addressPairs[i] = ap;
        }
    }*/
    
    // @dev helper fn to allow passing strings which will be truncated to 32
    function getAddressString(string memory contractName) public view returns (address a)
    {
        bytes32 contractNameAsBytes32 = StringLib.stringToBytes32(contractName);
        return getAddress(contractNameAsBytes32);
    }
}