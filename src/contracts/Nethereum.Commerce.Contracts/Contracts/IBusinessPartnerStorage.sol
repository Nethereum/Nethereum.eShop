pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

interface IBusinessPartnerStorage
{
    function configure(string calldata nameOfEternalStorage) external;

    // Maintain eShop information
    function getEshop(bytes32 eShopId) external view returns (IPoTypes.Eshop memory eShop);
    function setEshop(IPoTypes.Eshop calldata eShop) external;
    
    // Maintain seller information
    function getSeller(bytes32 sellerId) external view returns (IPoTypes.Seller memory seller);
    function setSeller(IPoTypes.Seller calldata seller) external;
}