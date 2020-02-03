using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Nethereum.Commerce.Contracts.PoStorage.ContractDefinition
{
    public partial class PoItem : PoItemBase { }

    public class PoItemBase 
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("uint8", "poItemNumber", 2)]
        public virtual byte PoItemNumber { get; set; }
        [Parameter("bytes32", "soNumber", 3)]
        public virtual byte[] SoNumber { get; set; }
        [Parameter("bytes32", "soItemNumber", 4)]
        public virtual byte[] SoItemNumber { get; set; }
        [Parameter("bytes32", "productId", 5)]
        public virtual byte[] ProductId { get; set; }
        [Parameter("uint256", "quantity", 6)]
        public virtual BigInteger Quantity { get; set; }
        [Parameter("bytes32", "unit", 7)]
        public virtual byte[] Unit { get; set; }
        [Parameter("bytes32", "quantitySymbol", 8)]
        public virtual byte[] QuantitySymbol { get; set; }
        [Parameter("address", "quantityAddress", 9)]
        public virtual string QuantityAddress { get; set; }
        [Parameter("uint256", "currencyValue", 10)]
        public virtual BigInteger CurrencyValue { get; set; }
        [Parameter("bytes32", "currencySymbol", 11)]
        public virtual byte[] CurrencySymbol { get; set; }
        [Parameter("address", "currencyAddress", 12)]
        public virtual string CurrencyAddress { get; set; }
        [Parameter("uint8", "status", 13)]
        public virtual byte Status { get; set; }
        [Parameter("uint256", "goodsIssueDate", 14)]
        public virtual BigInteger GoodsIssueDate { get; set; }
        [Parameter("uint256", "escrowReleaseDate", 15)]
        public virtual BigInteger EscrowReleaseDate { get; set; }
        [Parameter("uint8", "cancelStatus", 16)]
        public virtual byte CancelStatus { get; set; }
    }
}
