using Nethereum.eShop.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;

namespace Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate
{
    public class Quote: BaseEntity, IAggregateRoot
    {
        public DateTimeOffset Date { get; set; }
        public DateTimeOffset Expiry { get; set; }

        private readonly List<QuoteItem> _quoteItems = new List<QuoteItem>();

        public IReadOnlyCollection<QuoteItem> QuoteItems => _quoteItems.AsReadOnly();

        /// <summary>
        /// The transaction hash for Purchase Order creation
        /// </summary>
        public string TransactionHash { get; private set; }

        /// <summary>
        /// The Buyer Address
        /// </summary>
        public string BuyerAddress { get; private set; }

        public string CurrencySymbol { get; set; }
        public string CurrencyAddress { get; set; }

        public string ApproverAddress { get; private set; }

        /// <summary>
        /// The Purhase Order Number
        /// </summary>
        public long? PoNumber { get; private set; }

        // TODO: Change to enum when it is ready
        /*
         * enum PoType
    {
        Initial,                // 0  expect never to see this
        Cash,                   // 1  PO is paid up front by the buyer
        Invoice                 // 2  PO is paid later after buyer receives an invoice
    }
         */
        public int PoType { get; private set; }

        /// <summary>
        /// eShop constant - one wallet expected per shop
        /// </summary>
        public string BuyerWalletAddress { get; set; } // buyer wallet address

        /// <summary>
        /// eShop Id - allow orders in the db to reference multiple shops
        /// </summary>
        public string SellerId { get; set; }

        public string BuyerId { get; private set; }

        public PostalAddress BillTo { get; set; }

        public PostalAddress ShipTo { get; set; }

        public Quote()
        {

        }
    }
}
