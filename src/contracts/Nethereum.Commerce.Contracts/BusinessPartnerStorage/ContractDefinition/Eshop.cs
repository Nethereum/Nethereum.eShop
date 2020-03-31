using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition
{
    public partial class Eshop : EshopBase { }

    public class EshopBase 
    {
        [Parameter("bytes32", "eShopId", 1)]
        public virtual byte[] EShopId { get; set; }
        [Parameter("bytes32", "eShopDescription", 2)]
        public virtual byte[] EShopDescription { get; set; }
        [Parameter("address", "purchasingContractAddress", 3)]
        public virtual string PurchasingContractAddress { get; set; }
        [Parameter("bool", "isActive", 4)]
        public virtual bool IsActive { get; set; }
        [Parameter("address", "createdByAddress", 5)]
        public virtual string CreatedByAddress { get; set; }
        [Parameter("uint8", "quoteSignerCount", 6)]
        public virtual byte QuoteSignerCount { get; set; }
        [Parameter("address[]", "quoteSigners", 7)]
        public virtual List<string> QuoteSigners { get; set; }
    }
}
