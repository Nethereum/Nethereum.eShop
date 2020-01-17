pragma solidity ^0.5.10;

// This contract exists only to test contract code generation
contract SimpleStorage
{
    uint storedData;

    function set(uint x) public 
    {
        storedData = x;
    }

    function get() public view returns (uint)
    {
        return storedData;
    }
}