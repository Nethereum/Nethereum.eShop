pragma solidity ^0.6.1;

interface IBusinessPartnerStorage
{
    function configure(string calldata nameOfEternalStorage) external;

    // Map a System ID to a wallet address and a description
    function getWalletAddress(bytes32 systemId) external view returns (address walletAddress);
    function setWalletAddress(bytes32 systemId, address walletAddress) external;
    function getSystemDescription(bytes32 systemId) external view returns (bytes32 systemDescription);
    function setSystemDescription(bytes32 systemId, bytes32 systemDescription) external;
}