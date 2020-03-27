using Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using System.Numerics;
using System.Threading.Tasks;

namespace Nethereum.Commerce.Contracts.PoStorage
{
    public partial class PoStorageService
    {
        public Task<BigInteger> GetPoNumberByEshopIdAndQuoteQueryAsync(string eShopId, BigInteger quoteId, BlockParameter blockParameter = null)
        {
            var getPoNumberBySellerAndQuoteFunction = new GetPoNumberByEshopIdAndQuoteFunction();
            getPoNumberBySellerAndQuoteFunction.EShopId = eShopId.ConvertToBytes32();
            getPoNumberBySellerAndQuoteFunction.QuoteId = quoteId;

            return ContractHandler.QueryAsync<GetPoNumberByEshopIdAndQuoteFunction, BigInteger>(getPoNumberBySellerAndQuoteFunction, blockParameter);
        }
    }
}
