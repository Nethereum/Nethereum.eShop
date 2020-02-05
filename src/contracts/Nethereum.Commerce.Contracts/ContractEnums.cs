namespace Nethereum.Commerce.Contracts
{
    /// <summary>
    /// Manually added since ABI doesn't expose Solidity enums
    /// </summary>
    public class ContractEnums
    {
        public enum PoItemStatus
        {
            Initial,                // 0  expect never to see this
            PoCreated,              // 1  PO item has been created in the contract                                    | 0->1 managed by buyer waller contract fn call from buyer UI
            PoAcceptedSoCreated,    // 2  PO item has been accepted by the seller and a corresponding SO item created | 1->2 managed by seller wallet contract fn call from seller UI
            GoodsIssue,             // 3  PO item has been posted or issued by the seller                             | 2->3 managed by seller wallet contract fn call from seller UI
            GoodsReceived,          // 4  PO item has been received by the buyer or escrow time exceeded              | 3->4 managed by buyer wallet contract fn call from buyer OR po main contract 30 days or Seller UI
            CompletedPaid,          // 5  PO item is complete and escrow funds released to the buyer wallet           | 4->5 managed by seller wallet contract fn call from seller UI
            CancelledRefund         // 6  PO item has been successfully cancelled and funds refunded to buyer wallet  | 1->6 or 2->6 managed by seller wallet contract fn call from seller UI, at discretion of seller (poss break out into own field of po item payment status)
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
