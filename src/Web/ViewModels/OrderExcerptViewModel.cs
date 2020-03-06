using System;

namespace Nethereum.eShop.Web.ViewModels
{
    public class OrderExcerptViewModel
    {
        public int OrderId { get; set; }

        public int? QuoteId { get; set; }
        public string TransactionHash { get; set; }

        public string PoNumber { get; set; }

        public string PoType { get; set; }
        public string CurrencySymbol { get; set; }

        public string BuyerId { get; set; }
        public string BuyerAddress { get; set; }
        public DateTimeOffset OrderDate { get; set; }

        public DateTimeOffset? Expiry { get; set; }
        public decimal Total { get; set; }
        public int ItemCount { get; set; }
        public string Status { get; set; }

        public String ShipTo_RecipientName { get; set; }
        public String ShipTo_ZipCode { get; set; }
        public String BillTo_RecipientName { get; set; }
        public String BillTo_ZipCode { get; set; }
    }
}
