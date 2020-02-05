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
    // Client is hashed into every key to avoid collisions with other contracts using the same eternal storage
    string constant private CLIENT = "BpStorage"; 

    // Seller Id
    string constant private SELLER_ID = "sellerId";
    string constant private SELLER_DESC = "sellerDescription";
    string constant private CONTRACT_ADDRESS = "contractAddress";
    string constant private APPROVER_ADDRESS = "approverAddress";
    string constant private IS_ACTIVE = "isActive";

    IEternalStorage public eternalStorage;
    IAddressRegistry public addressRegistry;

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
    
    function getSeller(bytes32 sellerId) override external view returns (IPoTypes.Seller memory seller)
    {
        seller.sellerId = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(CLIENT, sellerId, SELLER_ID)));
        seller.sellerDescription = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(CLIENT, sellerId, SELLER_DESC)));
        seller.contractAddress = eternalStorage.getAddressValue(keccak256(abi.encodePacked(CLIENT, sellerId, CONTRACT_ADDRESS)));
        seller.approverAddress = eternalStorage.getAddressValue(keccak256(abi.encodePacked(CLIENT, sellerId, APPROVER_ADDRESS)));
        seller.isActive = eternalStorage.getBooleanValue(keccak256(abi.encodePacked(CLIENT, sellerId, IS_ACTIVE)));
    }
    
    function setSeller(IPoTypes.Seller calldata seller) onlyRegisteredCaller() override external
    {
        eternalStorage.setBytes32Value(keccak256(abi.encodePacked(CLIENT, seller.sellerId, SELLER_ID)), seller.sellerId);
        eternalStorage.setBytes32Value(keccak256(abi.encodePacked(CLIENT, seller.sellerId, SELLER_DESC)), seller.sellerDescription);
        eternalStorage.setAddressValue(keccak256(abi.encodePacked(CLIENT, seller.sellerId, CONTRACT_ADDRESS)), seller.contractAddress);
        eternalStorage.setAddressValue(keccak256(abi.encodePacked(CLIENT, seller.sellerId, APPROVER_ADDRESS)), seller.approverAddress);
        eternalStorage.setBooleanValue(keccak256(abi.encodePacked(CLIENT, seller.sellerId, IS_ACTIVE)), seller.isActive);
    }
}