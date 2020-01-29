pragma solidity ^0.6.1;

import "./Ownable.sol";

/**
 * @dev Contract module which provides an access control mechanism that 
 * extends Ownable. In addition to an owner, further addresses can be
 * bound to a Bindable contract, and the modifier onlyRegisteredCaller used
 * to see if caller is permitted. This module is used through inheritance.
 * Intended use:
 *   One Owner is the admin who can control what addresses can be bound.
 *   Many Bound addresses can call the Bindable contract.
 * In all cases msg.sender (the immediate tx sender in the chain) is the
 * address being checked.
 */
contract Bindable is Ownable
{
    event AddressBound(address indexed a);
    event AddressUnBound(address indexed a);
    event AddressAlreadyBound(address indexed a);
    event AddressAlreadyUnBound(address indexed a);
    mapping(address => bool) public BoundAddresses;
    int public BoundAddressCount;
 
    /// @dev Modifier throws if sender is not owner and not a bound contract address
    modifier onlyRegisteredCaller()
    {
        if (msg.sender == owner() || BoundAddresses[msg.sender] )
        {
           _;
        }
        else
        {
            revert("Only contract owner or a bound address may call this function.");
        }
    }

    /// @dev Binds the eternal storage contract to a specific address
    /// @param a the contract address to attach to
    function bindAddress(address a) onlyOwner() public
    {
        if (BoundAddresses[a])
        {
            emit AddressAlreadyBound(a);
        }
        else
        {
            BoundAddresses[a] = true;
            BoundAddressCount++;
            emit AddressBound(a);
        }
    }

    /// @dev Un-binds the eternal storage contract from the specified address
    function unBindAddress(address a) onlyOwner() public
    {
        if (BoundAddresses[a])
        {
            BoundAddresses[a] = false;
            BoundAddressCount--;
            emit AddressUnBound(a);
        }
        else
        {
            emit AddressAlreadyUnBound(a);
        }
    }
}