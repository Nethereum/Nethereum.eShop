pragma solidity ^0.5.3;
pragma experimental ABIEncoderV2;

contract IBusinessPartnerStorage
{
    function configure(string memory nameOfEternalStorage) public;

    // Mappings of business partners (customer, vendor ids in erp systems)
    // Get customer id that will be used on the Sales Order in the seller system (required)
    function getSellerViewCustomerIdForBuyerSysId(bytes32 buyerSystemId, bytes32 sellerSystemId) external view returns (bytes32 sellerViewCustomerId);
    function setSellerViewCustomerIdForBuyerSysId(bytes32 buyerSystemId, bytes32 sellerSystemId, bytes32 sellerViewCustomerId) external;

    // Get vendor id that will be used on the Purchase Order in the buyer system (optional)
    // Only needed if buyer system is an ERP-like system itself and needs a vendor number for its PO
    function getBuyerViewVendorIdForSellerSysId(bytes32 buyerSystemId, bytes32 sellerSystemId) external view returns (bytes32 buyerViewVendorId);
    function setBuyerViewVendorIdForSellerSysId(bytes32 buyerSystemId, bytes32 sellerSystemId, bytes32 buyerViewVendorId) external;

    // Map a System ID to a wallet address and a description
    function getWalletAddress(bytes32 systemId) external view returns (address walletAddress);
    function setWalletAddress(bytes32 systemId, address walletAddress) external;
    function getSystemDescription(bytes32 systemId) external view returns (bytes32 systemDescription);
    function setSystemDescription(bytes32 systemId, bytes32 systemDescription) external;
}