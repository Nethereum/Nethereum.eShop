pragma solidity ^0.6.1;
//pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

interface IFunding
{
    function configure(string calldata nameOfPoStorage, string calldata nameOfPoMain, string calldata nameOfBusinessPartnerStorage) external;
        
    function transferInFundsForPoFromBuyer(uint64 poNumber) external;
    function transferOutFundsForPoToSeller(uint64 poNumber) external;
    function transferOutFundsForPoToBuyer(uint64 poNumber) external;

    function getBalanceOfThis(address tokenAddress) external view returns (uint balance);
    function getPoFundingStatus(uint64 poNumber) external view returns (bool isFullyFunded);
}