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
    function configure(string calldata nameOfPurchasing, string calldata nameOfBusinessPartnerStorage) onlyOwner() override external
    {
        // Business partner storage
        businessPartnerStorage = IBusinessPartnerStorage(addressRegistry.getAddressString(nameOfBusinessPartnerStorage));
        require(address(businessPartnerStorage) != address(0), "Could not find BusinessPartnerStorage address in registry");

        // Address of the PO Main contract
        purchasingContractAddress = addressRegistry.getAddressString(nameOfPurchasing);
        purchasing = IPurchasing(purchasingContractAddress);
        require(address(purchasingContractAddress) != address(0), "Could not find Purchasing address in registry");
    }
    
    function transferInFundsForPoFromBuyerWallet(uint poNumber) onlyRegisteredCaller() override external
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
    
    function transferOutFundsForPoItemToBuyer(uint poNumber, uint8 poItemNumber) onlyRegisteredCaller() override external
    {
        // Refund to the PO buyer address on the PO header (NB: not the PO buyer wallet)
        IPoTypes.Po memory po = purchasing.getPo(poNumber);
        uint poItemIndex = poItemNumber - 1;
        uint poItemValue = po.poItems[poItemIndex].currencyValue;
        IErc20 token = IErc20(po.currencyAddress);
        require(po.buyerAddress != address(0), "PO has no buyer address");
        
        // Transfer
        bool result = token.transfer(po.buyerAddress, poItemValue);
        require(result == true, "Not enough funds transferred");
    }
    
    function transferOutFundsForPoItemToSeller(uint poNumber, uint8 poItemNumber) onlyRegisteredCaller() override external
    {
        // Pay the seller wallet
        IPoTypes.Po memory po = purchasing.getPo(poNumber);
        uint poItemIndex = poItemNumber - 1;
        uint poItemValue = po.poItems[poItemIndex].currencyValue;
        IErc20 token = IErc20(po.currencyAddress);
        IPoTypes.Seller memory seller = businessPartnerStorage.getSeller(po.sellerId);
        require(seller.contractAddress != address(0), "Seller Id has no contract address");
        
        // Transfer
        bool result = token.transfer(seller.contractAddress, poItemValue);
        require(result == true, "Not enough funds transferred");
    }
}