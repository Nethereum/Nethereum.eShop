pragma solidity ^0.5.3;
pragma experimental ABIEncoderV2;

import "./IAddressRegistry.sol";
import "./IErc20.sol";
import "./IPoMain.sol";
import "./IFunding.sol";
import "./StringLib.sol";

/// @title WalletBase
/// @notice Wallet abstract base contract for buyer, seller wallets.
/// @notice A wallet is associated with a single system id.
contract WalletBase
{
    IPoMain public poMain;
    IAddressRegistry public addressRegistry;
    IFunding public fundingContract;
    bytes32 public systemId;

    modifier onlyByPoMain()
    {
        require(
            msg.sender == address(poMain),
             "Function must be called from PO Main"
        );
        _;
    }
    
    /// @dev note internal constructor makes this contract abstract
    constructor (address contractAddressOfRegistry) internal payable
    {
        addressRegistry = IAddressRegistry(contractAddressOfRegistry);
    }

    /// Configure contract
    /// @param sysId the system id that this wallet belongs to (as human readable string)
    /// @param nameOfPoMain key of the entry in the address registry that holds the PO main contract address
    /// @param nameOfFundingContract key of the entry in the address registry that holds the funding contract address
    function configure(string memory sysIdAsString, string memory nameOfPoMain, string memory nameOfFundingContract) public
    {
        // Global system identifier
        systemId = StringLib.stringToBytes32(sysIdAsString);

        // PO main contract
        poMain = IPoMain(addressRegistry.getAddressString(nameOfPoMain));
        require(address(poMain) != address(0), "Could not find PoMain address in registry");

        // Funding Contract
        fundingContract = IFunding(addressRegistry.getAddressString(nameOfFundingContract));
        require(address(fundingContract) != address(0), "Could not find FundingContract address in registry");
    }

    function getTokenBalanceOwnedByMsgSender(address tokenAddress) public view returns (uint balanceOwnedByMsgSender)
    {
        IErc20 erc20Contract = IErc20(tokenAddress);
        return erc20Contract.balanceOf(address(msg.sender));
    }

    function getTokenBalanceOwnedByThis(address tokenAddress) public view returns (uint balanceOwnedByThis)
    {
        IErc20 erc20Contract = IErc20(tokenAddress);
        return erc20Contract.balanceOf(address(this));
    }
}

