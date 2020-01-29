using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;
using static Nethereum.Commerce.Contracts.ContractEnums;

namespace Nethereum.Commerce.Contracts.PoStorage.ContractDefinition
{
    public partial class PoItem
    {
        [Parameter("uint8", "poItemNumber", 1)]
        public new uint PoItemNumber { get; set; }

        [Parameter("bytes32", "soNumber", 2)]
        public new string SoNumber { get; set; }

        [Parameter("bytes32", "soItemNumber", 3)]
        public new string SoItemNumber { get; set; }

        [Parameter("bytes32", "productId", 4)]
        public new string ProductId { get; set; }

        [Parameter("uint256", "quantity", 5)]
        public new BigInteger Quantity { get; set; }

        [Parameter("bytes32", "unit", 6)]
        public new string Unit { get; set; }

        [Parameter("bytes32", "quantitySymbol", 7)]
        public new string QuantitySymbol { get; set; }

        [Parameter("address", "quantityAddress", 8)]
        public new string QuantityAddress { get; set; }

        [Parameter("uint256", "currencyValue", 9)]
        public new BigInteger CurrencyValue { get; set; }

        [Parameter("bytes32", "currencySymbol", 10)]
        public new string CurrencySymbol { get; set; }

        [Parameter("address", "currencyAddress", 11)]
        public new string CurrencyAddress { get; set; }

        [Parameter("uint8", "status", 12)]
        public new PoItemStatus Status { get; set; }

        [Parameter("uint256", "goodsIssueDate", 13)]
        public new BigInteger GoodsIssueDate { get; set; }

        [Parameter("uint256", "escrowReleaseDate", 14)]
        public new BigInteger EscrowReleaseDate { get; set; }

        [Parameter("uint8", "cancelStatus", 15)]
        public new PoItemCancelStatus CancelStatus { get; set; }
    }
}
