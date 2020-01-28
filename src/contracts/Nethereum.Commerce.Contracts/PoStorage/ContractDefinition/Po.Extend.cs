using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Nethereum.Commerce.Contracts.PoStorage.ContractDefinition
{
    public partial class Po
    { 
        [Parameter("bytes32", "sellerSysId", 5)]
        public new string SellerSysId { get; set; }

        [Parameter("uint256", "poCreateDate", 6)]
        public new DateTime PoCreateDate { get; set; }

        [Parameter("uint8", "poItemCount", 7)]
        public new uint PoItemCount { get; set; }
    }
}
