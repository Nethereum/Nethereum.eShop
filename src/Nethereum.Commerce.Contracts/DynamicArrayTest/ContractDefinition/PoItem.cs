using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Nethereum.Commerce.Contracts.DynamicArrayTest.ContractDefinition
{
    public partial class PoItem : PoItemBase { }

    public class PoItemBase 
    {
        [Parameter("uint8", "poItemNumber", 1)]
        public virtual byte PoItemNumber { get; set; }
        [Parameter("bytes32", "soNumber", 2)]
        public virtual byte[] SoNumber { get; set; }
        [Parameter("bytes32", "soItemNumber", 3)]
        public virtual byte[] SoItemNumber { get; set; }
        [Parameter("bytes32", "productId", 4)]
        public virtual byte[] ProductId { get; set; }
        [Parameter("uint256", "quantity", 5)]
        public virtual BigInteger Quantity { get; set; }
        [Parameter("bytes32", "unit", 6)]
        public virtual byte[] Unit { get; set; }
        [Parameter("bytes32", "quantityErc20Symbol", 7)]
        public virtual byte[] QuantityErc20Symbol { get; set; }
        [Parameter("address", "quantityErc20Address", 8)]
        public virtual string QuantityErc20Address { get; set; }
        [Parameter("uint256", "value", 9)]
        public virtual BigInteger Value { get; set; }
        [Parameter("bytes32", "currencyErc20Symbol", 10)]
        public virtual byte[] CurrencyErc20Symbol { get; set; }
        [Parameter("address", "currencyErc20Address", 11)]
        public virtual string CurrencyErc20Address { get; set; }
        [Parameter("uint8", "status", 12)]
        public virtual byte Status { get; set; }
        [Parameter("uint256", "goodsIssueDate", 13)]
        public virtual BigInteger GoodsIssueDate { get; set; }
        [Parameter("uint256", "escrowReleaseDate", 14)]
        public virtual BigInteger EscrowReleaseDate { get; set; }
        [Parameter("uint8", "cancelStatus", 15)]
        public virtual byte CancelStatus { get; set; }
    }
}
