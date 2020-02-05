using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;

namespace Nethereum.Commerce.Contracts.PoStorage.ContractDefinition
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
        [Parameter("bytes32", "quantitySymbol", 7)]
        public virtual byte[] QuantitySymbol { get; set; }
        [Parameter("address", "quantityAddress", 8)]
        public virtual string QuantityAddress { get; set; }
        [Parameter("uint256", "currencyValue", 9)]
        public virtual BigInteger CurrencyValue { get; set; }
        [Parameter("bytes32", "currencySymbol", 10)]
        public virtual byte[] CurrencySymbol { get; set; }
        [Parameter("address", "currencyAddress", 11)]
        public virtual string CurrencyAddress { get; set; }
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
