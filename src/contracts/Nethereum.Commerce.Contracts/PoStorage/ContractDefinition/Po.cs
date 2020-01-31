using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Nethereum.Commerce.Contracts.PoStorage.ContractDefinition
{
    public partial class Po : PoBase { }

    public class PoBase 
    {
        [Parameter("uint256", "poNumber", 1)]
        public virtual BigInteger PoNumber { get; set; }
        [Parameter("address", "buyerAddress", 2)]
        public virtual string BuyerAddress { get; set; }
        [Parameter("address", "buyerWalletAddress", 3)]
        public virtual string BuyerWalletAddress { get; set; }
        [Parameter("uint256", "buyerNonce", 4)]
        public virtual BigInteger BuyerNonce { get; set; }
        [Parameter("uint8", "poType", 5)]
        public virtual byte PoType { get; set; }
        [Parameter("bytes32", "sellerSysId", 6)]
        public virtual byte[] SellerSysId { get; set; }
        [Parameter("uint256", "poCreateDate", 7)]
        public virtual BigInteger PoCreateDate { get; set; }
        [Parameter("uint8", "poItemCount", 8)]
        public virtual byte PoItemCount { get; set; }
        [Parameter("tuple[]", "poItems", 9)]
        public virtual List<PoItem> PoItems { get; set; }
    }
}
