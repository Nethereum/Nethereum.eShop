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
        public new byte[] SellerSysId { get; set; }

        [Parameter("uint256", "poCreateDate", 6)]
        public new DateTime PoCreateDate { get; set; }
    }
}
