pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPoStorage.sol";
import "./IEternalStorage.sol";
import "./IAddressRegistry.sol";
import "./Ownable.sol";
import "./Bindable.sol";
import "./StringConvertible.sol";

/// @title Po Storage
/// @dev Wraps eternal storage, provides PO get/set storage functions. Stateless, no business logic other than table lookups.
/// @dev Offers a table-like api over the key-value-pair eternal storage.
contract PoStorage is IPoStorage, Ownable, Bindable, StringConvertible
{
    // PO record field names
    // Header
    string constant private PO_NUMBER = "poNumber";
    string constant private BUYER_ADDRESS = "buyerAddress";
    string constant private BUYER_WALLET_ADDRESS = "buyerWalletAddress";
    string constant private BUYER_NONCE = "buyerNonce";
    string constant private SELLER_SYS_ID = "sellerSysId";
    string constant private PO_CREATE_DATE = "poCreateDate";

    // Line Items
    string constant private PO_ITEM_NUMBER = "poItemNumber";
    string constant private SO_NUMBER = "soNumber";
    string constant private SO_ITEM_NUMBER = "soItemNumber";
    string constant private PRODUCT_ID = "productId";
    string constant private QUANTITY = "quantity";
    string constant private UNIT = "unit";
    string constant private QUANTITY_ERC20_SYMBOL = "quantityErc20Symbol";
    string constant private QUANTITY_ERC20_ADDRESS = "quantityErc20Address";
    string constant private VALUE = "value";
    string constant private CURRENCY_ERC20_SYMBOL = "currencyErc20Symbol";
    string constant private CURRENCY_ERC20_ADDRESS = "currencyErc20Address";
    string constant private STATUS = "status";
    string constant private GOODS_ISSUE_DATE = "goodsIssueDate";
    string constant private ESCROW_RELEASE_DATE = "escrowReleaseDate";
    string constant private CANCEL_STATUS = "cancelStatus";
    
    // Number range field names
    string constant private PO_GLOBAL_NUMBER = "po.global.number";

    // Names of mappings in eternal storage
    // Mapping from a buyer address (type address) to their current nonce (uint)
    string constant private MAP_ADDRESS_TO_NONCE = "mapAddressToNonce";

    IEternalStorage public eternalStorage;
    IAddressRegistry public addressRegistry;

    constructor (address contractAddressOfRegistry) public payable
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

    function getCurrentNonce(address a) onlyRegisteredCaller() override public view returns (uint nonce)
    {
        nonce = eternalStorage.getMappingAddressToUint256Value(stringToBytes32(MAP_ADDRESS_TO_NONCE), a);
    }

    function getCurrentPoNumber() onlyRegisteredCaller() override public view returns (uint poNumber)
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
    function getPoNumberByBuyerAddressAndNonce(address a, uint nonce) override public view returns (uint poNumber)
    {
       return 4;   
    }
    
    //function getPoByBuyerPoNumber(bytes32 buyerSystemId, bytes32 buyerPoNumber) override public view returns (IPoTypes.Po memory po)
    //{
    //    //// Use mapping index to get eth PO number from hash(buyer id + buyer po number)
    //    //bytes32 mappingKey = keccak256(abi.encodePacked(buyerSystemId, buyerPoNumber));
    //    //uint64 ethPoNumber = uint64(eternalStorage.getMappingBytes32ToUint256Value(stringToBytes32(MAP_BUYER_PO_TO_ETH_PO), mappingKey));
    //
    //        // Retrieve  PO
    //    //po = getPoByEthPoNumber(ethPoNumber);
    //}

    function getPo(uint poNumber) override public view returns (IPoTypes.Po memory po)
    {
        //po = IPoTypes.Po
        //({
            /*
            ethPurchaseOrderNumber: uint64(eternalStorage.getUint256Value(keccak256(abi.encodePacked(ethPoNumber, ETH_PO_NUMBER)))),

            buyerSysId: eternalStorage.getBytes32Value(keccak256(abi.encodePacked(ethPoNumber, BUYER_SYS_ID))),
            buyerPurchaseOrderNumber: eternalStorage.getBytes32Value(keccak256(abi.encodePacked(ethPoNumber, BUYER_PO_NUMBER))),
            buyerViewVendorId: eternalStorage.getBytes32Value(keccak256(abi.encodePacked(ethPoNumber, BUYER_VIEW_VENDOR_ID))),

            sellerSysId: eternalStorage.getBytes32Value(keccak256(abi.encodePacked(ethPoNumber, SELLER_SYS_ID))),
            sellerSalesOrderNumber: eternalStorage.getBytes32Value(keccak256(abi.encodePacked(ethPoNumber, SELLER_SO_NUMBER))),
            sellerViewCustomerId: eternalStorage.getBytes32Value(keccak256(abi.encodePacked(ethPoNumber, SELLER_VIEW_CUSTOMER_ID))),

            buyerProductId: eternalStorage.getBytes32Value(keccak256(abi.encodePacked(ethPoNumber, BUYER_PRODUCT_ID))),
            sellerProductId: eternalStorage.getBytes32Value(keccak256(abi.encodePacked(ethPoNumber, SELLER_PRODUCT_ID))),

            currency: eternalStorage.getBytes32Value(keccak256(abi.encodePacked(ethPoNumber, CURRENCY))),
            currencyAddress: eternalStorage.getAddressValue(keccak256(abi.encodePacked(ethPoNumber, CURRENCY_ADDRESS))),
            totalQuantity: uint32(eternalStorage.getUint256Value(keccak256(abi.encodePacked(ethPoNumber, TOTAL_QUANTITY)))),
            totalValue: uint32(eternalStorage.getUint256Value(keccak256(abi.encodePacked(ethPoNumber, TOTAL_VALUE)))),

            openInvoiceQuantity: uint32(eternalStorage.getUint256Value(keccak256(abi.encodePacked(ethPoNumber, OPEN_INVOICE_QUANTITY)))),
            openInvoiceValue: uint32(eternalStorage.getUint256Value(keccak256(abi.encodePacked(ethPoNumber, OPEN_INVOICE_VALUE)))),

            poStatus: IPoTypes.PoStatus(eternalStorage.getUint256Value(keccak256(abi.encodePacked(ethPoNumber, PO_STATUS)))),
            wiProcessStatus: IPoTypes.WiProcessStatus(eternalStorage.getUint256Value(keccak256(abi.encodePacked(ethPoNumber, WI_PROCESS_STATUS))))
            */
        //});
    }

    function setPo(IPoTypes.Po memory po) override public
    {
        // Update main PO storage
        // Header
        
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(po.poNumber, PO_NUMBER)), po.poNumber);

        /*
        eternalStorage.setBytes32Value(keccak256(abi.encodePacked(po.ethPurchaseOrderNumber, BUYER_SYS_ID)), po.buyerSysId);
        eternalStorage.setBytes32Value(keccak256(abi.encodePacked(po.ethPurchaseOrderNumber, BUYER_PO_NUMBER)), po.buyerPurchaseOrderNumber);
        eternalStorage.setBytes32Value(keccak256(abi.encodePacked(po.ethPurchaseOrderNumber, BUYER_VIEW_VENDOR_ID)), po.buyerViewVendorId);

        eternalStorage.setBytes32Value(keccak256(abi.encodePacked(po.ethPurchaseOrderNumber, SELLER_SYS_ID)), po.sellerSysId);
        eternalStorage.setBytes32Value(keccak256(abi.encodePacked(po.ethPurchaseOrderNumber, SELLER_SO_NUMBER)), po.sellerSalesOrderNumber);
        eternalStorage.setBytes32Value(keccak256(abi.encodePacked(po.ethPurchaseOrderNumber, SELLER_VIEW_CUSTOMER_ID)), po.sellerViewCustomerId);

        eternalStorage.setBytes32Value(keccak256(abi.encodePacked(po.ethPurchaseOrderNumber, BUYER_PRODUCT_ID)), po.buyerProductId);
        eternalStorage.setBytes32Value(keccak256(abi.encodePacked(po.ethPurchaseOrderNumber, SELLER_PRODUCT_ID)), po.sellerProductId);

        eternalStorage.setBytes32Value(keccak256(abi.encodePacked(po.ethPurchaseOrderNumber, CURRENCY)), po.currency);
        eternalStorage.setAddressValue(keccak256(abi.encodePacked(po.ethPurchaseOrderNumber, CURRENCY_ADDRESS)), po.currencyAddress);
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(po.ethPurchaseOrderNumber, TOTAL_QUANTITY)), po.totalQuantity);
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(po.ethPurchaseOrderNumber, TOTAL_VALUE)), po.totalValue);

        eternalStorage.setUint256Value(keccak256(abi.encodePacked(po.ethPurchaseOrderNumber, OPEN_INVOICE_QUANTITY)), po.openInvoiceQuantity);
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(po.ethPurchaseOrderNumber, OPEN_INVOICE_VALUE)), po.openInvoiceValue);

        eternalStorage.setUint256Value(keccak256(abi.encodePacked(po.ethPurchaseOrderNumber, PO_STATUS)), uint256(po.poStatus));
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(po.ethPurchaseOrderNumber, WI_PROCESS_STATUS)), uint256(po.wiProcessStatus));
        */
        
        // Line Items
        uint len = po.poItems.length;
        for (uint i = 0; i < len; i++)
        {
            bytes32 lineItemKey = keccak256(abi.encodePacked(po.poNumber, po.poItems[i].poItemNumber));
            eternalStorage.setUint256Value(lineItemKey, po.poItems[i].poItemNumber);
            
        }
        
        
        
        /*
        // Update mappings (indexes)
        bytes32 mappingKey = keccak256(abi.encodePacked(po.buyerSysId, po.buyerPurchaseOrderNumber));
        eternalStorage.setMappingBytes32ToUint256Value(stringToBytes32(MAP_BUYER_PO_TO_ETH_PO), mappingKey,
            uint256(po.ethPurchaseOrderNumber));
        */
    }
}

