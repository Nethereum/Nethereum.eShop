pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";
import "./IAddressRegistry.sol";
import "./IErc20.sol";
import "./IFunding.sol";
import "./IBusinessPartnerStorage.sol";
import "./IPurchasing.sol";
import "./Ownable.sol";
import "./Bindable.sol";
import "./StringConvertible.sol";

/// @title Funding main contract
contract Funding is IFunding, Ownable, Bindable, StringConvertible
{
    IAddressRegistry public addressRegistry;
    IBusinessPartnerStorage public businessPartnerStorage;
    IPurchasing public purchasing;
    address public purchasingContractAddress;

    constructor (address contractAddressOfRegistry) public 
    {
        addressRegistry = IAddressRegistry(contractAddressOfRegistry);
    }

    /// @notice Configure contract
    function configure(string calldata nameOfPurchasing, string calldata nameOfBusinessPartnerStorage) override external
    {
        // Business partner storage
        businessPartnerStorage = IBusinessPartnerStorage(addressRegistry.getAddressString(nameOfBusinessPartnerStorage));
        require(address(businessPartnerStorage) != address(0), "Could not find BusinessPartnerStorage address in registry");

        // Address of the PO Main contract
        purchasingContractAddress = addressRegistry.getAddressString(nameOfPurchasing);
        purchasing = IPurchasing(purchasingContractAddress);
        require(address(purchasingContractAddress) != address(0), "Could not find Purchasing address in registry");
    }
    
    function transferInFundsForPoFromBuyerWallet(uint poNumber) override external
    {
        // Get total PO value
        IPoTypes.Po memory po = purchasing.getPo(poNumber);
        uint totalPoValue = 0;
        for (uint i = 0; i < po.poItemCount; i++)
        {
            totalPoValue += po.poItems[i].currencyValue;
        }
        
        // Do the pull of funds, from buyer wallet into this funding contract 
        IErc20 token = IErc20(po.currencyAddress);
        bool isTransferSuccessful = token.transferFrom(po.buyerWalletAddress, address(this), totalPoValue);
        require(isTransferSuccessful == true, "Insufficient funds transferred for PO");
    }
    
    //function transferOutFundsForPoItemToSeller(uint poNumber, uint8 poItemNumber) override external
    //{}
    
    //function transferOutFundsForPoItemToBuyer(uint poNumber,uint8 poItemNumber) override external
    //{}
    
    //function getBalanceOfThis(address tokenAddress) override external view returns (uint balance)
    //{}


/*
    /// @notice Transfer In Funds for PO from the buyer wallet
    /// @dev Pulls funds in for a PO from the buyer wallet.
    /// @dev Currently expects whole PO value to have been authorised for transfer to Funding contract.
    function transferInFundsForPoFromBuyer(uint64 poNumber) public 
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
    */
}