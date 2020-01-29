pragma solidity ^0.5.3;
pragma experimental ABIEncoderV2;

import "./WalletBase.sol";
import "./IWalletSeller.sol";

/// @title WalletSeller
/// @notice Store funds for seller, pass fn calls to PoMain
contract WalletSeller is WalletBase, IWalletSeller
{
    constructor (address contractAddressOfRegistry) WalletBase(contractAddressOfRegistry) public payable {}
    
    event PurchaseRaisedOkLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event PurchaseCancelRequestedOkLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    
    function onCreatePurchaseOrderRequested(IPoTypes.Po calldata po) external
    {
        emit PurchaseRaisedOkLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);    
    }
    
    function onCancelPurchaseOrderRequested(IPoTypes.Po calldata po) external
    {
        emit PurchaseCancelRequestedOkLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
    }
    
    /// @dev Pass through to PO Main
    function setSalesOrderNumberByEthPoNumber(uint64 ethPoNumber, bytes32 sellerSalesOrderNumber) external
    {
       poMain.setSalesOrderNumberByEthPoNumber(ethPoNumber, systemId, sellerSalesOrderNumber);
    }
    
    /// @dev Pass through to PO Main    
    function refundPoToBuyer(uint64 ethPoNumber) external
    {
       poMain.refundPoToBuyer(ethPoNumber); 
    }
    
    /// @dev Pass through to PO Main    
    function releasePoFundsToSeller(uint64 ethPoNumber) external
    {
        poMain.releasePoFundsToSeller(ethPoNumber);
    }
    
    /// @dev Pass through to PO Main 
    function reportSalesOrderNotApproved(uint64 ethPoNumber) external
    {
        poMain.reportSalesOrderNotApproved(ethPoNumber);
    }
    
    /// @dev Pass through to PO Main 
    function reportSalesOrderCancelFailure(uint64 ethPoNumber) external
    {
        poMain.reportSalesOrderCancelFailure(ethPoNumber);
    }
    
    /// @dev Pass through to PO Main 
    function reportSalesOrderInvoiceFault(uint64 ethPoNumber) external
    {
        poMain.reportSalesOrderInvoiceFault(ethPoNumber);
    }
}
