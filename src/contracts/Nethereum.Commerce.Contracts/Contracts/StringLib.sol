pragma solidity ^0.5.3;

library StringLib
{
    /// @dev Set truncateToLength to <= 0 to take max bytes available
    function bytes32ToString(bytes32 x, uint truncateToLength) public pure returns (string memory) 
    {
        bytes memory bytesString = new bytes(32);
        uint charCount = 0;
        
        for (uint j = 0; j < 32; j++) 
        {
            byte char = byte(bytes32(uint(x) * 2 ** (8 * j)));
            if (char != 0) 
            {
                bytesString[charCount] = char;
                charCount++;
            }
        }
        
        uint finalLength = 0;
        if (truncateToLength > charCount || truncateToLength <= 0)
        {
            finalLength = charCount;
        }
        else
        {
            finalLength = truncateToLength - 1;
        }
        
        bytes memory bytesStringTrimmed = new bytes(finalLength);
        for (uint j = 0; j < finalLength; j++) 
        {
            bytesStringTrimmed[j] = bytesString[j];
        }
        return string(bytesStringTrimmed);
    }
    
    /// @dev Pads shorter strings with 0, truncates longer strings to length 32
    function stringToBytes32(string memory source) public pure returns (bytes32 result) 
    {
        bytes memory tempEmptyStringTest = bytes(source);
        if (tempEmptyStringTest.length == 0) 
        {
            return 0x0;
        }

        assembly
        {
            result := mload(add(source, 32))
        }
    }
}