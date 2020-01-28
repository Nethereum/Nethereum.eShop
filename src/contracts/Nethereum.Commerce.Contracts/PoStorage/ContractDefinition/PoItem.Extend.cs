using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Numerics;
using static Nethereum.Commerce.Contracts.ContractEnums;

namespace Nethereum.Commerce.Contracts.PoStorage.ContractDefinition
{
    public partial class PoItem
    {
        [Parameter("bytes32", "soNumber", 2)]
        public new string SoNumber { get; set; }

        [Parameter("bytes32", "soItemNumber", 3)]
        public new string SoItemNumber { get; set; }

        [Parameter("bytes32", "productId", 4)]
        public new string ProductId { get; set; }

        [Parameter("bytes32", "unit", 6)]
        public new string Unit { get; set; }

        [Parameter("bytes32", "quantityErc20Symbol", 7)]
        public new string QuantityErc20Symbol { get; set; }

        [Parameter("bytes32", "currencyErc20Symbol", 10)]
        public new string CurrencyErc20Symbol { get; set; }

        [Parameter("address", "currencyErc20Address", 11)]
        public new string CurrencyErc20Address { get; set; }

        [Parameter("uint8", "status", 12)]
        public new PoItemStatus Status { get; set; }

        [Parameter("uint256", "goodsIssueDate", 13)]
        public new DateTime GoodsIssueDate { get; set; }

        [Parameter("uint256", "escrowReleaseDate", 14)]
        public new DateTime EscrowReleaseDate { get; set; }

        [Parameter("uint8", "cancelStatus", 15)]
        public new PoItemCancelStatus CancelStatus { get; set; }
    }
}
