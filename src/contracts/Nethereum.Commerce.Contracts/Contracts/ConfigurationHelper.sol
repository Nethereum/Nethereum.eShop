pragma solidity ^0.5.3;
pragma experimental ABIEncoderV2;

import "./IAddressRegistry.sol";
import "./IErc20.sol";
import "./IPoMain.sol";
import "./IFunding.sol";
import "./IWalletBase.sol";
import "./IBusinessPartnerStorage.sol";
import "./StringLib.sol";
import "./Debuggable.sol";

/// @notice Helper contract for reconfiguring application layer after a redeployment
contract ConfigurationHelper is Debuggable
{
    IAddressRegistry public addressRegistry;
    IPoMain public poMain;
    IFunding public funding;
    IWalletBase public walletBuyer01;
    IWalletBase public walletBuyer02;
    IWalletBase public walletSeller01;
    IWalletBase public walletSeller02;
    IBusinessPartnerStorage public businessPartnerStorage;
    IErc20 public erc20;
    
    string constant private PO_MAIN = "PoMain";
    string constant private FUNDING = "Funding";

    string constant private PO_STORAGE = "PoStorage";
    string constant private PRODUCT_STORAGE = "ProductStorage";
    string constant private BUSINESS_PARTNER_STORAGE = "BusinessPartnerStorage";
    
    string constant private SYS_ID_AS_STRING_BUYER_01 = "OmniConsumerProducts";
    string constant private SYS_ID_AS_STRING_BUYER_02 = "CyberdyneSystems";
    string constant private SYS_ID_AS_STRING_SELLER_01 = "SoylentCorporation";
    string constant private SYS_ID_AS_STRING_SELLER_02 = "SoylentUk";
    
    // bytes32 representations of above
    bytes32 constant private SYS_ID_BUYER_01 = 0x4f6d6e69436f6e73756d657250726f6475637473000000000000000000000000;
    bytes32 constant private SYS_ID_BUYER_02 = 0x437962657264796e6553797374656d7300000000000000000000000000000000;
    bytes32 constant private SYS_ID_SELLER_01 = 0x536f796c656e74436f72706f726174696f6e0000000000000000000000000000;
    bytes32 constant private SYS_ID_SELLER_02 = 0x536f796c656e74556b0000000000000000000000000000000000000000000000;
    
    constructor (address contractAddressOfRegistry) public
    {
        addressRegistry = IAddressRegistry(contractAddressOfRegistry);
        
        // Default to showing debug messages
        isDebugOn = true;
    }
    
    function replaceAppLayer(address addressOfPoMain, address addressOfFunding,
        address addressOfWalletBuyer01,  address addressOfWalletBuyer02,
        address addressOfWalletSeller01, address addressOfWalletSeller02) public
    {
        // Step 1 - contract deployment, still done manually
        
        // Step 2 - replace address registry entries
        addressRegistry.registerAddressString(PO_MAIN, addressOfPoMain);
        addressRegistry.registerAddressString(FUNDING, addressOfFunding);
        
        // Step 3 - call configure on all new contracts
        // PoMain (get from registry which we just updated)
        poMain = IPoMain(addressRegistry.getAddressString(PO_MAIN));
        if (address(poMain) != address(0))
        {
            poMain.configure(PO_STORAGE, PRODUCT_STORAGE, BUSINESS_PARTNER_STORAGE, FUNDING);
        }
        else
        {
            logDebugString("Po main not found in registry");
        }

        // Funding (get from registry which we just updated)
        funding = IFunding(addressRegistry.getAddressString(FUNDING));
        if (address(funding) != address(0))
        {
            funding.configure(PO_STORAGE, PO_MAIN, BUSINESS_PARTNER_STORAGE);
        }
        else
        {
            logDebugString("Funding not found in registry");
        }
        
        // Wallets of Buyers
        if (addressOfWalletBuyer01 != address(0))
        {
            walletBuyer01 = IWalletBase(addressOfWalletBuyer01);
            walletBuyer01.configure(SYS_ID_AS_STRING_BUYER_01, PO_MAIN, FUNDING);
        }
        else
        {
            logDebugString("WalletBuyer01 not configured");
        }
        
        if (addressOfWalletBuyer02 != address(0))
        {
            walletBuyer02 = IWalletBase(addressOfWalletBuyer02);
            walletBuyer02.configure(SYS_ID_AS_STRING_BUYER_02, PO_MAIN, FUNDING);
        }
        else
        {
            logDebugString("WalletBuyer02 not configured");
        }
        
        // Wallets of Sellers
        if (addressOfWalletSeller01 != address(0))
        {
            walletSeller01 = IWalletBase(addressOfWalletSeller01);
            walletSeller01.configure(SYS_ID_AS_STRING_SELLER_01, PO_MAIN, FUNDING);
        }
        else
        {
            logDebugString("WalletSeller01 not configured");
        }
        
        if (addressOfWalletSeller02 != address(0))
        {
            walletSeller02 = IWalletBase(addressOfWalletSeller02);
            walletSeller02.configure(SYS_ID_AS_STRING_SELLER_02, PO_MAIN, FUNDING);
        }
        else
        {
            logDebugString("WalletSeller02 not configured");
        }
        
        // Step 4 - call business partner storage to update wallets addresses
        businessPartnerStorage = IBusinessPartnerStorage(addressRegistry.getAddressString(BUSINESS_PARTNER_STORAGE));
        if (address(businessPartnerStorage) != address(0))
        {
            businessPartnerStorage.setWalletAddress(SYS_ID_BUYER_01, addressOfWalletBuyer01);
            businessPartnerStorage.setWalletAddress(SYS_ID_BUYER_02, addressOfWalletBuyer02);
            businessPartnerStorage.setWalletAddress(SYS_ID_SELLER_01, addressOfWalletSeller01);
            businessPartnerStorage.setWalletAddress(SYS_ID_SELLER_02, addressOfWalletSeller02);
        }
        else
        {
            logDebugString("businessPartnerStorage not found in registry");
        }
        
        logDebugString("Completed replaceAppLayer");
    }

    //function transferTokensToWalletBuyer(address addressOfWalletBuyer, address addressOfTokens) public payable
    //{
    //    // Step 5 - transfer MOCKDAI to buyer wallet for spending
    //    erc20 = IErc20(addressOfTokens);
    //    erc20.transfer(addressOfWalletBuyer, 1000);
    //    
    //    logDebugString("Completed transfer tokens");
    //}
}
