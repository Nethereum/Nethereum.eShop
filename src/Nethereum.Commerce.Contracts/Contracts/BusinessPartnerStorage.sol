pragma solidity ^0.5.3;
pragma experimental ABIEncoderV2;

import "./IBusinessPartnerStorage.sol";
import "./IEternalStorage.sol";
import "./IAddressRegistry.sol";
import "./StringLib.sol";

contract BusinessPartnerStorage is IBusinessPartnerStorage
{
    IEternalStorage public eternalStorage;
    IAddressRegistry public addressRegistry;

    string constant private MAP_SELLER_GET_BUYER_VIEW_VENDOR = "mapSellerGetBuyerViewVendor";
    string constant private MAP_BUYER_GET_SELLER_VIEW_CUSTOMER = "mapBuyerGetSellerViewCustomer";
    string constant private MAP_SYSTEM_ID_TO_WALLET_ADDRESS = "mapSystemIdToWalletAddress";
    string constant private MAP_SYSTEM_ID_TO_DESCRIPTION = "mapSystemIdToDescription";

    constructor (address contractAddressOfRegistry) public payable
    {
        addressRegistry = IAddressRegistry(contractAddressOfRegistry);
    }

    /// Configure contract
    /// @param nameOfEternalStorage key of the entry in the address registry that holds the eternal storage contract address
    function configure(string memory nameOfEternalStorage) public
    {
        // Eternal storage
        eternalStorage = IEternalStorage(addressRegistry.getAddressString(nameOfEternalStorage));
        require(address(eternalStorage) != address(0), "Could not find EternalStorage address in registry");
    }

    /// @dev Get mapping from [buyer system id + seller system id] to get [customer id in seller system that represents the buyer system id]
    function getSellerViewCustomerIdForBuyerSysId(bytes32 buyerSystemId, bytes32 sellerSystemId) external view returns (bytes32 sellerViewCustomerId)
    {
        bytes32 mappingKey = keccak256(abi.encodePacked(buyerSystemId, sellerSystemId));
        return eternalStorage.getMappingBytes32ToBytes32Value(StringLib.stringToBytes32(MAP_BUYER_GET_SELLER_VIEW_CUSTOMER), mappingKey);
    }

    /// @dev Store mapping with key [buyer system id + seller system id] to store [customer id in seller system that represents the buyer system id]
    function setSellerViewCustomerIdForBuyerSysId(bytes32 buyerSystemId, bytes32 sellerSystemId, bytes32 sellerViewCustomerId) external
    {
        bytes32 mappingKey = keccak256(abi.encodePacked(buyerSystemId, sellerSystemId));
        eternalStorage.setMappingBytes32ToBytes32Value(StringLib.stringToBytes32(MAP_BUYER_GET_SELLER_VIEW_CUSTOMER), mappingKey, sellerViewCustomerId);
    }

    /// @dev Get mapping from [buyer system id + seller system id] to get [vendor id in buyer system that represents the seller system id]
    /// @dev Only needed if buyer system is an ERP-like system itself and needs a vendor number for its PO
    function getBuyerViewVendorIdForSellerSysId(bytes32 buyerSystemId, bytes32 sellerSystemId) external view returns (bytes32 buyerViewVendorId)
    {
        bytes32 mappingKey = keccak256(abi.encodePacked(buyerSystemId, sellerSystemId));
        return eternalStorage.getMappingBytes32ToBytes32Value(StringLib.stringToBytes32(MAP_SELLER_GET_BUYER_VIEW_VENDOR), mappingKey);
    }

    /// @dev Store mapping with key [buyer system id + seller system id] to store [vendor id in buyer system that represents the seller system id]
    /// @dev Only needed if buyer system is an ERP-like system itself and needs a vendor number for its PO
    function setBuyerViewVendorIdForSellerSysId(bytes32 buyerSystemId, bytes32 sellerSystemId, bytes32 buyerViewVendorId) external
    {
        bytes32 mappingKey = keccak256(abi.encodePacked(buyerSystemId, sellerSystemId));
        eternalStorage.setMappingBytes32ToBytes32Value(StringLib.stringToBytes32(MAP_SELLER_GET_BUYER_VIEW_VENDOR), mappingKey, buyerViewVendorId);
    }

    /// @dev Get mapping from [system id] to get [wallet address for that system id]
    function getWalletAddress(bytes32 systemId) external view returns (address walletAddress)
    {
        bytes32 mappingKey = keccak256(abi.encodePacked(systemId));
        return eternalStorage.getMappingBytes32ToAddressValue(StringLib.stringToBytes32(MAP_SYSTEM_ID_TO_WALLET_ADDRESS), mappingKey);
    }

    /// @dev Store mapping with key [system id] to store [wallet address for that system id]
    function setWalletAddress(bytes32 systemId, address walletAddress) external
    {
        bytes32 mappingKey = keccak256(abi.encodePacked(systemId));
        eternalStorage.setMappingBytes32ToAddressValue(StringLib.stringToBytes32(MAP_SYSTEM_ID_TO_WALLET_ADDRESS), mappingKey, walletAddress);
    }

    /// @dev Get mapping from [system id] to get [description for that system id]
    function getSystemDescription(bytes32 systemId) external view returns (bytes32 systemDescription)
    {
        bytes32 mappingKey = keccak256(abi.encodePacked(systemId));
        return eternalStorage.getMappingBytes32ToBytes32Value(StringLib.stringToBytes32(MAP_SYSTEM_ID_TO_DESCRIPTION), mappingKey);
    }

    /// @dev Store mapping with key [system id] to store [description for that system id]
    function setSystemDescription(bytes32 systemId, bytes32 systemDescription) external
    {
        bytes32 mappingKey = keccak256(abi.encodePacked(systemId));
        eternalStorage.setMappingBytes32ToBytes32Value(StringLib.stringToBytes32(MAP_SYSTEM_ID_TO_DESCRIPTION), mappingKey, systemDescription);
    }
}