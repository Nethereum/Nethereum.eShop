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
    // PO record field names
    // Header
    string constant private PO_NUMBER = "poNumber";
    string constant private BUYER_ADDRESS = "buyerAddress";
    string constant private BUYER_WALLET_ADDRESS = "buyerWalletAddress";
    string constant private BUYER_NONCE = "buyerNonce";
    string constant private PO_TYPE = "poType";
    string constant private SELLER_SYS_ID = "sellerSysId";
    string constant private PO_CREATE_DATE = "poCreateDate";
    string constant private PO_ITEM_COUNT = "poItemCount";

    // Line Items
    string constant private PO_ITEM_NUMBER = "poItemNumber";
    string constant private SO_NUMBER = "soNumber";
    string constant private SO_ITEM_NUMBER = "soItemNumber";
    string constant private PRODUCT_ID = "productId";
    string constant private QUANTITY = "quantity";
    string constant private UNIT = "unit";
    string constant private QUANTITY_SYMBOL = "quantitySymbol";
    string constant private QUANTITY_ADDRESS = "quantityAddress";
    string constant private CURRENCY_VALUE = "currencyValue";
    string constant private CURRENCY_SYMBOL = "currencySymbol";
    string constant private CURRENCY_ADDRESS = "currencyAddress";
    string constant private STATUS = "status";
    string constant private GOODS_ISSUE_DATE = "goodsIssueDate";
    string constant private ESCROW_RELEASE_DATE = "escrowReleaseDate";
    string constant private CANCEL_STATUS = "cancelStatus";
    
    // Number range field names
    string constant private PO_GLOBAL_NUMBER = "po.global.number";

    // Names of mappings in eternal storage
    // Mapping buyer address => their current nonce
    string constant private MAP_ADDRESS_TO_NONCE = "mapAddressToNonce";

    // Mapping [buyer address + nonce] => po number
    string constant private MAP_ADDRESS_AND_NONCE_TO_PO_NUMBER = "mapAddressAndNonceToPoNumber";

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
    function incrementNonce(address a) onlyRegisteredCaller() override public
    {
        uint newNonce = getCurrentNonce(a);
        newNonce++;
        eternalStorage.setMappingAddressToUint256Value(stringToBytes32(MAP_ADDRESS_TO_NONCE), a, newNonce);
    }

    function getCurrentNonce(address a) override public view returns (uint nonce)
    {
        nonce = eternalStorage.getMappingAddressToUint256Value(stringToBytes32(MAP_ADDRESS_TO_NONCE), a);
    }

    function getCurrentPoNumber() override public view returns (uint poNumber)
    {
        poNumber = uint(eternalStorage.getUint256Value(keccak256(abi.encodePacked(PO_GLOBAL_NUMBER))));
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
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(PO_GLOBAL_NUMBER)), poNumber);
    }
    
    //------------------------------------------------------------------------------------------
    // PO data
    //------------------------------------------------------------------------------------------
    function getPoNumberByAddressAndNonce(address a, uint nonce) override public view returns (uint poNumber)
    {
        // Use mapping to get PO number from [buyer address + buyer nonce]
        bytes32 mappingKey = keccak256(abi.encodePacked(a, nonce));
        poNumber = eternalStorage.getMappingBytes32ToUint256Value(stringToBytes32(MAP_ADDRESS_AND_NONCE_TO_PO_NUMBER), mappingKey);
    }
    
    function getPo(uint poNumber) override public view returns (IPoTypes.Po memory po)
    {
        // Retrieve PO from storage
        // Header
        po.poNumber = eternalStorage.getUint256Value(keccak256(abi.encodePacked(poNumber, PO_NUMBER)));
        po.buyerAddress = eternalStorage.getAddressValue(keccak256(abi.encodePacked(poNumber, BUYER_ADDRESS)));
        po.buyerWalletAddress = eternalStorage.getAddressValue(keccak256(abi.encodePacked(poNumber, BUYER_WALLET_ADDRESS)));
        po.buyerNonce = eternalStorage.getUint256Value(keccak256(abi.encodePacked(poNumber, BUYER_NONCE)));
        po.poType = IPoTypes.PoType(eternalStorage.getUint256Value(keccak256(abi.encodePacked(poNumber, PO_TYPE))));
        po.sellerSysId = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(poNumber, SELLER_SYS_ID)));
        po.poCreateDate = eternalStorage.getUint256Value(keccak256(abi.encodePacked(poNumber, PO_CREATE_DATE)));
        po.poItemCount = uint8(eternalStorage.getUint256Value(keccak256(abi.encodePacked(poNumber, PO_ITEM_COUNT))));
        uint len = po.poItemCount;
        
        // Line items
        po.poItems = new IPoTypes.PoItem[](len);
        for (uint i = 0; i < len; i++)
        {
            bytes32 lineItemKey = keccak256(abi.encodePacked(po.poNumber, i));
            po.poItems[i].poItemNumber = uint8(eternalStorage.getUint256Value(keccak256(abi.encodePacked(lineItemKey, PO_ITEM_NUMBER))));
            po.poItems[i].soNumber = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(lineItemKey, SO_NUMBER)));
            po.poItems[i].soItemNumber = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(lineItemKey, SO_ITEM_NUMBER)));
            po.poItems[i].productId = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(lineItemKey, PRODUCT_ID)));
            po.poItems[i].quantity = eternalStorage.getUint256Value(keccak256(abi.encodePacked(lineItemKey, QUANTITY)));
            po.poItems[i].unit = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(lineItemKey, UNIT)));
            po.poItems[i].quantitySymbol = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(lineItemKey, QUANTITY_SYMBOL)));
            po.poItems[i].quantityAddress = eternalStorage.getAddressValue(keccak256(abi.encodePacked(lineItemKey, QUANTITY_ADDRESS)));
            po.poItems[i].currencyValue = eternalStorage.getUint256Value(keccak256(abi.encodePacked(lineItemKey, CURRENCY_VALUE)));
            po.poItems[i].currencySymbol = eternalStorage.getBytes32Value(keccak256(abi.encodePacked(lineItemKey, CURRENCY_SYMBOL)));
            po.poItems[i].currencyAddress = eternalStorage.getAddressValue(keccak256(abi.encodePacked(lineItemKey, CURRENCY_ADDRESS)));
            po.poItems[i].status = IPoTypes.PoItemStatus(eternalStorage.getUint256Value(keccak256(abi.encodePacked(lineItemKey, STATUS))));
            po.poItems[i].goodsIssueDate = eternalStorage.getUint256Value(keccak256(abi.encodePacked(lineItemKey, GOODS_ISSUE_DATE)));
            po.poItems[i].escrowReleaseDate = eternalStorage.getUint256Value(keccak256(abi.encodePacked(lineItemKey, ESCROW_RELEASE_DATE)));
            po.poItems[i].cancelStatus = IPoTypes.PoItemCancelStatus(eternalStorage.getUint256Value(keccak256(abi.encodePacked(lineItemKey, CANCEL_STATUS))));
        }
    }

    function setPo(IPoTypes.Po memory po) onlyRegisteredCaller() override public
    {
        // Update main PO storage
        uint len = po.poItems.length;
        
        // Header
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(po.poNumber, PO_NUMBER)), po.poNumber);
        eternalStorage.setAddressValue(keccak256(abi.encodePacked(po.poNumber, BUYER_ADDRESS)), po.buyerAddress);
        eternalStorage.setAddressValue(keccak256(abi.encodePacked(po.poNumber, BUYER_WALLET_ADDRESS)), po.buyerWalletAddress);
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(po.poNumber, BUYER_NONCE)), po.buyerNonce);
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(po.poNumber, PO_TYPE)), uint256(po.poType));
        eternalStorage.setBytes32Value(keccak256(abi.encodePacked(po.poNumber, SELLER_SYS_ID)), po.sellerSysId);
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(po.poNumber, PO_CREATE_DATE)), po.poCreateDate);
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(po.poNumber, PO_ITEM_COUNT)), len);
        
        // Line Items
        for (uint i = 0; i < len; i++)
        {
            bytes32 lineItemKey = keccak256(abi.encodePacked(po.poNumber, i));
            eternalStorage.setUint256Value(keccak256(abi.encodePacked(lineItemKey, PO_ITEM_NUMBER)), po.poItems[i].poItemNumber);
            eternalStorage.setBytes32Value(keccak256(abi.encodePacked(lineItemKey, SO_NUMBER)), po.poItems[i].soNumber);
            eternalStorage.setBytes32Value(keccak256(abi.encodePacked(lineItemKey, SO_ITEM_NUMBER)), po.poItems[i].soItemNumber);
            eternalStorage.setBytes32Value(keccak256(abi.encodePacked(lineItemKey, PRODUCT_ID)), po.poItems[i].productId);
            eternalStorage.setUint256Value(keccak256(abi.encodePacked(lineItemKey, QUANTITY)), po.poItems[i].quantity);
            eternalStorage.setBytes32Value(keccak256(abi.encodePacked(lineItemKey, UNIT)), po.poItems[i].unit);
            eternalStorage.setBytes32Value(keccak256(abi.encodePacked(lineItemKey, QUANTITY_SYMBOL)), po.poItems[i].quantitySymbol);
            eternalStorage.setAddressValue(keccak256(abi.encodePacked(lineItemKey, QUANTITY_ADDRESS)), po.poItems[i].quantityAddress);
            eternalStorage.setUint256Value(keccak256(abi.encodePacked(lineItemKey, CURRENCY_VALUE)), uint256(po.poItems[i].currencyValue));
            eternalStorage.setBytes32Value(keccak256(abi.encodePacked(lineItemKey, CURRENCY_SYMBOL)), po.poItems[i].currencySymbol);
            eternalStorage.setAddressValue(keccak256(abi.encodePacked(lineItemKey, CURRENCY_ADDRESS)), po.poItems[i].currencyAddress);
            eternalStorage.setUint256Value(keccak256(abi.encodePacked(lineItemKey, STATUS)), uint256(po.poItems[i].status));
            eternalStorage.setUint256Value(keccak256(abi.encodePacked(lineItemKey, GOODS_ISSUE_DATE)), po.poItems[i].goodsIssueDate);
            eternalStorage.setUint256Value(keccak256(abi.encodePacked(lineItemKey, ESCROW_RELEASE_DATE)), po.poItems[i].escrowReleaseDate);
            eternalStorage.setUint256Value(keccak256(abi.encodePacked(lineItemKey, CANCEL_STATUS)), uint256(po.poItems[i].cancelStatus));
        }
        
        // Update mapping of [buyer address + nonce] => po number
        bytes32 mappingKey = keccak256(abi.encodePacked(po.buyerAddress, po.buyerNonce));
        eternalStorage.setMappingBytes32ToUint256Value(stringToBytes32(MAP_ADDRESS_AND_NONCE_TO_PO_NUMBER), mappingKey, uint256(po.poNumber));
    }
}

