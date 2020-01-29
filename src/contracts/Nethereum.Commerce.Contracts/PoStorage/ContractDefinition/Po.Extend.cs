using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Collections.Generic;
using System.Numerics;

namespace Nethereum.Commerce.Contracts.PoStorage.ContractDefinition
{
    public partial class Po
    {
        [Parameter("uint256", "poNumber", 1)]
        public new BigInteger PoNumber { get; set; }

        [Parameter("address", "buyerAddress", 2)]
        public new string BuyerAddress { get; set; }

        [Parameter("address", "buyerWalletAddress", 3)]
        public new string BuyerWalletAddress { get; set; }

        [Parameter("uint256", "buyerNonce", 4)]
        public new BigInteger BuyerNonce { get; set; }

        [Parameter("bytes32", "sellerSysId", 5)]
        public new string SellerSysId { get; set; }

        [Parameter("uint256", "poCreateDate", 6)]  // comment out to see error
        public new BigInteger PoCreateDate { get; set; } // comment out to see error

        [Parameter("uint8", "poItemCount", 7)]
        public new uint PoItemCount { get; set; }

        [Parameter("tuple[]", "poItems", 8)]
        public new List<PoItem> PoItems { get; set; }
    }
}
