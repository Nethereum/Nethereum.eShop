﻿using Nethereum.eShop.ApplicationCore.Entities;
using System;
using System.Collections.Generic;

namespace Nethereum.eShop.Web.ViewModels
{
    public class QuoteViewModel
    {
        public int QuoteId { get; set; }

        public string TransactionHash { get; set; }

        public DateTimeOffset QuoteDate { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public PostalAddress ShipTo { get; set; }

        public PostalAddress BillTo { get; set; }
        public List<QuoteItemViewModel> QuoteItems { get; set; } = new List<QuoteItemViewModel>();
    }

    public class QuoteExcerptViewModel
    {
        public int QuoteId { get; set; }
        public string TransactionHash { get; set; }

        public string PoNumber { get; set; }

        public string PoType { get; set; }
        public string CurrencySymbol { get; set; }

        public string BuyerId { get; set; }
        public string BuyerAddress { get; set; }
        public DateTimeOffset QuoteDate { get; set; }

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
