using Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using System.Numerics;
using System.Threading.Tasks;

namespace Nethereum.Commerce.Contracts.PoStorage
{
    public partial class PoStorageService
    {
        public Task<BigInteger> GetPoNumberBySellerAndQuoteQueryAsync(string sellerId, BigInteger quoteId, BlockParameter blockParameter = null)
        {
            var getPoNumberBySellerAndQuoteFunction = new GetPoNumberBySellerAndQuoteFunction();
            getPoNumberBySellerAndQuoteFunction.SellerId = sellerId.ConvertToBytes();
            getPoNumberBySellerAndQuoteFunction.QuoteId = quoteId;

            return ContractHandler.QueryAsync<GetPoNumberBySellerAndQuoteFunction, BigInteger>(getPoNumberBySellerAndQuoteFunction, blockParameter);
        }
    }
}
