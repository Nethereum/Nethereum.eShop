pragma solidity ^0.6.1;

import "./IEternalStorage.sol";
import "./Ownable.sol";

/// @title Eternal Storage
/// @dev A key value storage container for contracts.
/// Allows the bound contracts to be upgraded whilst maintaining storage data
contract EternalStorage is IEternalStorage, Ownable
{
    // Governance - Bound addresses are able to call storage functions
    event AddressBound(address indexed a);
    event AddressUnBound(address indexed a);
    event AddressAlreadyBound(address indexed a);
    event AddressAlreadyUnBound(address indexed a);
    mapping(address => bool) public BoundAddresses;
    int public BoundAddressCount;
 
    // Storage
    mapping(bytes32 => uint256) private UIntStorage;
    mapping(bytes32 => int) private IntStorage;
    mapping(bytes32 => string) private StringStorage;
    mapping(bytes32 => address) private AddressStorage;
    mapping(bytes32 => bytes32) private BytesStorage;
    mapping(bytes32 => bool) private BooleanStorage;

    // Storage of mappings
    mapping(bytes32 => mapping(bytes32 => uint256)) private MappingBytes32ToUintStorage;
    mapping(bytes32 => mapping(bytes32 => bytes32)) private MappingBytes32ToBytes32Storage;
    mapping(bytes32 => mapping(bytes32 => address)) private MappingBytes32ToAddressStorage;
    mapping(bytes32 => mapping(bytes32 => bool)) private MappingBytes32ToBoolStorage;

    //-------------------------------------------------------------------------------------------
    // Governance functions
    //-------------------------------------------------------------------------------------------
    /// @dev Modifier throws if sender is not owner and not a bound contract address
    modifier onlyRegisteredCaller()
    {
        if (msg.sender == owner() || BoundAddresses[msg.sender] )
        {
           _;    
        }
        else
        {
            revert("Only contract owner or a bound address may call this function.");
        }
    }

    /// @dev Binds the eternal storage contract to a specific address
    /// @param a the contract address to attach to
    function bindAddress(address a) override onlyOwner() public
    {
        if (BoundAddresses[a])
        {
            emit AddressAlreadyBound(a);
        }
        else
        {
            BoundAddresses[a] = true;
            BoundAddressCount++;
            emit AddressBound(a);
        }
    }

    /// @dev Un-binds the eternal storage contract from the specified address
    function unBindAddress(address a) override onlyOwner() public
    {
        if (BoundAddresses[a])
        {
            BoundAddresses[a] = false;
            BoundAddressCount--;
            emit AddressUnBound(a);
        }
        else
        {
            emit AddressAlreadyUnBound(a);
        }
    }

    //-------------------------------------------------------------------------------------------
    // Storage functions
    //-------------------------------------------------------------------------------------------
    function getInt256Value(bytes32 key) override public view returns (int256)
    {
        return IntStorage[key];
    }

    function setInt256Value(bytes32 key, int256 value) onlyRegisteredCaller() override public
    {
        IntStorage[key] = value;
    }
    
    function getUint256Value(bytes32 key) override public view returns (uint256)
    {
        return UIntStorage[key];
    }

    function setUint256Value(bytes32 key, uint256 value) onlyRegisteredCaller() override public
    {
        UIntStorage[key] = value;
    }

    function getStringValue(bytes32 key) override public view returns (string memory)
    {
        return StringStorage[key];
    }

    function setStringValue(bytes32 key, string memory value) onlyRegisteredCaller() override public
    {
        StringStorage[key] = value;
    }

    function getAddressValue(bytes32 key) override public view returns (address)
    {
        return AddressStorage[key];
    }

    function setAddressValue(bytes32 key, address value) onlyRegisteredCaller() override public
    {
        AddressStorage[key] = value;
    }

    function getBytes32Value(bytes32 key) override public view returns (bytes32)
    {
        return BytesStorage[key];
    }

    function setBytes32Value(bytes32 key, bytes32 value) onlyRegisteredCaller() override public
    {
        BytesStorage[key] = value;
    }

    function getBooleanValue(bytes32 key) override public view returns (bool)
    {
        return BooleanStorage[key];
    }

    function setBooleanValue(bytes32 key, bool value) onlyRegisteredCaller() override public
    {
        BooleanStorage[key] = value;
    }

    /// @param storageKey identifies what mapping to use in storage
    /// @param mappingKey identifies what mapping KEY to use inside the mapping identified by storageKey
    function getMappingBytes32ToUint256Value(bytes32 storageKey, bytes32 mappingKey) override public view returns (uint256)
    {
        return MappingBytes32ToUintStorage[storageKey][mappingKey];
    }

    /// @param storageKey identifies what mapping to use in storage
    /// @param mappingKey identifies what mapping KEY to use inside the mapping identified by storageKey
    function setMappingBytes32ToUint256Value(bytes32 storageKey, bytes32 mappingKey, uint256 value) onlyRegisteredCaller() override public
    {
        MappingBytes32ToUintStorage[storageKey][mappingKey] = value;
    }

    /// @param storageKey identifies what mapping to use in storage
    /// @param mappingKey identifies what mapping KEY to use inside the mapping identified by storageKey
    function getMappingBytes32ToBytes32Value(bytes32 storageKey, bytes32 mappingKey) override public view returns (bytes32)
    {
        return MappingBytes32ToBytes32Storage[storageKey][mappingKey];
    }

    /// @param storageKey identifies what mapping to use in storage
    /// @param mappingKey identifies what mapping KEY to use inside the mapping identified by storageKey
    function setMappingBytes32ToBytes32Value(bytes32 storageKey, bytes32 mappingKey, bytes32 value) onlyRegisteredCaller() override public
    {
        MappingBytes32ToBytes32Storage[storageKey][mappingKey] = value;
    }

    /// @param storageKey identifies what mapping to use in storage
    /// @param mappingKey identifies what mapping KEY to use inside the mapping identified by storageKey
    function getMappingBytes32ToAddressValue(bytes32 storageKey, bytes32 mappingKey) override public view returns (address)
    {
        return MappingBytes32ToAddressStorage[storageKey][mappingKey];
    }

    /// @param storageKey identifies what mapping to use in storage
    /// @param mappingKey identifies what mapping KEY to use inside the mapping identified by storageKey
    function setMappingBytes32ToAddressValue(bytes32 storageKey, bytes32 mappingKey, address value) onlyRegisteredCaller() override public
    {
        MappingBytes32ToAddressStorage[storageKey][mappingKey] = value;
    }

    /// @param storageKey identifies what mapping to use in storage
    /// @param mappingKey identifies what mapping KEY to use inside the mapping identified by storageKey
    function getMappingBytes32ToBoolValue(bytes32 storageKey, bytes32 mappingKey) override public view returns (bool)
    {
        return MappingBytes32ToBoolStorage[storageKey][mappingKey];
    }

    /// @param storageKey identifies what mapping to use in storage
    /// @param mappingKey identifies what mapping KEY to use inside the mapping identified by storageKey
    function setMappingBytes32ToBoolValue(bytes32 storageKey, bytes32 mappingKey, bool value) onlyRegisteredCaller() override public
    {
        MappingBytes32ToBoolStorage[storageKey][mappingKey] = value;
    }
}