using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Collections.Generic;
using System.Numerics;
using static Nethereum.Commerce.Contracts.ContractEnums;

namespace Nethereum.Commerce.Contracts.Purchasing.ContractDefinition
{
    public partial class Po
    {
        [Parameter("uint256", "poNumber", 1)]
        public new BigInteger PoNumber { get; set; }



        [Parameter("address", "buyerUserAddress", 2)]
        public new string BuyerUserAddress { get; set; }


        [Parameter("address", "buyerReceiverAddress", 3)]
        public new string BuyerReceiverAddress { get; set; }


        [Parameter("address", "buyerWalletAddress", 4)]
        public new string BuyerWalletAddress { get; set; }



        [Parameter("bytes32", "eShopId", 5)]
        public new string EShopId { get; set; }


        [Parameter("uint256", "quoteId", 6)]
        public new BigInteger QuoteId { get; set; }


        [Parameter("uint256", "quoteExpiryDate", 7)]
        public new BigInteger QuoteExpiryDate { get; set; }


        [Parameter("address", "quoteSignerAddress", 8)]
        public new string QuoteSignerAddress { get; set; }



        [Parameter("bytes32", "sellerId", 9)]
        public new string SellerId { get; set; }



        [Parameter("bytes32", "currencySymbol", 10)]
        public new string CurrencySymbol { get; set; }


        [Parameter("address", "currencyAddress", 11)]
        public new string CurrencyAddress { get; set; }


        [Parameter("uint8", "poType", 12)]
        public new PoType PoType { get; set; }


        [Parameter("uint256", "poCreateDate", 13)]
        public new BigInteger PoCreateDate { get; set; }


        [Parameter("uint8", "poItemCount", 14)]
        public new uint PoItemCount { get; set; }


        [Parameter("tuple[]", "poItems", 15)]
        public new List<PoItem> PoItems { get; set; }


        [Parameter("uint8", "rulesCount", 16)]
        public new uint RulesCount { get; set; }


        [Parameter("bytes32[]", "rules", 17)]
        public new List<byte[]> Rules { get; set; }
    }
}
