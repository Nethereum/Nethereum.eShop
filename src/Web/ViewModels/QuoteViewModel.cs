using Nethereum.eShop.ApplicationCore.Entities;
using System;
using System.Collections.Generic;

namespace Nethereum.eShop.Web.ViewModels
{
    public class QuoteViewModel
    {
        private const string DEFAULT_STATUS = "Pending";

        public int QuoteId { get; set; }

        public string TransactionHash { get; set; }

        public DateTimeOffset QuoteDate { get; set; }
        public decimal Total { get; set; }
        public string Status => DEFAULT_STATUS;
        public PostalAddress ShipTo { get; set; }

        public PostalAddress BillTo { get; set; }
        public List<QuoteItemViewModel> QuoteItems { get; set; } = new List<QuoteItemViewModel>();
    }
}
