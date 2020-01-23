pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

contract DynamicArrayTest 
{
    IPoTypes.Po poInStorage;
    
    function testStoringAPo(IPoTypes.Po memory po) public
    {
        // These give unimplemented feature errors:
        // poInStorage = po;
        // poInStorage.poItems = po.poItems;
        
        // Header
        poInStorage.poNumber = po.poNumber;
        poInStorage.buyerAddress = po.buyerAddress;              
        poInStorage.buyerWalletAddress = po.buyerWalletAddress;        
        poInStorage.buyerNonce = po.buyerNonce;                   
        poInStorage.sellerSysId = po.sellerSysId;              
        poInStorage.poCreateDate = po.poCreateDate;               
        
        // Items
        uint len = po.poItems.length;
        //poInStorage.poItems = new IPoTypes.PoItem[](len); // this give not implemented error
        
        for (uint i = 0; i < len; i++)
        {
            //poInStorage.poItems[i] = po.poItems[i]; // this will prob error because poItem[] does not have that item yet?
            poInStorage.poItems.push(po.poItems[i]);
        }
    }
    
    function testReadingPoItemCount() public view returns (uint)
    {
        return poInStorage.poItems.length;
    }
    
    function testGettingPoItem(uint i) public view returns (IPoTypes.PoItem memory item)
    {
        return poInStorage.poItems[i];
    }
    
    // This fn should return value 3
    function testPassingPoBetweenFunctions_Sender() public view returns (uint)
    {
        IPoTypes.Po memory poToSend;
        
        // Header
        poToSend.poNumber = 1;
        poToSend.buyerAddress = msg.sender;              
        poToSend.buyerWalletAddress = msg.sender;        
        poToSend.buyerNonce = 2;                   
        
        // Items
        poToSend.poItems = new IPoTypes.PoItem[](3);
        
        IPoTypes.PoItem memory poItemToSend;
        poItemToSend.poItemNumber = 10;
        poItemToSend.quantity = 400;
        poToSend.poItems[0] = poItemToSend;
        
        poItemToSend.poItemNumber = 20;
        poItemToSend.quantity = 800;
        poToSend.poItems[1] = poItemToSend;
        
        poItemToSend.poItemNumber = 30;
        poItemToSend.quantity = 1200;
        poToSend.poItems[2] = poItemToSend;
        
        return testPassingPoBetweenFunctions_Receiver(poToSend);
    }
    
    function testPassingPoBetweenFunctions_Receiver(IPoTypes.Po memory poPassedIn) public view returns (uint)
    {
        return poPassedIn.poItems.length;
    }
    
}