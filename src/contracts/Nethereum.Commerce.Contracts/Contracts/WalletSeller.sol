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
    function configure(string calldata sellerIdString, string calldata nameOfPurchasing, string calldata nameOfFunding) override external
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
    
    function setPoItemAccepted(uint poNumber, uint8 poItemNumber, bytes32 soNumber, bytes32 soItemNumber) override external
    {}
    
    function setPoItemRejected(uint poNumber, uint8 poItemNumber) override external
    {}
    
    function setPoItemReadyForGoodsIssue(uint poNumber, uint8 poItemNumber) override external
    {}
    
    function setPoItemGoodsIssued(uint poNumber, uint8 poItemNumber) override external
    {}
    
    function setPoItemGoodsReceived(uint poNumber, uint8 poItemNumber) override external
    {}
}

