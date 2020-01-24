pragma solidity ^0.5.3;
pragma experimental ABIEncoderV2;

import "./WalletBase.sol";
import "./IWalletBuyer.sol";

/// @title WalletBuyer
/// @notice UI facing purchasing component for buyer
contract WalletBuyer is WalletBase, IWalletBuyer
{
    // PO creation requested ok
    // WalletPurchaseLog has index choices intended to be buyer friendly
    event WalletPurchaseLog(bytes32 indexed buyerSysId, bytes32 indexed buyerPurchaseOrderNumber, bytes32 indexed buyerProductId, IPoTypes.Po po);
    event PurchaseRaisedOkLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    
    // PO cancellation requested ok
    event PurchaseCancelRequestedOkLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
        
    // Reponses
    event PurchaseUpdatedWithSalesOrderOkLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event PurchasePaymentMadeOkLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event PurchasePaymentFailedLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event PurchaseRefundMadeOkLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event PurchaseRefundFailedLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event SalesOrderCancelFailedLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event SalesOrderNotApprovedLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event SalesOrderInvoiceFaultLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    
    constructor (address contractAddressOfRegistry) WalletBase(contractAddressOfRegistry) public payable {}
    
    function createPurchaseOrderTest(address tokenAddress) public
    {
        IPoTypes.Po memory poTest;
        //poTest.ethPurchaseOrderNumber; during PO creation leave this initial, filled by PoMain

        poTest.buyerSysId = systemId;
        poTest.buyerPurchaseOrderNumber = StringLib.stringToBytes32("InAppPurchase123");
        //poTest.buyerViewVendorId; leave initial, filled by PoMain

        poTest.sellerSysId = StringLib.stringToBytes32("SoylentCorporation");
        //poTest.sellerSalesOrderNumber; leave initial, filled by PoMain
        //poTest.sellerViewCustomerId; leave initial, filled by PoMain

        poTest.buyerProductId = StringLib.stringToBytes32("BHT-1101");
        //poTest.sellerProductId; leave initial, filled by PoMain

        poTest.currency = StringLib.stringToBytes32("DAITEST");
        poTest.currencyAddress = tokenAddress;
        poTest.totalQuantity = 10;
        poTest.totalValue = 16;

        //poTest.openInvoiceQuantity; leave initial, filled by PoMain
        //poTest.openInvoiceValue; leave initial, filled by PoMain

        //poTest.poStatus; leave initial, filled by PoMain
        //poTest.wiProcessStatus; leave initial, filled by PoMain

        createPurchaseOrder(poTest);
    }

    function createPurchaseOrder(IPoTypes.Po memory po) public
    {
        // Allow Funding contract to withdraw funds, NB: this is approving from THIS Wallet contract (not msg.sender)
        IErc20 erc20Contract = IErc20(po.currencyAddress);
        erc20Contract.approve(address(fundingContract), po.totalValue);

        // PO main does the purchase
        bool success = poMain.createPurchaseOrder(po);
        if (success == true)
        {
            // Retrieve the actual PO that was sent to the ERP system
            IPoTypes.Po memory poAsSentToBuyer = poMain.getPoByBuyerPoNumber(po.buyerSysId, po.buyerPurchaseOrderNumber);
            emit WalletPurchaseLog(poAsSentToBuyer.buyerSysId, poAsSentToBuyer.buyerPurchaseOrderNumber, poAsSentToBuyer.buyerProductId, poAsSentToBuyer);
            emit PurchaseRaisedOkLog(poAsSentToBuyer.buyerSysId, poAsSentToBuyer.sellerSysId, poAsSentToBuyer.ethPurchaseOrderNumber, poAsSentToBuyer);
        }
    }
    
    function cancelPurchaseOrder(uint64 ethPoNumber) public
    {
        // PO main requests the cancelation
        bool success = poMain.cancelPurchaseOrder(ethPoNumber);
        if (success == true)
        {
            // Retrieve the PO
            IPoTypes.Po memory po = poMain.getPoByEthPoNumber(ethPoNumber);
            emit PurchaseCancelRequestedOkLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
        }
    }
    
    /// @dev only called by PoMain, this is a pass through event from PO main to the buyer wallet UI
    function onPurchaseUpdatedWithSalesOrder(IPoTypes.Po calldata po) onlyByPoMain external
    {
        emit PurchaseUpdatedWithSalesOrderOkLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
    }
    
    /// @dev only called by PoMain, this is a pass through event from PO main to the buyer wallet UI
    function onPurchasePaymentMadeOk(IPoTypes.Po calldata po) onlyByPoMain external
    {
        emit PurchasePaymentMadeOkLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
    }
    
    /// @dev only called by PoMain, this is a pass through event from PO main to the buyer wallet UI
    function onPurchasePaymentFailed(IPoTypes.Po calldata po) onlyByPoMain external
    {
        emit PurchasePaymentFailedLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
    }
    
    /// @dev only called by PoMain, this is a pass through event from PO main to the buyer wallet UI
    function onPurchaseRefundMadeOk(IPoTypes.Po calldata po) onlyByPoMain external
    {
        emit PurchaseRefundMadeOkLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
    }
    
    /// @dev only called by PoMain, this is a pass through event from PO main to the buyer wallet UI
    function onPurchaseRefundFailed(IPoTypes.Po calldata po) onlyByPoMain external
    {
        emit PurchaseRefundFailedLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
    }
    
    /// @dev only called by PoMain, this is a pass through event from PO main to the buyer wallet UI
    function onSalesOrderNotApproved(IPoTypes.Po calldata po) external
    {
        emit SalesOrderNotApprovedLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
    }
    
    /// @dev only called by PoMain, this is a pass through event from PO main to the buyer wallet UI
    function onSalesOrderCancelFailure(IPoTypes.Po calldata po) external
    {
        emit SalesOrderCancelFailedLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
    }
        
    /// @dev only called by PoMain, this is a pass through event from PO main to the buyer wallet UI
    function onSalesOrderInvoiceFault(IPoTypes.Po calldata po) external
    {
        emit SalesOrderInvoiceFaultLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
    }
}

