pragma solidity ^0.5.3;
pragma experimental ABIEncoderV2;

interface IWalletBase
{
    function configure(string calldata sysIdAsString, string calldata nameOfPoMain, string calldata nameOfFundingContract) external;
    function getTokenBalanceOwnedByMsgSender(address tokenAddress) external view returns (uint balanceOwnedByMsgSender);
    function getTokenBalanceOwnedByThis(address tokenAddress) external view returns (uint balanceOwnedByThis);
}

