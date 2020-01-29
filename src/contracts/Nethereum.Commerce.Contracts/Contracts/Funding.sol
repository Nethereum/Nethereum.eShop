pragma solidity ^0.5.3;
pragma experimental ABIEncoderV2;

import "./IAddressRegistry.sol";
import "./IErc20.sol";
import "./IFunding.sol";
import "./IPoStorage.sol";
import "./IBusinessPartnerStorage.sol";
import "./StringLib.sol";
import "./Debuggable.sol";

/// @title Funding main contract
/// @notice This contract address holds funding for all POs
/// @dev TODO investments can happen here
contract Funding is IFunding, Debuggable
{
    IAddressRegistry public addressRegistry;
    IPoStorage public poStorage;
    IBusinessPartnerStorage public businessPartnerStorage;

    address public poMainContractAddress;

    modifier onlyByPoMain()
    {
        require(
            msg.sender == poMainContractAddress,
             "Function must be called from PO Main"
        );
        _;
    }

    constructor (address contractAddressOfRegistry) public payable
    {
        addressRegistry = IAddressRegistry(contractAddressOfRegistry);
    }

    /// @notice Configure contract
    function configure(string memory nameOfPoStorage, string memory nameOfPoMain, string memory nameOfBusinessPartnerStorage) public
    {
        // Po Storage
        poStorage = IPoStorage(addressRegistry.getAddressString(nameOfPoStorage));
        require(address(poStorage) != address(0), "Could not find PoStorage address in registry");

        // Business partner storage
        businessPartnerStorage = IBusinessPartnerStorage(addressRegistry.getAddressString(nameOfBusinessPartnerStorage));
        require(address(businessPartnerStorage) != address(0), "Could not find BusinessPartnerStorage address in registry");

        // Address of the PO Main contract
        poMainContractAddress = addressRegistry.getAddressString(nameOfPoMain);
        require(address(poMainContractAddress) != address(0), "Could not find PoMain address in registry");
    }

    /// @notice Transfer In Funds for PO from the buyer wallet
    /// @dev Pulls funds in for a PO from the buyer wallet.
    /// @dev Currently expects whole PO value to have been authorised for transfer to Funding contract.
    /// @dev Expects caller to be PO Main.
    function transferInFundsForPoFromBuyer(uint64 poNumber) public onlyByPoMain
    {
        logDebugUint64("po number", poNumber);
        
        IPoTypes.Po memory po = poStorage.getPoByEthPoNumber(poNumber);
        IErc20 token = IErc20(po.currencyAddress);
        address buyerWalletAddress = businessPartnerStorage.getWalletAddress(po.buyerSysId);
        
        logDebugAddr("transfer from", buyerWalletAddress);
        logDebugAddr("spender", address(this));
        logDebugUint32("total value", po.totalValue);
        
        bool result = token.transferFrom(buyerWalletAddress, address(this), po.totalValue);
        //require(result == true, "Not enough funds transferred");
        if (result)
        {
            logDebugString("PO funded ok");
        }
        else
        {
            logDebugString("PO not funded");
        }

    }

    /// @notice Transfer Out Funds for PO to Seller
    /// @dev Pays whole PO value to the seller. Expects caller to be PO Main.
    function transferOutFundsForPoToSeller(uint64 poNumber) public onlyByPoMain
    {
        // Pay whole PO value to the seller's wallet
        IPoTypes.Po memory po = poStorage.getPoByEthPoNumber(poNumber);
        IErc20 token = IErc20(po.currencyAddress);
        address sellerWalletAddress = businessPartnerStorage.getWalletAddress(po.sellerSysId);
        
        bool result = token.transfer(sellerWalletAddress, po.totalValue);
        require(result == true, "Not enough funds transferred");
    }

    /// @notice Transfer Out Funds for PO to Buyer (ie a refund)
    /// @dev Pays whole PO value to the buyer. Expects caller to be PO Main.
    function transferOutFundsForPoToBuyer(uint64 poNumber) public onlyByPoMain
    {
        // Pay whole PO value to the buyer's wallet
        IPoTypes.Po memory po = poStorage.getPoByEthPoNumber(poNumber);
        IErc20 token = IErc20(po.currencyAddress);
        address buyerWalletAddress = businessPartnerStorage.getWalletAddress(po.buyerSysId);
        
        bool result = token.transfer(buyerWalletAddress, po.totalValue);
        require(result == true, "Not enough funds transferred");
    }

    /// @notice Get an ERC20 balance owned by this contract
    function getBalanceOfThis(address tokenAddress) public view returns (uint balance)
    {
        return IErc20(tokenAddress).balanceOf(address(this));
    }

    function getPoFundingStatus(uint64 poNumber) public view returns (bool isFullyFunded)
    {
        // TODO add IFundingStorage, FundingStorage and store mapping there of PO numbers => bool (MappingBytes32ToBoolStorage)
        return true;
    }
}