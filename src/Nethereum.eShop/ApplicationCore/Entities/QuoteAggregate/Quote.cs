using Nethereum.eShop.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;

namespace Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate
{
    public class Quote: BaseEntity, IAggregateRoot
    {
        public QuoteStatus Status { get; set; }
        public DateTimeOffset Date { get; set; }
        public DateTimeOffset Expiry { get; set; }

        private readonly List<QuoteItem> _quoteItems = new List<QuoteItem>();

        public IReadOnlyCollection<QuoteItem> QuoteItems => _quoteItems.AsReadOnly();

        /// <summary>
        /// The transaction hash for Purchase Order creation
        /// </summary>
        public string TransactionHash { get; set; }

        /// <summary>
        /// The Buyer Address
        /// </summary>
        public string BuyerAddress { get; set; }

        public string CurrencySymbol { get; set; }
        public string CurrencyAddress { get; set; }

        public string ApproverAddress { get; set; }

        /// <summary>
        /// The Purhase Order Number
        /// </summary>
        public long? PoNumber { get; set; }

        // TODO: Change to enum when it is ready
        /*
         * enum PoType
    {
        Initial,                // 0  expect never to see this
        Cash,                   // 1  PO is paid up front by the buyer
        Invoice                 // 2  PO is paid later after buyer receives an invoice
    }
         */
        public int PoType { get; set; }

        /// <summary>
        /// eShop constant - one wallet expected per shop
        /// </summary>
        public string BuyerWalletAddress { get; set; } // buyer wallet address

        /// <summary>
        /// eShop Id - allow orders in the db to reference multiple shops
        /// </summary>
        public string SellerId { get; set; }

        public string BuyerId { get; set; }

        public PostalAddress BillTo { get; set; }

        public PostalAddress ShipTo { get; set; }

        public Quote(IEnumerable<QuoteItem> quoteItems)
        {
            _quoteItems.AddRange(quoteItems);
        }

        private Quote()
        {
            //for EF
        }

        public decimal Total()
        {
            var total = 0m;
            foreach (var item in _quoteItems)
            {
                total += item.UnitPrice * item.Quantity;
            }
            return total;
        }

        public int ItemCount() => _quoteItems == null ? 0 : _quoteItems.Count;

        public void SetConvertedToOrder(int orderNumber, long purchaseOrderNumber, string transactionHash)
        {
            //TODO: Add order number/id field
            TransactionHash = transactionHash;
            PoNumber = purchaseOrderNumber;
            Status = QuoteStatus.Complete;
        }

    }
}
