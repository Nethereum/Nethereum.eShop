using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Nethereum.Commerce.Contracts.BuyerWallet.ContractDefinition
{
    public partial class Po : PoBase { }

    public class PoBase 
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("address", "buyerUserAddress", 2)]
        public virtual string BuyerUserAddress { get; set; }
        [Parameter("address", "buyerReceiverAddress", 3)]
        public virtual string BuyerReceiverAddress { get; set; }
        [Parameter("address", "buyerWalletAddress", 4)]
        public virtual string BuyerWalletAddress { get; set; }
        [Parameter("bytes32", "eShopId", 5)]
        public virtual byte[] EShopId { get; set; }
        [Parameter("uint256", "quoteId", 6)]
        public virtual BigInteger QuoteId { get; set; }
        [Parameter("uint256", "quoteExpiryDate", 7)]
        public virtual BigInteger QuoteExpiryDate { get; set; }
        [Parameter("address", "quoteSignerAddress", 8)]
        public virtual string QuoteSignerAddress { get; set; }
        [Parameter("bytes32", "sellerId", 9)]
        public virtual byte[] SellerId { get; set; }
        [Parameter("bytes32", "currencySymbol", 10)]
        public virtual byte[] CurrencySymbol { get; set; }
        [Parameter("address", "currencyAddress", 11)]
        public virtual string CurrencyAddress { get; set; }
        [Parameter("uint8", "poType", 12)]
        public virtual byte PoType { get; set; }
        [Parameter("uint256", "poCreateDate", 13)]
        public virtual BigInteger PoCreateDate { get; set; }
        [Parameter("uint8", "poItemCount", 14)]
        public virtual byte PoItemCount { get; set; }
        [Parameter("tuple[]", "poItems", 15)]
        public virtual List<PoItem> PoItems { get; set; }
        [Parameter("uint8", "rulesCount", 16)]
        public virtual byte RulesCount { get; set; }
        [Parameter("bytes32[]", "rules", 17)]
        public virtual List<byte[]> Rules { get; set; }
    }
}
