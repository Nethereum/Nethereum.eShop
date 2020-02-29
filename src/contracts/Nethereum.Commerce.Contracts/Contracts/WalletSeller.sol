pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IWalletSeller.sol";
import "./IAddressRegistry.sol";
import "./IPurchasing.sol";
import "./IFunding.sol";
import "./Ownable.sol";
import "./Bindable.sol";
import "./StringConvertible.sol";

/// @title WalletSeller
contract WalletSeller is IWalletSeller, Ownable, Bindable, StringConvertible
{
    IAddressRegistry public addressRegistry;
    IPurchasing public purchasing;
    IFunding public funding;
    bytes32 public sellerId;

    constructor (address contractAddressOfRegistry) public
    {
        addressRegistry = IAddressRegistry(contractAddressOfRegistry);
    }
    
    // Contract setup
    function configure(string calldata sellerIdString, string calldata nameOfPurchasing, string calldata nameOfFunding) onlyOwner() override external
    {
        sellerId = stringToBytes32(sellerIdString);
          
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
    
    function setPoItemAccepted(uint poNumber, uint8 poItemNumber, bytes32 soNumber, bytes32 soItemNumber) onlyOwner() override external
    {
        purchasing.setPoItemAccepted(poNumber, poItemNumber, soNumber, soItemNumber);
    }
    
    function setPoItemRejected(uint poNumber, uint8 poItemNumber) onlyOwner() override external
    {
        purchasing.setPoItemRejected(poNumber, poItemNumber);
    }
    
    function setPoItemReadyForGoodsIssue(uint poNumber, uint8 poItemNumber) onlyOwner() override external
    {
        purchasing.setPoItemReadyForGoodsIssue(poNumber, poItemNumber);
    }
    
    function setPoItemGoodsIssued(uint poNumber, uint8 poItemNumber) onlyOwner() override external
    {
        purchasing.setPoItemGoodsIssued(poNumber, poItemNumber);
    }
    
    function setPoItemGoodsReceived(uint poNumber, uint8 poItemNumber) onlyOwner() override external
    {
        purchasing.setPoItemGoodsReceivedSeller(poNumber, poItemNumber);
    }
    
    function setPoItemCompleted(uint poNumber, uint8 poItemNumber) onlyOwner() override external
    {
        purchasing.setPoItemCompleted(poNumber, poItemNumber);
    }
}

