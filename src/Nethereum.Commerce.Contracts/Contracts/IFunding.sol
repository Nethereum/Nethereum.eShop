pragma solidity ^0.5.3;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

contract IFunding
{
    function configure(string calldata nameOfPoStorage, string calldata nameOfPoMain, string calldata nameOfBusinessPartnerStorage) external;
        
    function transferInFundsForPoFromBuyer(uint64 poNumber) public;
    function transferOutFundsForPoToSeller(uint64 poNumber) public;
    function transferOutFundsForPoToBuyer(uint64 poNumber) public;

    function getBalanceOfThis(address tokenAddress) public view returns (uint balance);
    function getPoFundingStatus(uint64 poNumber) public view returns (bool isFullyFunded);
}