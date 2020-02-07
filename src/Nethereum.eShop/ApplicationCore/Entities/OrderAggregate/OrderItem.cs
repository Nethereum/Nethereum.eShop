using System;

namespace Nethereum.eShop.ApplicationCore.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItemStatus Status { get; set; }
        public CatalogItemExcerpt ItemOrdered { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public string Unit { get; set; }

        // TODO: change to enum
        public int? PoItemStatus { get; set; }
        public int? PoItemNumber { get; set; }

        /// <summary>
        /// eShop assigned issue date (gets sent to smart contract)
        /// </summary>
        public DateTimeOffset? GoodsIssueDate { get; set; }

        public DateTimeOffset? ActualEscrowReleaseDate { get; set; }

        public DateTimeOffset? PlannedEscrowReleaseDate { get; set; }

        public bool? IsEscrowReleased { get; set; }
        public string QuantitySymbol { get; set; }
        public string QuantityAddress { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyAddress { get; set; }
        public string CurrencyValue { get; set; }

        private OrderItem()
        {
            // required by EF
        }

        public OrderItem(CatalogItemExcerpt itemOrdered, decimal unitPrice, int units)
        {
            ItemOrdered = itemOrdered;
            UnitPrice = unitPrice;
            Quantity = units;
        }
    }
}
