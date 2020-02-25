namespace Nethereum.Commerce.Contracts
{
    /// <summary>
    /// Manually added since ABI doesn't expose Solidity enums
    /// </summary>
    public class ContractEnums
    {
        public enum PoType
        {
            Initial,                // 0  expect never to see this
            Cash,                   // 1  PO is paid up front by the buyer
            Invoice                 // 2  PO is paid later after buyer receives an invoice
        }

        public enum PoItemStatus
        {
            Initial,                // 0  expect never to see this
            Created,                // 1  PO item has been created in the contract                                    | 0->1 managed by buyer waller contract fn call from buyer UI
            Accepted,               // 2  PO item has been accepted by the seller and a corresponding SO item created | 1->2 managed by seller wallet contract fn call from seller UI
            Rejected,               // 3  PO item has been rejected by the seller                                     | 1->3 managed by seller wallet contract fn call from seller UI 
            ReadyForGoodsIssue,     // 4  PO item is ready for goods issue                                            | 2->4 managed by seller wallet contract fn call from seller UI 
            GoodsIssued,            // 5  PO item has been posted or issued by the seller                             | 4->5 managed by seller wallet contract fn call from seller UI
            GoodsReceived,          // 6  PO item has been received by the buyer or escrow time exceeded              | 5->6 managed by buyer wallet contract fn call from buyer OR po main contract 30 days or Seller UI
            Completed,              // 7  PO item is complete and escrow funds released to the buyer wallet           | 6->7 managed by seller wallet contract fn call from seller UI
            Cancelled               // 8  PO item has been successfully cancelled and funds refunded to buyer wallet  | 1->8 or 2->8 managed by seller wallet contract fn call from seller UI, at discretion of seller (poss break out into own field of po item payment status)
        }

        public enum PoItemCancelStatus
        {
            Initial,                // 0  default to empty, no request made
            RequestMade,            // 1  PO item has had cancellation requested | 0->1 managed by buyer wallet contract fn call from buyer UI
            RequestRejected,        // 2  PO item has had cancellation rejected  | 1->2 managed by seller wallet contract fn call from seller UI 
            RequestAccepted         // 3  PO item has had cancellation accepted  | 1->3 managed by seller wallet contract fn call from seller UI 
        }        
    }
}
