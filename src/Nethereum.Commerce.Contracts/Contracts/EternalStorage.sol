pragma solidity ^0.5.3;

import "./IEternalStorage.sol";
import "./Claimable.sol";
import "./IAddressRegistry.sol";

/// @title Eternal Storage
/// @dev A key value storage container for contracts.
/// Allows the bound contract to be upgraded whilst maintaining storage data
contract EternalStorage is IEternalStorage, Claimable
{
    // Field names
    bytes constant private CONTRACT_ADDRESS = "contract.address";
    bytes constant private CONTRACT_STORAGE_INITIALISED = "contract.storage.initialised";

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

    /// @dev Modifier throws when A: is initialised and sender isn't the bound contract address.  B: not initialised and sender is not owner.
    modifier onlyRegisteredCaller()
    {
        //TODO
        //if(getStorageInitialised()) {
        //    require(getContractAddress() == msg.sender, "Once storage is initialised - only the contract address can invoke this function");
        //}
        //else{
        //    require(msg.sender == owner, "Until the storage is initialised - only the owner can invoke this function");
        //}
        _;
    }

    /// @dev Binds the eternal storage contract to a specific calling contract
    /// @param _contractAddress the contract address to attach to
    function bindToContract(address _contractAddress) onlyOwner() public
    {
        setContractAddress(_contractAddress);
        setStorageInitialised(true);
    }

    /// @dev Un-Binds the eternal storage contract from the calling contract - returning control to the owner
    function unBindFromContract() onlyOwner() public
    {
        setContractAddress(address(0));
        setStorageInitialised(false);
    }

    /// @dev Binds the eternal storage to a specific contract.
    /// @param _address the contract address to attach to
    function setContractAddress(address _address) onlyOwner() private
    {
        AddressStorage[keccak256(CONTRACT_ADDRESS)] = _address;
    }

    /// @dev Returns the contract address the eternal storage is bound to.
    function getContractAddress() public view returns (address)
    {
        return AddressStorage[keccak256(CONTRACT_ADDRESS)];
    }

    /// @dev Setting the storage as initialized prevents anyone but the contract address calling setters.
    /// @param _initialised A bool flag to indicate whether or not the storage should be initialised.
    function setStorageInitialised(bool _initialised) private
    {
        BooleanStorage[keccak256(CONTRACT_STORAGE_INITIALISED)] = _initialised;
    }

    function getStorageInitialised() public view returns (bool)
    {
        return BooleanStorage[keccak256(CONTRACT_STORAGE_INITIALISED)];
    }

    function getInt256Value(bytes32 key) public view returns (int256)
    {
        return IntStorage[key];
    }

    function setInt256Value(bytes32 key, int256 value) onlyRegisteredCaller() public
    {
        IntStorage[key] = value;
    }
    
    function getUint256Value(bytes32 key) public view returns (uint256)
    {
        return UIntStorage[key];
    }

    function setUint256Value(bytes32 key, uint256 value) onlyRegisteredCaller() public
    {
        UIntStorage[key] = value;
    }

    function getStringValue(bytes32 key)  public view returns (string memory)
    {
        return StringStorage[key];
    }

    function setStringValue(bytes32 key, string memory value) onlyRegisteredCaller() public
    {
        StringStorage[key] = value;
    }

    function getAddressValue(bytes32 key)  public view returns (address)
    {
        return AddressStorage[key];
    }

    function setAddressValue(bytes32 key, address value) onlyRegisteredCaller() public
    {
        AddressStorage[key] = value;
    }

    function getBytes32Value(bytes32 key)  public view returns (bytes32)
    {
        return BytesStorage[key];
    }

    function setBytes32Value(bytes32 key, bytes32 value) onlyRegisteredCaller() public
    {
        BytesStorage[key] = value;
    }

    function getBooleanValue(bytes32 key)  public view returns (bool)
    {
        return BooleanStorage[key];
    }

    function setBooleanValue(bytes32 key, bool value) onlyRegisteredCaller() public
    {
        BooleanStorage[key] = value;
    }

    /// @param storageKey identifies what mapping to use in storage
    /// @param mappingKey identifies what mapping KEY to use inside the mapping identified by storageKey
    function getMappingBytes32ToUint256Value(bytes32 storageKey, bytes32 mappingKey)  public view returns (uint256)
    {
        return MappingBytes32ToUintStorage[storageKey][mappingKey];
    }

    /// @param storageKey identifies what mapping to use in storage
    /// @param mappingKey identifies what mapping KEY to use inside the mapping identified by storageKey
    function setMappingBytes32ToUint256Value(bytes32 storageKey, bytes32 mappingKey, uint256 value) onlyRegisteredCaller() public
    {
        MappingBytes32ToUintStorage[storageKey][mappingKey] = value;
    }

    /// @param storageKey identifies what mapping to use in storage
    /// @param mappingKey identifies what mapping KEY to use inside the mapping identified by storageKey
    function getMappingBytes32ToBytes32Value(bytes32 storageKey, bytes32 mappingKey) public view returns (bytes32)
    {
        return MappingBytes32ToBytes32Storage[storageKey][mappingKey];
    }

    /// @param storageKey identifies what mapping to use in storage
    /// @param mappingKey identifies what mapping KEY to use inside the mapping identified by storageKey
    function setMappingBytes32ToBytes32Value(bytes32 storageKey, bytes32 mappingKey, bytes32 value) public
    {
        MappingBytes32ToBytes32Storage[storageKey][mappingKey] = value;
    }

    /// @param storageKey identifies what mapping to use in storage
    /// @param mappingKey identifies what mapping KEY to use inside the mapping identified by storageKey
    function getMappingBytes32ToAddressValue(bytes32 storageKey, bytes32 mappingKey) public view returns (address)
    {
        return MappingBytes32ToAddressStorage[storageKey][mappingKey];
    }

    /// @param storageKey identifies what mapping to use in storage
    /// @param mappingKey identifies what mapping KEY to use inside the mapping identified by storageKey
    function setMappingBytes32ToAddressValue(bytes32 storageKey, bytes32 mappingKey, address value) public
    {
        MappingBytes32ToAddressStorage[storageKey][mappingKey] = value;
    }

    /// @param storageKey identifies what mapping to use in storage
    /// @param mappingKey identifies what mapping KEY to use inside the mapping identified by storageKey
    function getMappingBytes32ToBoolValue(bytes32 storageKey, bytes32 mappingKey) public view returns (bool)
    {
        return MappingBytes32ToBoolStorage[storageKey][mappingKey];
    }

    /// @param storageKey identifies what mapping to use in storage
    /// @param mappingKey identifies what mapping KEY to use inside the mapping identified by storageKey
    function setMappingBytes32ToBoolValue(bytes32 storageKey, bytes32 mappingKey, bool value) public
    {
        MappingBytes32ToBoolStorage[storageKey][mappingKey] = value;
    }
}