pragma solidity ^0.5.3;
pragma experimental ABIEncoderV2;

import "./IPoStorage.sol";
import "./IEternalStorage.sol";
import "./IAddressRegistry.sol";
import "./StringLib.sol";

/// @title Po Storage
/// @dev Wraps eternal storage, provides PO get/set storage functions. Stateless, no business logic other than table lookups.
/// @dev Offers a relation-db-like api over the key-value-pair eternal storage.
contract PoStorage is IPoStorage
{
    // PO record field names
    string constant private ETH_PO_NUMBER = "ethPurchaseOrderNumber";
    string constant private BUYER_SYS_ID = "buyerSysId";
    string constant private BUYER_PO_NUMBER = "buyerPurchaseOrderNumber";
    string constant private BUYER_VIEW_VENDOR_ID = "buyerViewVendorId";
    string constant private SELLER_SYS_ID = "sellerSysId";
    string constant private SELLER_SO_NUMBER = "sellerSalesOrderNumber";
    string constant private SELLER_VIEW_CUSTOMER_ID = "sellerViewCustomerId";
    string constant private BUYER_PRODUCT_ID = "buyerProductId";
    string constant private SELLER_PRODUCT_ID = "sellerProductId";
    string constant private CURRENCY = "currency";
    string constant private CURRENCY_ADDRESS = "currencyAddress";
    string constant private TOTAL_QUANTITY = "totalQuantity";
    string constant private TOTAL_VALUE = "totalValue";
    string constant private OPEN_INVOICE_QUANTITY = "openInvoiceQuantity";
    string constant private OPEN_INVOICE_VALUE = "openInvoiceValue";
    string constant private PO_STATUS = "poStatus";
    string constant private WI_PROCESS_STATUS = "wiProcessStatus";

    // Number range field names
    string constant private PO_GLOBAL_NUMBER = "po.global.number";

    // Mapping names
    /// @dev Name of a mapping in eternal storage.
    /// @dev This is used like an index, knowing hash(buyerid + buyerPO) return the EthPO number
    string constant private MAP_BUYER_PO_TO_ETH_PO = "mapBuyerPoToEthPO";

    IEternalStorage public eternalStorage;
    IAddressRegistry public addressRegistry;

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

    function getEternalStorageAddress() public view returns (address eternalStorageAddress)
    {
        return address(eternalStorage);
    }

    function getCurrentPoNumber() public view returns (uint64 poNumber)
    {
        return uint64(eternalStorage.getUint256Value(keccak256(abi.encodePacked(PO_GLOBAL_NUMBER))));
    }

    function getNextPoNumber() public returns (uint64 poNumber)
    {
        uint64 currentPoNumber = getCurrentPoNumber();
        currentPoNumber++;
        setCurrentPoNumber(currentPoNumber);
        return currentPoNumber;
    }

    function setCurrentPoNumber(uint64 poNumber) private
    {
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(PO_GLOBAL_NUMBER)), poNumber);
    }

    function getPoByBuyerPoNumber(bytes32 buyerSystemId, bytes32 buyerPoNumber) public view returns (IPoTypes.Po memory po)
    {
        // Use mapping index to get eth PO number from hash(buyer id + buyer po number)
        bytes32 mappingKey = keccak256(abi.encodePacked(buyerSystemId, buyerPoNumber));
        uint64 ethPoNumber = uint64(eternalStorage.getMappingBytes32ToUint256Value(StringLib.stringToBytes32(MAP_BUYER_PO_TO_ETH_PO), mappingKey));

        // Retrieve  PO
        po = getPoByEthPoNumber(ethPoNumber);
    }

    function getPoByEthPoNumber(uint64 ethPoNumber) public view returns (IPoTypes.Po memory po)
    {
        po = IPoTypes.Po
        ({
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
        });
    }

    function setPo(IPoTypes.Po memory po) public
    {
        // Update main PO storage
        eternalStorage.setUint256Value(keccak256(abi.encodePacked(po.ethPurchaseOrderNumber, ETH_PO_NUMBER)), po.ethPurchaseOrderNumber);

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

        // Update mappings (indexes)
        bytes32 mappingKey = keccak256(abi.encodePacked(po.buyerSysId, po.buyerPurchaseOrderNumber));
        eternalStorage.setMappingBytes32ToUint256Value(StringLib.stringToBytes32(MAP_BUYER_PO_TO_ETH_PO), mappingKey,
            uint256(po.ethPurchaseOrderNumber));
    }
}

