using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition
{
    public partial class Seller : SellerBase { }

    public class SellerBase 
    {
        [Parameter("bytes32", "sellerId", 1)]
        public virtual byte[] SellerId { get; set; }
        [Parameter("bytes32", "sellerDescription", 2)]
        public virtual byte[] SellerDescription { get; set; }
        [Parameter("address", "adminContractAddress", 3)]
        public virtual string AdminContractAddress { get; set; }
        [Parameter("bool", "isActive", 4)]
        public virtual bool IsActive { get; set; }
        [Parameter("address", "createdByAddress", 5)]
        public virtual string CreatedByAddress { get; set; }
    }
}
