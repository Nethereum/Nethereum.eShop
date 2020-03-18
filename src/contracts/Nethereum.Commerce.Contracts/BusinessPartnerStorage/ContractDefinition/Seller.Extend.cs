using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition
{
    public partial class Seller
    {
        [Parameter("bytes32", "sellerId", 1)]
        public new string SellerId { get; set; }


        [Parameter("bytes32", "sellerDescription", 2)]
        public new string SellerDescription { get; set; }


        [Parameter("address", "adminContractAddress", 3)]
        public new string AdminContractAddress { get; set; }

        [Parameter("bool", "isActive", 4)]
        public new bool IsActive { get; set; }


        [Parameter("address", "createdByAddress", 5)]
        public new string CreatedByAddress { get; set; }
    }
}
