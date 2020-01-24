pragma solidity ^0.5.3;
pragma experimental ABIEncoderV2;

import "./IPoMain.sol";
import "./IPoTypes.sol";
import "./IErc20.sol";
import "./IPoStorage.sol";
import "./IProductStorage.sol";
import "./IBusinessPartnerStorage.sol";
import "./IFunding.sol";
import "./IWalletBuyer.sol";
import "./IWalletSeller.sol";
import "./IAddressRegistry.sol";
import "./Debuggable.sol";

/// @title Purchasing Main Contract
/// @notice Create or change a purchase order, pulling funds as required (funds should be approved before calling here).
contract PoMain is IPoMain, Debuggable
{
    // Mainly Seller facing
    event PurchaseRaisedOkLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event PurchaseCancelRequestedOkLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);

    // Mainly Buyer facing
    event PurchaseCancelSucceededLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event PurchaseCancelFailedLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event PurchaseUpdatedWithSalesOrderOkLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event SalesOrderCancelFailedLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event SalesOrderNotApprovedLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event SalesOrderInvoiceFault(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    
    // Buyer and Seller facing payment related
    event PurchasePaymentMadeOkLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event PurchasePaymentFailedLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event PurchaseRefundMadeOkLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event PurchaseRefundFailedLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    
    // Debug
    event PurchaseDataInLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    event PurchaseOrderSnapshotLog(bytes32 indexed buyerSysId, bytes32 indexed sellerSysId, uint64 indexed ethPurchaseOrderNumber, IPoTypes.Po po);
    
    IPoStorage public poStorage;
    IProductStorage public productStorage;
    IBusinessPartnerStorage public businessPartnerStorage;
    IFunding public fundingContract;
    IAddressRegistry public addressRegistry;

    constructor (address contractAddressOfRegistry) public payable
    {
        addressRegistry = IAddressRegistry(contractAddressOfRegistry);
    }

    /// @title Configure contract
    /// @param nameOfPoStorage key of the entry in the address registry that holds the PO storage contract address
    /// @param nameOfProductStorage key of the entry in the address registry that holds the product storage contract address
    /// @param nameOfBusinessPartnerStorage key of the entry in the address registry that holds the business partner storage contract address
    /// @param nameOfFundingContract key of the entry in the address registry that holds the funding contract address
    function configure(string memory nameOfPoStorage, string memory nameOfProductStorage,
        string memory nameOfBusinessPartnerStorage, string memory nameOfFundingContract) public
    {
        // Po Storage
        poStorage = IPoStorage(addressRegistry.getAddressString(nameOfPoStorage));
        require(address(poStorage) != address(0), "Could not find PoStorage address in registry");

        // Product Storage
        productStorage = IProductStorage(addressRegistry.getAddressString(nameOfProductStorage));
        require(address(productStorage) != address(0), "Could not find ProductStorage address in registry");

        // Business Partner Storage
        businessPartnerStorage = IBusinessPartnerStorage(addressRegistry.getAddressString(nameOfBusinessPartnerStorage));
        require(address(businessPartnerStorage) != address(0), "Could not find BusinessPartnerStorage address in registry");

        // Funding Contract
        fundingContract = IFunding(addressRegistry.getAddressString(nameOfFundingContract));
        require(address(fundingContract) != address(0), "Could not find FundingContract address in registry");
    }

    /// @title Make a purchase as specified by po. Critical fields for this fn:
    /// @param po.currencyAddress: address of an ERC20 that has already approved us some funds
    /// @param po.totalValue: token quantity in its base integer unit
    /// @return requestSuccessful true means the request for creation was made successfully (not that the creation itself was successful, this is still unknown)
    /// @dev must be called by wallet of buyer system (only they have auths to raise a PO for their own system)
    function createPurchaseOrder(IPoTypes.Po memory po) public returns (bool requestSuccessful)
    {
        // Debug, record PO data exactly as we received it
        emit PurchaseDataInLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
        
        // Po Validation
        // Ensure buyer told us who they are
        //require(po.buyerSysId.length > 0, "Buyer Id must be specified");
        logDebugBytes32("Buyer id:", po.buyerSysId);

        // Ensure buyer really is who they say they are (tx must come from buyer wallet address)
        address buyerWalletShouldBe = businessPartnerStorage.getWalletAddress(po.buyerSysId);
        //require(msg.sender == buyerWalletShouldBe, "Function must be called from Buyer Wallet");
        logDebugBytes32("Buyer sys id:", po.buyerSysId);
        logDebugAddr("Buyer wallet should be:", buyerWalletShouldBe);
        logDebugAddr("msg.sender:", msg.sender);
        
        // Ensure buyer chose a seller
        //require(po.sellerSysId.length > 0, "Seller Id must be specified");
		logDebugBytes32("Seller id:", po.sellerSysId);
		// TODO validate product IDs here

        // Globally unique Eth PO number
        po.ethPurchaseOrderNumber = poStorage.getNextPoNumber();
        logDebugUint64("Next PO number:", po.ethPurchaseOrderNumber);

        // Map buyer system id to a customer number in the seller system
        po.sellerViewCustomerId = businessPartnerStorage.getSellerViewCustomerIdForBuyerSysId(po.buyerSysId, po.sellerSysId);
        logDebugBytes32("Customer id:", po.sellerViewCustomerId);

        // Map seller system id to a vendor number in the buyer system (may be needed if buyer system is ERP, returns initial value if not configured)
        po.buyerViewVendorId = businessPartnerStorage.getBuyerViewVendorIdForSellerSysId(po.buyerSysId, po.sellerSysId);
        logDebugBytes32("Vendor id:", po.buyerViewVendorId);
        
        // Map product id from buyer id to seller id
        po.sellerProductId = productStorage.getSellerProductIdForBuyerProductId(po.buyerSysId, po.buyerProductId, po.sellerSysId);
        logDebugBytes32("Seller Product id:", po.sellerProductId);
        
        // Qty value
        po.openInvoiceQuantity = po.totalQuantity;
        po.openInvoiceValue = po.totalValue;
        
        // po.tokenId = uint256(keccak256(abi.encodePacked(block.timestamp, block.difficulty, po.ethPurchaseOrderNumber, po.sellerProductId, po.totalQuantity)));
        
        // Statuses
        po.poStatus = IPoTypes.PoStatus.PurchaseOrderCreated;  // Contract controlled status
        po.wiProcessStatus = IPoTypes.WiProcessStatus.Empty;   // Mirror of work item process status from app

        // Store Po details in eternal storage
        poStorage.setPo(po);
        logDebugString("PO storage ok");
        
        // Funding. Here, the Funding contract attempts to pull in funds from buyer wallet
        fundingContract.transferInFundsForPoFromBuyer(po.ethPurchaseOrderNumber);
        logDebugString("Call funding ok");
        
        bool isFunded = fundingContract.getPoFundingStatus(po.ethPurchaseOrderNumber);
        //require(isFunded == true, "Insufficient funding for PO");
        if (isFunded)
        {
            logDebugString("PO funded ok");
        }
        else
        {
            logDebugString("PO not funded");
        }

        // Events
        requestSuccessful = true;
        emit PurchaseRaisedOkLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
        
        // Inform seller wallet
        address walletSellerAddress = businessPartnerStorage.getWalletAddress(po.sellerSysId);
        require(walletSellerAddress != address(0), "Could not find wallet seller address in business partner storage");
        IWalletSeller walletSeller = IWalletSeller(walletSellerAddress);
        walletSeller.onCreatePurchaseOrderRequested(po);
    }
    
    function cancelPurchaseOrder(uint64 ethPoNumber) external returns (bool requestSuccessful)
    {
        IPoTypes.Po memory po = poStorage.getPoByEthPoNumber(ethPoNumber);
           
        // Tx must come from buyer wallet address
        address buyerWalletShouldBe = businessPartnerStorage.getWalletAddress(po.buyerSysId);
        require(msg.sender == buyerWalletShouldBe, "Function must be called from Buyer Wallet");
        
        requestSuccessful = true;
        emit PurchaseCancelRequestedOkLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
        
        // Inform seller Wallet
        address walletSellerAddress = businessPartnerStorage.getWalletAddress(po.sellerSysId);
        require(walletSellerAddress != address(0), "Could not find wallet seller address in registry");
        IWalletSeller walletSeller = IWalletSeller(walletSellerAddress);
        walletSeller.onCancelPurchaseOrderRequested(po);
        
    }

    /// @dev Update the PO with the given sales order
    /// @dev must be called by wallet of seller system (only they have auths to add a sales order)
    function setSalesOrderNumberByEthPoNumber(uint64 ethPoNumber, bytes32 sellerSysId, bytes32 sellerSalesOrderNumber) public
    {
        IPoTypes.Po memory po = poStorage.getPoByEthPoNumber(ethPoNumber);

        // Ensure seller really is who they say they are (tx must come from seller wallet address)
        address sellerWalletShouldBe = businessPartnerStorage.getWalletAddress(po.sellerSysId);
        require(msg.sender == sellerWalletShouldBe, "Function must be called from Seller Wallet");

        po.sellerSysId = sellerSysId;
        po.sellerSalesOrderNumber = sellerSalesOrderNumber;
        po.poStatus = IPoTypes.PoStatus.SalesOrderNumberReceived;
        poStorage.setPo(po);
        emit PurchaseUpdatedWithSalesOrderOkLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
        
        // Inform buyer wallet
        address walletBuyerAddress = businessPartnerStorage.getWalletAddress(po.buyerSysId);
        require(walletBuyerAddress != address(0), "Could not find wallet buyer address in business partner storage");
        IWalletBuyer walletBuyer = IWalletBuyer(walletBuyerAddress);
        walletBuyer.onPurchaseUpdatedWithSalesOrder(po);
    }
    
    function getSalesOrderNumberByEthPoNumber(uint64 ethPoNumber) 
        public view returns (bytes32 sellerSysId, bytes32 sellerSalesOrderNumber)
    {
        IPoTypes.Po memory po = poStorage.getPoByEthPoNumber(ethPoNumber);
        return (po.sellerSysId, po.sellerSalesOrderNumber);
    }

    /// @dev pass-through to po storage
    function getSalesOrderNumberByBuyerPoNumber(bytes32 buyerSystemId, bytes32 buyerPoNumber)
        public view returns (bytes32 sellerSysId, bytes32 sellerSalesOrderNumber)
    {
        IPoTypes.Po memory po = poStorage.getPoByBuyerPoNumber(buyerSystemId, buyerPoNumber);
        return (po.sellerSysId, po.sellerSalesOrderNumber);
    }

    /// @dev pass-through to po storage
    function getPoByEthPoNumber(uint64 ethPoNumber) public view returns (IPoTypes.Po memory po)
    {
        return poStorage.getPoByEthPoNumber(ethPoNumber);
    }
    
    /// @dev pass-through to po storage
    function getPoByBuyerPoNumber(bytes32 buyerSystemId, bytes32 buyerPoNumber) public view returns (IPoTypes.Po memory po)
    {
        return poStorage.getPoByBuyerPoNumber(buyerSystemId, buyerPoNumber);
    }
    
    /// @dev writes a snapshot of the PO to event log, for debugging 
    function writePoToEventLog(uint64 ethPoNumber) external
    {
        IPoTypes.Po memory po = poStorage.getPoByEthPoNumber(ethPoNumber);
        emit PurchaseOrderSnapshotLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
    }
    
    /// @dev pass-through to po storage
    function getLatestPoNumber() external view returns (uint64 poNumber)
    {
        return poStorage.getCurrentPoNumber();
    }

    /// @dev must be called by wallet of seller system (only they have auths to refund money)
    function refundPoToBuyer(uint64 ethPoNumber) public
    {
        IPoTypes.Po memory po = poStorage.getPoByEthPoNumber(ethPoNumber);

        // Ensure seller really is who they say they are (tx must come from seller wallet address)
        address sellerWalletShouldBe = businessPartnerStorage.getWalletAddress(po.sellerSysId);
        require(msg.sender == sellerWalletShouldBe, "Function must be called from Seller Wallet");

        // Ensure PO is in suitable state
        require(po.poStatus != IPoTypes.PoStatus.Completed, "PO must not be completed");
        require(po.poStatus != IPoTypes.PoStatus.Cancelled, "PO must not be cancelled");
           
        // Prepare to inform buyer wallet of result
        address walletBuyerAddress = businessPartnerStorage.getWalletAddress(po.buyerSysId);
        IWalletBuyer walletBuyer = IWalletBuyer(walletBuyerAddress);
        
        // Refund
        bool isFunded = fundingContract.getPoFundingStatus(po.ethPurchaseOrderNumber);
        if (isFunded)
        {
            fundingContract.transferOutFundsForPoToBuyer(po.ethPurchaseOrderNumber);
            po.poStatus = IPoTypes.PoStatus.Cancelled;
            poStorage.setPo(po);
        
            emit PurchaseRefundMadeOkLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
            walletBuyer.onPurchaseRefundMadeOk(po);
        }
        else
        {
            // Cannot refund to buyer
            emit PurchaseRefundFailedLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
            walletBuyer.onPurchaseRefundFailed(po);
        }
    }

    /// @dev must be called by wallet of seller system
    function releasePoFundsToSeller(uint64 ethPoNumber) public
    {
        IPoTypes.Po memory po = poStorage.getPoByEthPoNumber(ethPoNumber);

        // Ensure seller really is who they say they are (tx must come from seller wallet address)
        address sellerWalletShouldBe = businessPartnerStorage.getWalletAddress(po.sellerSysId);
        require(msg.sender == sellerWalletShouldBe, "Function must be called from Seller Wallet");

        // Ensure PO is in suitable state
        require(po.poStatus == IPoTypes.PoStatus.SalesOrderNumberReceived, "PO must have SO");
        
        // Prepare to inform buyer wallet of result
        address walletBuyerAddress = businessPartnerStorage.getWalletAddress(po.buyerSysId);
        IWalletBuyer walletBuyer = IWalletBuyer(walletBuyerAddress);
        
        // Try to release the funds
        bool isFunded = fundingContract.getPoFundingStatus(po.ethPurchaseOrderNumber);
        if (isFunded)
        {
            fundingContract.transferOutFundsForPoToSeller(po.ethPurchaseOrderNumber);
            po.poStatus = IPoTypes.PoStatus.Completed;
            poStorage.setPo(po);
        
            emit PurchasePaymentMadeOkLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
            walletBuyer.onPurchasePaymentMadeOk(po);
        }
        else
        {
            // Cannot release funds to seller
            emit PurchasePaymentFailedLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
            walletBuyer.onPurchasePaymentFailed(po);
        }
    }
    
    function reportSalesOrderNotApproved(uint64 ethPoNumber) external
    {
        IPoTypes.Po memory po = poStorage.getPoByEthPoNumber(ethPoNumber);
        
         // Ensure seller really is who they say they are (tx must come from seller wallet address)
        address sellerWalletShouldBe = businessPartnerStorage.getWalletAddress(po.sellerSysId);
        require(msg.sender == sellerWalletShouldBe, "Function must be called from Seller Wallet");
        
        emit SalesOrderNotApprovedLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
                            
        // Inform buyer wallet
        address walletBuyerAddress = businessPartnerStorage.getWalletAddress(po.buyerSysId);
        IWalletBuyer walletBuyer = IWalletBuyer(walletBuyerAddress);
        walletBuyer.onSalesOrderNotApproved(po);
 
    }
    
    function reportSalesOrderCancelFailure(uint64 ethPoNumber) external
    {
        IPoTypes.Po memory po = poStorage.getPoByEthPoNumber(ethPoNumber);
        
         // Ensure seller really is who they say they are (tx must come from seller wallet address)
        address sellerWalletShouldBe = businessPartnerStorage.getWalletAddress(po.sellerSysId);
        require(msg.sender == sellerWalletShouldBe, "Function must be called from Seller Wallet");
        
        emit SalesOrderCancelFailedLog(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
        
        // Inform buyer wallet
        address walletBuyerAddress = businessPartnerStorage.getWalletAddress(po.buyerSysId);
        IWalletBuyer walletBuyer = IWalletBuyer(walletBuyerAddress);
        walletBuyer.onSalesOrderCancelFailure(po);
    }
    
    function reportSalesOrderInvoiceFault(uint64 ethPoNumber) external
    {
        IPoTypes.Po memory po = poStorage.getPoByEthPoNumber(ethPoNumber);
        
         // Ensure seller really is who they say they are (tx must come from seller wallet address)
        address sellerWalletShouldBe = businessPartnerStorage.getWalletAddress(po.sellerSysId);
        require(msg.sender == sellerWalletShouldBe, "Function must be called from Seller Wallet");
        
        emit SalesOrderInvoiceFault(po.buyerSysId, po.sellerSysId, po.ethPurchaseOrderNumber, po);
        
        // Inform buyer wallet
        address walletBuyerAddress = businessPartnerStorage.getWalletAddress(po.buyerSysId);
        IWalletBuyer walletBuyer = IWalletBuyer(walletBuyerAddress);
        walletBuyer.onSalesOrderInvoiceFault(po);
    }
}

