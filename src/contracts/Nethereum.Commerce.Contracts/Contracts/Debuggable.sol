pragma solidity ^0.5.3;
pragma experimental ABIEncoderV2;

contract Debuggable
{
    event LogString(string message);
    event LogAddr(string desc, address message);
    event LogBytes32(string desc, bytes32 message);
    event LogUint64(string desc, uint64 message);
    event LogUint32(string desc, uint32 message);
 
    bool public isDebugOn;
    
    function switchDebugOn() public
    {
        isDebugOn = true;
    }
    
    function switchDebugOff() public
    {
        isDebugOn = false;
    }
    
    function logDebugString(string memory message) internal
    {
        if (isDebugOn)
        {
            emit LogString(message);
        }
    }
    
    function logDebugAddr(string memory desc, address message) internal
    {
        if (isDebugOn)
        {
            emit LogAddr(desc, message);
        }
    }
    
    function logDebugBytes32(string memory desc, bytes32 message) internal
    {
        if (isDebugOn)
        {
            emit LogBytes32(desc, message);
        }
    }
    
    function logDebugUint64(string memory desc, uint64 message) internal
    {
        if (isDebugOn)
        {
            emit LogUint64(desc, message);
        }
    }
    
    function logDebugUint32(string memory desc, uint32 message) internal
    {
        if (isDebugOn)
        {
            emit LogUint32(desc, message);
        }
    }
}
