using Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using System.Threading.Tasks;

namespace Nethereum.Commerce.Contracts.BusinessPartnerStorage
{
    /// <summary>
    /// This partial class is manually maintained, it is not produced by code gen.
    /// Use it to add overloads to methods, eg so we can pass strings in that will
    /// get converted to byte[] (default c# for Solidity bytes32).
    /// </summary>
    public partial class BusinessPartnerStorageService
    {
        public Task<GetSellerOutputDTO> GetSellerQueryAsync(string sellerId, BlockParameter blockParameter = null)
        {
            var getSellerFunction = new GetSellerFunction();
            getSellerFunction.SellerId = sellerId.ConvertToBytes();

            return ContractHandler.QueryDeserializingToObjectAsync<GetSellerFunction, GetSellerOutputDTO>(getSellerFunction, blockParameter);
        }
    }
}
