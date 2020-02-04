using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;

namespace Nethereum.Commerce.Contracts.PoStorage
{
    public partial class PoStorageService
    {
        public Task<BigInteger> GetPoNumberBySellerAndQuoteQueryAsync(string sellerId, BigInteger quoteId, BlockParameter blockParameter = null)
        {
            var getPoNumberBySellerAndQuoteFunction = new GetPoNumberBySellerAndQuoteFunction();
            getPoNumberBySellerAndQuoteFunction.SellerId = ConversionUtils.ConvertStringToBytes32Array(sellerId);
            getPoNumberBySellerAndQuoteFunction.QuoteId = quoteId;

            return ContractHandler.QueryAsync<GetPoNumberBySellerAndQuoteFunction, BigInteger>(getPoNumberBySellerAndQuoteFunction, blockParameter);
        }

    }
}
