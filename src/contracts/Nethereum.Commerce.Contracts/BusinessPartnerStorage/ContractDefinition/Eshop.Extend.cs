using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition
{
    public partial class Eshop 
    {
        /// <summary>
        /// eShop seller id, 32 chars max, eg "Nethereum.eShop"
        /// </summary>
        [Parameter("bytes32", "eShopId", 1)]
        public new string EShopId { get; set; }


        [Parameter("bytes32", "eShopDescription", 2)]
        public new string EShopDescription { get; set; }


        [Parameter("address", "purchasingContractAddress", 3)]
        public new string PurchasingContractAddress { get; set; }

        
        [Parameter("bool", "isActive", 4)]
        public new bool IsActive { get; set; }


        [Parameter("address", "createdByAddress", 5)]
        public new string CreatedByAddress { get; set; }


        [Parameter("uint8", "quoteSignerCount", 6)]
        public new uint QuoteSignerCount { get; set; }


        [Parameter("address[]", "quoteSigners", 7)]
        public new List<string> QuoteSigners { get; set; }
    }
}
