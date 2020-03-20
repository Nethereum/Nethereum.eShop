pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPoStorage.sol";
import "./IEternalStorage.sol";
import "./IAddressRegistry.sol";
import "./Ownable.sol";
import "./Bindable.sol";
import "./StringConvertible.sol";

/// @title Po Storage
/// @dev Wraps eternal storage, provides PO get/set storage functions.
contract PoStorage is IPoStorage, Ownable, Bindable, StringConvertible
{
    // Client is hashed into every key to avoid collisions with other contracts using the same eternal storage
    string constant private CLIENT = "PoS"; 
    
    // PO record field names
    // Header
    string constant private PO_NUMBER = "poNumber";
    
    string constant private BUYER_USER_ADDRESS = "buyerUserAddress";
    string constant private BUYER_RECEIVER_ADDRESS = "buyerReceiverAddress";
    string constant private BUYER_WALLET_ADDRESS = "buyerWalletAddress";
    
    string constant private ESHOP_ID = "eShopId";
    string constant private QUOTE_ID = "quoteId";
    string constant private QUOTE_EXPIRY_DATE = "quoteExpiryDate";
    string constant private QUOTE_SIGNER_ADDRESS = "quoteSignerAddress";
    
    string constant private SELLER_ID = "sellerId";
    
    string constant private CURRENCY_SYMBOL = "currencySymbol";
    string constant private CURRENCY_ADDRESS = "currencyAddress";
    string constant private PO_TYPE = "poType";
    string constant private PO_CREATE_DATE = "poCreateDate";
    string constant private PO_ITEM_COUNT = "poItemCount";
    string constant private PO_RULES_COUNT = "rulesCount";

    // Line Items
    string constant private LINE_ITEM_PREFIX = "li";
    string constant private PO_ITEM_NUMBER = "poItemNumber";
    string constant private SO_NUMBER = "soNumber";
    string constant private SO_ITEM_NUMBER = "soItemNumber";
    string constant private PRODUCT_ID = "productId";
    string constant private QUANTITY = "quantity";
    string constant private UNIT = "unit";
    string constant private QUANTITY_SYMBOL = "quantitySymbol";
    string constant private QUANTITY_ADDRESS = "quantityAddress";
    string constant private CURRENCY_VALUE = "currencyValue";
    string constant private STATUS = "status";
    string constant private GOODS_ISSUED_DATE = "goodsIssuedDate";
    string constant private GOODS_RECEIVED_DATE = "goodsReceivedDate";
    string constant private PLANNED_ESCROW_RELEASE_DATE = "plannedEscrowReleaseDate";
    string constant private ACTUAL_ESCROW_RELEASE_DATE = "actualEscrowReleaseDate";
    string constant private IS_ESCROW_RELEASED = "isEscrowReleased";
    string constant private CANCEL_STATUS = "cancelStatus";
    
    // Rules
    string constant private RULE_ITEM_PREFIX = "ru";
    
    // Number range field names
    string constant private PO_GLOBAL_NUMBER = "po.global.number";

    // Names of mappings in eternal storage
    // Mapping [eShopId + quoteId] => po number
    string constant private MAP_ESHOP_AND_QUOTE_TO_PO = "mapEshopAndQuoteToPo";

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
    
    //------------------------------------------------------------------------------------------
    // Number ranges
    //------------------------------------------------------------------------------------------
    function getCurrentPoNumber() override public view returns (uint poNumber)
    {
        poNumber = uint(eternalStorage.getUint256Value(keccak256(abi.encodePacked(CLIENT, PO_GLOBAL_NUMBER))));
    }
    
    /// @dev gets next po number and then increments it for next caller
    function incrementPoNumber() onlyRegisteredCaller() override public
    {
        uint currentPoNumber = getCurrentPoNumber();
        currentPoNumber++;
        setCurrentPoNumber(currentPoNumber);
    }

    function setCurrentPoNumber(uint poNumber) private
    {
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(CLIENT, PO_GLOBAL_NUMBER)), poNumber);
    }
    
    //------------------------------------------------------------------------------------------
    // PO data
    //------------------------------------------------------------------------------------------
    function getPoNumberByEshopIdAndQuote(bytes32 eShopId, uint quoteId) override public view returns (uint poNumber)
    {
        // Use mapping to get PO number from [eShopId + quoteId]
        bytes32 mappingKey = keccak256(abi.encodePacked(CLIENT, eShopId, quoteId));
        poNumber = eternalStorage.getMappingBytes32ToUint256Value(stringToBytes32(MAP_ESHOP_AND_QUOTE_TO_PO), mappingKey);
    }
    
    function getPo(uint poNumber) override public view returns (IPoTypes.Po memory po)
    {
        // Retrieve PO from storage
        // Header
        bytes32 headerKey = keccak256(abi.encodePacked(CLIENT, poNumber));
        po.poNumber = eternalStorage.getUint256Value(keccak256(abi.encodePacked(headerKey, PO_NUMBER)));
        
        po.buyerUserAddress = eternalStorage.getAddressValue(keccak256(abi.encodePacked(headerKey, BUYER_USER_ADDRESS)));
        po.buyerReceiverAddress = eternalStorage.getAddressValue(keccak256(abi.encodePacked(headerKey, BUYER_RECEIVER_ADDRESS)));
        po.buyerWalletAddress = eternalStorage.getAddressValue(keccak256(abi.encodePacked(headerKey, BUYER_WALLET_ADDRESS)));
        
        po.eShopId = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(headerKey, ESHOP_ID)));
        po.quoteId = eternalStorage.getUint256Value(keccak256(abi.encodePacked(headerKey, QUOTE_ID)));
        po.quoteExpiryDate = eternalStorage.getUint256Value(keccak256(abi.encodePacked(headerKey, QUOTE_EXPIRY_DATE)));
        po.quoteSignerAddress = eternalStorage.getAddressValue(keccak256(abi.encodePacked(headerKey, QUOTE_SIGNER_ADDRESS)));
        
        po.sellerId = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(headerKey, SELLER_ID)));
        
        po.currencySymbol = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(headerKey, CURRENCY_SYMBOL)));
        po.currencyAddress = eternalStorage.getAddressValue(keccak256(abi.encodePacked(headerKey, CURRENCY_ADDRESS)));
        po.poType = IPoTypes.PoType(eternalStorage.getUint256Value(keccak256(abi.encodePacked(headerKey, PO_TYPE))));
        po.poCreateDate = eternalStorage.getUint256Value(keccak256(abi.encodePacked(headerKey, PO_CREATE_DATE)));
        po.poItemCount = uint8(eternalStorage.getUint256Value(keccak256(abi.encodePacked(headerKey, PO_ITEM_COUNT))));
        
        // Line items
        uint lenItems = po.poItemCount;
        po.poItems = new IPoTypes.PoItem[](lenItems);
        for (uint i = 0; i < lenItems; i++)
        {
            bytes32 lineItemKey = keccak256(abi.encodePacked(CLIENT, LINE_ITEM_PREFIX, po.poNumber, i));
            po.poItems[i].poNumber = eternalStorage.getUint256Value(keccak256(abi.encodePacked(lineItemKey, PO_NUMBER)));
            po.poItems[i].poItemNumber = uint8(eternalStorage.getUint256Value(keccak256(abi.encodePacked(lineItemKey, PO_ITEM_NUMBER))));
            po.poItems[i].soNumber = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(lineItemKey, SO_NUMBER)));
            po.poItems[i].soItemNumber = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(lineItemKey, SO_ITEM_NUMBER)));
            po.poItems[i].productId = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(lineItemKey, PRODUCT_ID)));
            po.poItems[i].quantity = eternalStorage.getUint256Value(keccak256(abi.encodePacked(lineItemKey, QUANTITY)));
            po.poItems[i].unit = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(lineItemKey, UNIT)));
            po.poItems[i].quantitySymbol = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(lineItemKey, QUANTITY_SYMBOL)));
            po.poItems[i].quantityAddress = eternalStorage.getAddressValue(keccak256(abi.encodePacked(lineItemKey, QUANTITY_ADDRESS)));
            po.poItems[i].currencyValue = eternalStorage.getUint256Value(keccak256(abi.encodePacked(lineItemKey, CURRENCY_VALUE)));
            po.poItems[i].status = IPoTypes.PoItemStatus(eternalStorage.getUint256Value(keccak256(abi.encodePacked(lineItemKey, STATUS))));
            po.poItems[i].goodsIssuedDate = eternalStorage.getUint256Value(keccak256(abi.encodePacked(lineItemKey, GOODS_ISSUED_DATE)));
            po.poItems[i].goodsReceivedDate = eternalStorage.getUint256Value(keccak256(abi.encodePacked(lineItemKey, GOODS_RECEIVED_DATE)));
            po.poItems[i].plannedEscrowReleaseDate = eternalStorage.getUint256Value(keccak256(abi.encodePacked(lineItemKey, PLANNED_ESCROW_RELEASE_DATE)));
            po.poItems[i].actualEscrowReleaseDate = eternalStorage.getUint256Value(keccak256(abi.encodePacked(lineItemKey, ACTUAL_ESCROW_RELEASE_DATE)));
            po.poItems[i].isEscrowReleased = eternalStorage.getBooleanValue(keccak256(abi.encodePacked(lineItemKey, IS_ESCROW_RELEASED)));
            po.poItems[i].cancelStatus = IPoTypes.PoItemCancelStatus(eternalStorage.getUint256Value(keccak256(abi.encodePacked(lineItemKey, CANCEL_STATUS))));
        }
        
        // Rules
        po.rulesCount = uint8(eternalStorage.getUint256Value(keccak256(abi.encodePacked(headerKey, PO_RULES_COUNT))));
        uint lenRules = po.rulesCount;
        po.rules = new bytes32[](lenRules);
        for (uint i = 0; i < lenRules; i++)
        {
            bytes32 ruleItemKey = keccak256(abi.encodePacked(CLIENT, RULE_ITEM_PREFIX, po.poNumber, i));
            po.rules[i] = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(ruleItemKey)));
        }
    }

    function setPo(IPoTypes.Po memory po) onlyRegisteredCaller() override public
    {
        // Update main PO storage
        uint lenItems = po.poItems.length;
        uint lenRules = po.rules.length;
        
        // Header
        bytes32 headerKey = keccak256(abi.encodePacked(CLIENT, po.poNumber));
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(headerKey, PO_NUMBER)), po.poNumber);
        
        eternalStorage.setAddressValue(keccak256(abi.encodePacked(headerKey, BUYER_USER_ADDRESS)), po.buyerUserAddress);
        eternalStorage.setAddressValue(keccak256(abi.encodePacked(headerKey, BUYER_RECEIVER_ADDRESS)), po.buyerReceiverAddress);
        eternalStorage.setAddressValue(keccak256(abi.encodePacked(headerKey, BUYER_WALLET_ADDRESS)), po.buyerWalletAddress);
        
        eternalStorage.setBytes32Value(keccak256(abi.encodePacked(headerKey, ESHOP_ID)), po.eShopId);
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(headerKey, QUOTE_ID)), po.quoteId);
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(headerKey, QUOTE_EXPIRY_DATE)), po.quoteExpiryDate);
        eternalStorage.setAddressValue(keccak256(abi.encodePacked(headerKey, QUOTE_SIGNER_ADDRESS)), po.quoteSignerAddress);
        
        eternalStorage.setBytes32Value(keccak256(abi.encodePacked(headerKey, SELLER_ID)), po.sellerId);
        
        eternalStorage.setBytes32Value(keccak256(abi.encodePacked(headerKey, CURRENCY_SYMBOL)), po.currencySymbol);
        eternalStorage.setAddressValue(keccak256(abi.encodePacked(headerKey, CURRENCY_ADDRESS)), po.currencyAddress);
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(headerKey, PO_TYPE)), uint256(po.poType));
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(headerKey, PO_CREATE_DATE)), po.poCreateDate);
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(headerKey, PO_ITEM_COUNT)), lenItems);
        
        // Line Items
        for (uint i = 0; i < lenItems; i++)
        {
            bytes32 lineItemKey = keccak256(abi.encodePacked(CLIENT, LINE_ITEM_PREFIX, po.poNumber, i));
            eternalStorage.setUint256Value(keccak256(abi.encodePacked(lineItemKey, PO_NUMBER)), po.poItems[i].poNumber);
            eternalStorage.setUint256Value(keccak256(abi.encodePacked(lineItemKey, PO_ITEM_NUMBER)), po.poItems[i].poItemNumber);
            eternalStorage.setBytes32Value(keccak256(abi.encodePacked(lineItemKey, SO_NUMBER)), po.poItems[i].soNumber);
            eternalStorage.setBytes32Value(keccak256(abi.encodePacked(lineItemKey, SO_ITEM_NUMBER)), po.poItems[i].soItemNumber);
            eternalStorage.setBytes32Value(keccak256(abi.encodePacked(lineItemKey, PRODUCT_ID)), po.poItems[i].productId);
            eternalStorage.setUint256Value(keccak256(abi.encodePacked(lineItemKey, QUANTITY)), po.poItems[i].quantity);
            eternalStorage.setBytes32Value(keccak256(abi.encodePacked(lineItemKey, UNIT)), po.poItems[i].unit);
            eternalStorage.setBytes32Value(keccak256(abi.encodePacked(lineItemKey, QUANTITY_SYMBOL)), po.poItems[i].quantitySymbol);
            eternalStorage.setAddressValue(keccak256(abi.encodePacked(lineItemKey, QUANTITY_ADDRESS)), po.poItems[i].quantityAddress);
            eternalStorage.setUint256Value(keccak256(abi.encodePacked(lineItemKey, CURRENCY_VALUE)), uint256(po.poItems[i].currencyValue));
            eternalStorage.setUint256Value(keccak256(abi.encodePacked(lineItemKey, STATUS)), uint256(po.poItems[i].status));
            eternalStorage.setUint256Value(keccak256(abi.encodePacked(lineItemKey, GOODS_ISSUED_DATE)), po.poItems[i].goodsIssuedDate);
            eternalStorage.setUint256Value(keccak256(abi.encodePacked(lineItemKey, GOODS_RECEIVED_DATE)), po.poItems[i].goodsReceivedDate);
            eternalStorage.setUint256Value(keccak256(abi.encodePacked(lineItemKey, PLANNED_ESCROW_RELEASE_DATE)), po.poItems[i].plannedEscrowReleaseDate);
            eternalStorage.setUint256Value(keccak256(abi.encodePacked(lineItemKey, ACTUAL_ESCROW_RELEASE_DATE)), po.poItems[i].actualEscrowReleaseDate);
            eternalStorage.setBooleanValue(keccak256(abi.encodePacked(lineItemKey, IS_ESCROW_RELEASED)), po.poItems[i].isEscrowReleased);
            eternalStorage.setUint256Value(keccak256(abi.encodePacked(lineItemKey, CANCEL_STATUS)), uint256(po.poItems[i].cancelStatus));
        }
        
        // Rules
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(headerKey, PO_RULES_COUNT)), lenRules);
        for (uint i = 0; i < lenRules; i++)
        {
            bytes32 ruleItemKey = keccak256(abi.encodePacked(CLIENT, RULE_ITEM_PREFIX, po.poNumber, i));
            eternalStorage.setBytes32Value(keccak256(abi.encodePacked(ruleItemKey)), po.rules[i]);
        }
        
        // Update mapping of [eShopId + quoteId] => po number
        bytes32 mappingKey = keccak256(abi.encodePacked(CLIENT, po.eShopId, po.quoteId));
        eternalStorage.setMappingBytes32ToUint256Value(stringToBytes32(MAP_ESHOP_AND_QUOTE_TO_PO), mappingKey, uint256(po.poNumber));
    }
}

