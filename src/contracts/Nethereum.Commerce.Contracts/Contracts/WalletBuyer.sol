pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IWalletBuyer.sol";
import "./IAddressRegistry.sol";
import "./IPurchasing.sol";
import "./IFunding.sol";
import "./IErc20.sol";
import "./Ownable.sol";
import "./Bindable.sol";

/// @title WalletBuyer
contract WalletBuyer is IWalletBuyer, Ownable, Bindable
{
    IAddressRegistry public addressRegistry;
    IPurchasing public purchasing;
    IFunding public funding;

    constructor (address contractAddressOfRegistry) public
    {
        addressRegistry = IAddressRegistry(contractAddressOfRegistry);
    }
    
    // Contract setup
    function configure(string calldata nameOfPurchasing, string calldata nameOfFunding) onlyOwner() override external
    {
        // Purchasing contract
        purchasing = IPurchasing(addressRegistry.getAddressString(nameOfPurchasing));
        require(address(purchasing) != address(0), "Could not find Purchasing contract address in registry");

        // Funding contract
        funding = IFunding(addressRegistry.getAddressString(nameOfFunding));
        require(address(funding) != address(0), "Could not find Funding contract address in registry");
    }
    
    // Purchasing
    function getPo(uint poNumber) override external view returns (IPoTypes.Po memory po)
    {
        return purchasing.getPo(poNumber);
    }
    
    function getPoBySellerAndQuote(string calldata sellerIdString, uint quoteId) override external view returns (IPoTypes.Po memory po)
    {
        return purchasing.getPoBySellerAndQuote(sellerIdString, quoteId);
    }
    
    function createPurchaseOrder(IPoTypes.Po calldata po) override external
    {
        // Allow Funding contract to withdraw funds
        // Calculate total value
        uint totalValue = 0;
        uint itemCount = po.poItems.length;
        for (uint i = 0; i< itemCount; i++)
        {
            totalValue += po.poItems[i].currencyValue;
        }
        
        // NB: erc20.approve() is approving from THIS Wallet contract (not msg.sender)
        IErc20 tokenContract = IErc20(po.currencyAddress);
        tokenContract.approve(address(funding), totalValue);

        // Purchasing contract does the creation
        purchasing.createPurchaseOrder(po);
    }
    
    function cancelPurchaseOrderItem(uint poNumber, uint8 poItemNumber) override external
    {
        revert("Not implemented yet");
    }
    
    function setPoItemGoodsReceived(uint poNumber, uint8 poItemNumber) override external
    {
        purchasing.setPoItemGoodsReceivedBuyer(poNumber, poItemNumber);
    }
}

