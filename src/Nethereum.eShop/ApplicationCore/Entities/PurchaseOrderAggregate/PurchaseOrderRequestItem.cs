using System;

namespace Nethereum.eShop.ApplicationCore.Entities.PurchaseOrderAggregate
{
    public class PurchaseOrderRequestItem : BaseEntity
    {
        public CatalogItemOrdered ItemOrdered { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public string Unit { get; set; }
        public int POItemNumber { get; set; }
        public DateTimeOffset? GoodsIssueDate { get; set; }
        public DateTimeOffset EscrowReleaseDate { get; set; }
        public string Value { get; set; }
        public string QuantityErc20Symbol { get; set; }
        public string QuantityErc20Address { get; set; }
        public string CurrencyErc20Symbol { get; set; }
        public string CurrencyErc20Address { get; set; }

        private PurchaseOrderRequestItem()
        {
            // required by EF
        }

        public PurchaseOrderRequestItem(CatalogItemOrdered itemOrdered, decimal unitPrice, int units)
        {
            ItemOrdered = itemOrdered;
            UnitPrice = unitPrice;
            Quantity = units;
        }
    }
}
