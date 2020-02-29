using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Nethereum.Commerce.Contracts.IPurchasing.ContractDefinition
{
    public partial class Po : PoBase { }

    public class PoBase 
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("address", "buyerAddress", 2)]
        public virtual string BuyerAddress { get; set; }
        [Parameter("address", "receiverAddress", 3)]
        public virtual string ReceiverAddress { get; set; }
        [Parameter("address", "buyerWalletAddress", 4)]
        public virtual string BuyerWalletAddress { get; set; }
        [Parameter("bytes32", "currencySymbol", 5)]
        public virtual byte[] CurrencySymbol { get; set; }
        [Parameter("address", "currencyAddress", 6)]
        public virtual string CurrencyAddress { get; set; }
        [Parameter("uint256", "quoteId", 7)]
        public virtual BigInteger QuoteId { get; set; }
        [Parameter("uint256", "quoteExpiryDate", 8)]
        public virtual BigInteger QuoteExpiryDate { get; set; }
        [Parameter("address", "approverAddress", 9)]
        public virtual string ApproverAddress { get; set; }
        [Parameter("uint8", "poType", 10)]
        public virtual byte PoType { get; set; }
        [Parameter("bytes32", "sellerId", 11)]
        public virtual byte[] SellerId { get; set; }
        [Parameter("uint256", "poCreateDate", 12)]
        public virtual BigInteger PoCreateDate { get; set; }
        [Parameter("uint8", "poItemCount", 13)]
        public virtual byte PoItemCount { get; set; }
        [Parameter("tuple[]", "poItems", 14)]
        public virtual List<PoItem> PoItems { get; set; }
    }
}
