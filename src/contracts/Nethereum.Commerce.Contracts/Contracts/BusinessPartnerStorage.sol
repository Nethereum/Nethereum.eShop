pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IBusinessPartnerStorage.sol";
import "./IEternalStorage.sol";
import "./IAddressRegistry.sol";
import "./Ownable.sol";
import "./Bindable.sol";
import "./StringConvertible.sol";

contract BusinessPartnerStorage is IBusinessPartnerStorage, Ownable, Bindable, StringConvertible
{
    IEternalStorage public eternalStorage;
    IAddressRegistry public addressRegistry;

    string constant private MAP_SYSTEM_ID_TO_WALLET_ADDRESS = "mapSystemIdToWalletAddress";
    string constant private MAP_SYSTEM_ID_TO_DESCRIPTION = "mapSystemIdToDescription";

    constructor (address contractAddressOfRegistry) public
    {
        addressRegistry = IAddressRegistry(contractAddressOfRegistry);
    }

    /// Configure contract
    /// @param nameOfEternalStorage key of the entry in the address registry that holds the eternal storage contract address
    function configure(string memory nameOfEternalStorage) onlyOwner() override public
    {
        // Eternal storage
        eternalStorage = IEternalStorage(addressRegistry.getAddressString(nameOfEternalStorage));
        require(address(eternalStorage) != address(0), "Could not find EternalStorage address in registry");
    }

    /// @dev Get mapping from [system id] to get [wallet address for that system id]
    function getWalletAddress(bytes32 systemId) override external view returns (address walletAddress)
    {
        bytes32 mappingKey = keccak256(abi.encodePacked(systemId));
        return eternalStorage.getMappingBytes32ToAddressValue(stringToBytes32(MAP_SYSTEM_ID_TO_WALLET_ADDRESS), mappingKey);
    }

    /// @dev Store mapping with key [system id] to store [wallet address for that system id]
    function setWalletAddress(bytes32 systemId, address walletAddress) onlyRegisteredCaller() override external
    {
        bytes32 mappingKey = keccak256(abi.encodePacked(systemId));
        eternalStorage.setMappingBytes32ToAddressValue(stringToBytes32(MAP_SYSTEM_ID_TO_WALLET_ADDRESS), mappingKey, walletAddress);
    }

    /// @dev Get mapping from [system id] to get [description for that system id]
    function getSystemDescription(bytes32 systemId) override external view returns (bytes32 systemDescription)
    {
        bytes32 mappingKey = keccak256(abi.encodePacked(systemId));
        return eternalStorage.getMappingBytes32ToBytes32Value(stringToBytes32(MAP_SYSTEM_ID_TO_DESCRIPTION), mappingKey);
    }

    /// @dev Store mapping with key [system id] to store [description for that system id]
    function setSystemDescription(bytes32 systemId, bytes32 systemDescription) onlyRegisteredCaller() override external
    {
        bytes32 mappingKey = keccak256(abi.encodePacked(systemId));
        eternalStorage.setMappingBytes32ToBytes32Value(stringToBytes32(MAP_SYSTEM_ID_TO_DESCRIPTION), mappingKey, systemDescription);
    }
}