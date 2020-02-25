using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Collections.Generic;
using System.Numerics;
using static Nethereum.Commerce.Contracts.ContractEnums;

namespace Nethereum.Commerce.Contracts.WalletBuyer.ContractDefinition
{
    public partial class Po
    {
        [Parameter("uint256", "poNumber", 1)]
        public new BigInteger PoNumber { get; set; }


        [Parameter("address", "buyerAddress", 2)]
        public new string BuyerAddress { get; set; }


        [Parameter("address", "receiverAddress", 3)]
        public new string ReceiverAddress { get; set; }


        [Parameter("address", "buyerWalletAddress", 4)]
        public new string BuyerWalletAddress { get; set; }


        [Parameter("bytes32", "currencySymbol", 5)]
        public new string CurrencySymbol { get; set; }


        [Parameter("address", "currencyAddress", 6)]
        public new string CurrencyAddress { get; set; }


        [Parameter("uint256", "quoteId", 7)]
        public new BigInteger QuoteId { get; set; }


        [Parameter("uint256", "quoteExpiryDate", 8)]
        public new BigInteger QuoteExpiryDate { get; set; }


        [Parameter("address", "approverAddress", 9)]
        public new string ApproverAddress { get; set; }


        [Parameter("uint8", "poType", 10)]
        public new PoType PoType { get; set; }


        [Parameter("bytes32", "sellerId", 11)]
        public new string SellerId { get; set; }


        [Parameter("uint256", "poCreateDate", 12)]
        public new BigInteger PoCreateDate { get; set; }


        [Parameter("uint8", "poItemCount", 13)]
        public new uint PoItemCount { get; set; }


        [Parameter("tuple[]", "poItems", 14)]
        public new List<PoItem> PoItems { get; set; }
    }
}
