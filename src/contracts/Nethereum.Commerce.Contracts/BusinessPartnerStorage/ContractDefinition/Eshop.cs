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
        [Parameter("address", "quoteSignerAddress", 4)]
        public virtual string QuoteSignerAddress { get; set; }
        [Parameter("bool", "isActive", 5)]
        public virtual bool IsActive { get; set; }
        [Parameter("address", "createdByAddress", 6)]
        public virtual string CreatedByAddress { get; set; }
    }
}
