using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Nethereum.Commerce.Contracts.SellerAdmin.ContractDefinition
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
        [Parameter("uint256", "currencyValueFee", 11)]
        public virtual BigInteger CurrencyValueFee { get; set; }
        [Parameter("uint8", "status", 12)]
        public virtual byte Status { get; set; }
        [Parameter("uint256", "goodsIssuedDate", 13)]
        public virtual BigInteger GoodsIssuedDate { get; set; }
        [Parameter("uint256", "goodsReceivedDate", 14)]
        public virtual BigInteger GoodsReceivedDate { get; set; }
        [Parameter("uint256", "plannedEscrowReleaseDate", 15)]
        public virtual BigInteger PlannedEscrowReleaseDate { get; set; }
        [Parameter("uint256", "actualEscrowReleaseDate", 16)]
        public virtual BigInteger ActualEscrowReleaseDate { get; set; }
        [Parameter("bool", "isEscrowReleased", 17)]
        public virtual bool IsEscrowReleased { get; set; }
        [Parameter("uint8", "cancelStatus", 18)]
        public virtual byte CancelStatus { get; set; }
    }
}
