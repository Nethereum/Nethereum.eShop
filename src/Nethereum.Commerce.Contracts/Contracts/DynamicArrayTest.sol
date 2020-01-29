pragma solidity ^0.6.1;
pragma experimental ABIEncoderV2;

import "./IPoTypes.sol";

contract DynamicArrayTest 
{
    event PoLog(IPoTypes.Po po);
    
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
    
    function testStoringADummyPo() public
    {
        IPoTypes.Po memory poToSend;
        
        // Header
        poToSend.poNumber = 1;
        poToSend.buyerAddress = msg.sender;              
        poToSend.buyerWalletAddress = msg.sender;        
        poToSend.buyerNonce = 2;                   
        
        // Items
        poToSend.poItems = new IPoTypes.PoItem[](3);
        
        IPoTypes.PoItem memory poItemToSend0;
        poItemToSend0.poItemNumber = 10;
        poItemToSend0.quantity = 400;
        poToSend.poItems[0] = poItemToSend0;
        
        IPoTypes.PoItem memory poItemToSend1;
        poItemToSend1.poItemNumber = 20;
        poItemToSend1.quantity = 800;
        poToSend.poItems[1] = poItemToSend1;
        
        IPoTypes.PoItem memory poItemToSend2;
        poItemToSend2.poItemNumber = 30;
        poItemToSend2.quantity = 1200;
        poToSend.poItems[2] = poItemToSend2;
        
        testStoringAPo(poToSend);
    }
    
    function testEmittingWholePoInLog() public
    {
        emit PoLog(poInStorage);
    }
    
    function testReadingPoItemCount() public view returns (uint)
    {
        return poInStorage.poItems.length;
    }
    
    function testGettingPoItem(uint i) public view returns (IPoTypes.PoItem memory item)
    {
        return poInStorage.poItems[i];
    }
    
}