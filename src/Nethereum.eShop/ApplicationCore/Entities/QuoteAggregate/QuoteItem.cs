using System;

namespace Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate
{
    public class QuoteItem: BaseEntity
    {
        public CatalogItemExcerpt ItemOrdered { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public string Unit { get; set; }

        public int? PoItemNumber { get; set; }

        /// <summary>
        /// Smart contract assigned date
        /// </summary>
        public DateTimeOffset? EscrowReleaseDate { get; set; }
        public string QuantitySymbol { get; set; }
        public string QuantityAddress { get; set; }
        public string CurrencyValue { get; set; }

        public QuoteItem(CatalogItemExcerpt itemOrdered, decimal unitPrice, int quantity)
        {
            ItemOrdered = itemOrdered;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        private QuoteItem()
        {
            // for ef
        }
    }
}
