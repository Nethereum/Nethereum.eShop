pragma solidity ^0.6.1;

interface IFunding
{
    function configure(string calldata nameOfPurchasing, string calldata nameOfBusinessPartnerStorage) external;
    function transferInFundsForPoFromBuyerWallet(uint poNumber) external;
    
    //function transferOutFundsForPoItemToSeller(uint poNumber, uint8 poItemNumber) external;
    //function transferOutFundsForPoItemToBuyer(uint poNumber,uint8 poItemNumber) external;
    //function getBalanceOfThis(address tokenAddress) external view returns (uint balance);
}