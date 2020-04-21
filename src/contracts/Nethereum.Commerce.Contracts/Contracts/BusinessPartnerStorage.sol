pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IAddressRegistry.sol";
import "./IBusinessPartnerStorage.sol";
import "./IEternalStorage.sol";
import "./Ownable.sol";
import "./StringConvertible.sol";

contract BusinessPartnerStorage is IBusinessPartnerStorage, Ownable, StringConvertible
{
    // Client is hashed into every key to avoid collisions with other contracts using the same eternal storage
    string constant private CLIENT = "BpStorage"; 
    
    // eShop
    string constant private DATA_ESHOP = "eShop";
    string constant private ESHOP_ID = "eShopId";
    string constant private ESHOP_DESCRIPTION = "eShopDescription";
    string constant private PURCH_CONTRACT_ADDRESS = "purchasingContractAddress";
    string constant private IS_ACTIVE = "isActive";
    string constant private CREATED_BY_ADDRESS = "createdByAddress";
    string constant private QUOTE_SIGNER_COUNT = "quoteSignerCount";
    
    // eShop quote signers
    string constant private QUOTE_SIGNER_PREFIX = "qsp";
    
    // Seller Id
    string constant private DATA_SELLER = "seller";
    string constant private SELLER_ID = "sellerId";
    string constant private SELLER_DESC = "sellerDescription";
    string constant private ADMIN_CONTRACT_ADDRESS = "adminContractAddress";
    
    IEternalStorage public eternalStorage;

    constructor (address eternalStorageAddress) public
    {
        eternalStorage = IEternalStorage(eternalStorageAddress);
    }

    function reconfigure(address eternalStorageAddress) onlyOwner() override public
    {
        eternalStorage = IEternalStorage(eternalStorageAddress);
    }
    
    function getEshop(bytes32 eShopId) override public view returns (IPoTypes.Eshop memory eShop)
    {
        bytes32 recordKey = keccak256(abi.encodePacked(CLIENT, DATA_ESHOP, eShopId));
        eShop.eShopId = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(recordKey, ESHOP_ID)));
        eShop.eShopDescription = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(recordKey, ESHOP_DESCRIPTION)));
        eShop.purchasingContractAddress = eternalStorage.getAddressValue(keccak256(abi.encodePacked(recordKey, PURCH_CONTRACT_ADDRESS)));
        eShop.isActive = eternalStorage.getBooleanValue(keccak256(abi.encodePacked(recordKey, IS_ACTIVE)));
        eShop.createdByAddress = eternalStorage.getAddressValue(keccak256(abi.encodePacked(recordKey, CREATED_BY_ADDRESS)));
        
        // Quote signers
        eShop.quoteSignerCount = uint8(eternalStorage.getUint256Value(keccak256(abi.encodePacked(recordKey, QUOTE_SIGNER_COUNT))));
        uint lenSigners = eShop.quoteSignerCount;
        eShop.quoteSigners = new address[](lenSigners);
        for (uint i = 0; i < lenSigners; i++)
        {
            bytes32 signerItemKey = keccak256(abi.encodePacked(recordKey, QUOTE_SIGNER_PREFIX, i));
            eShop.quoteSigners[i] = eternalStorage.getAddressValue(keccak256(abi.encodePacked(signerItemKey)));
        }
    }
        
    /// @notice Create a new eShop or change existing. If changing an existing eShop, the transaction must by sent
    /// @notice by the same address that created the record. Any address can create a new record.
    function setEshop(IPoTypes.Eshop memory eShop) override public
    {
        // Validation
        uint lenSigners = eShop.quoteSigners.length;
        require(eShop.purchasingContractAddress != address(0), "Must specify a purchasing contract address");
        require(lenSigners != 0, "Must specify at least one quote signer address");
        
        // Is this a new record?
        IPoTypes.Eshop memory existingEshop = getEshop(eShop.eShopId);
        if (existingEshop.purchasingContractAddress == address(0))
        {
            // New record
            eShop.createdByAddress = msg.sender;
            emit EshopCreatedLog(eShop.createdByAddress, eShop.eShopId, eShop);
        }
        else
        {
            // Existing record
            require(existingEshop.createdByAddress == msg.sender, "Only createdByAddress can change this record");
            eShop.createdByAddress = existingEshop.createdByAddress;
            emit EshopChangedLog(eShop.createdByAddress, eShop.eShopId, eShop);
        }
        
        // Store
        bytes32 recordKey = keccak256(abi.encodePacked(CLIENT, DATA_ESHOP, eShop.eShopId));
        eternalStorage.setBytes32Value(keccak256(abi.encodePacked(recordKey, ESHOP_ID)), eShop.eShopId);
        eternalStorage.setBytes32Value(keccak256(abi.encodePacked(recordKey, ESHOP_DESCRIPTION)), eShop.eShopDescription);
        eternalStorage.setAddressValue(keccak256(abi.encodePacked(recordKey, PURCH_CONTRACT_ADDRESS)), eShop.purchasingContractAddress);
        eternalStorage.setBooleanValue(keccak256(abi.encodePacked(recordKey, IS_ACTIVE)), eShop.isActive);
        eternalStorage.setAddressValue(keccak256(abi.encodePacked(recordKey, CREATED_BY_ADDRESS)), eShop.createdByAddress);
        
        // Quote signers
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(recordKey, QUOTE_SIGNER_COUNT)), lenSigners);
        for (uint i = 0; i < lenSigners; i++)
        {
            bytes32 signerItemKey = keccak256(abi.encodePacked(recordKey, QUOTE_SIGNER_PREFIX, i));
            eternalStorage.setAddressValue(keccak256(abi.encodePacked(signerItemKey)), eShop.quoteSigners[i]);
        }
    }
    
    function getSeller(bytes32 sellerId) override public view returns (IPoTypes.Seller memory seller)
    {
        bytes32 recordKey = keccak256(abi.encodePacked(CLIENT, DATA_SELLER, sellerId));
        seller.sellerId = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(recordKey, SELLER_ID)));
        seller.sellerDescription = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(recordKey, SELLER_DESC)));
        seller.adminContractAddress = eternalStorage.getAddressValue(keccak256(abi.encodePacked(recordKey, ADMIN_CONTRACT_ADDRESS)));
        seller.isActive = eternalStorage.getBooleanValue(keccak256(abi.encodePacked(recordKey, IS_ACTIVE)));
        seller.createdByAddress = eternalStorage.getAddressValue(keccak256(abi.encodePacked(recordKey, CREATED_BY_ADDRESS)));
    }
    
    /// @notice Create a new Seller or change existing. If changing an existing Seller, the transaction must by sent
    /// @notice by the same address that created the record. Any address can create a new record.
    function setSeller(IPoTypes.Seller memory seller) override public
    {
        // Validation
        require(seller.adminContractAddress != address(0), "Must specify an admin contract address");
        
        // Is this a new record?
        IPoTypes.Seller memory existingSeller = getSeller(seller.sellerId);
        if (existingSeller.adminContractAddress == address(0))
        {
            // New record
            seller.createdByAddress = msg.sender;
            emit SellerCreatedLog(seller.createdByAddress, seller.sellerId, seller);
        }
        else
        {
            // Existing record
            require(existingSeller.createdByAddress == msg.sender, "Only createdByAddress can change this record");
            seller.createdByAddress = existingSeller.createdByAddress;
            emit SellerChangedLog(seller.createdByAddress, seller.sellerId, seller);
        }
        
        // Store
        bytes32 recordKey = keccak256(abi.encodePacked(CLIENT, DATA_SELLER, seller.sellerId));
        eternalStorage.setBytes32Value(keccak256(abi.encodePacked(recordKey, SELLER_ID)), seller.sellerId);
        eternalStorage.setBytes32Value(keccak256(abi.encodePacked(recordKey, SELLER_DESC)), seller.sellerDescription);
        eternalStorage.setAddressValue(keccak256(abi.encodePacked(recordKey, ADMIN_CONTRACT_ADDRESS)), seller.adminContractAddress);
        eternalStorage.setBooleanValue(keccak256(abi.encodePacked(recordKey, IS_ACTIVE)), seller.isActive);
        eternalStorage.setAddressValue(keccak256(abi.encodePacked(recordKey, CREATED_BY_ADDRESS)), seller.createdByAddress);
    }
}