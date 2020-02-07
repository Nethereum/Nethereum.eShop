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
using Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition;

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
            getSellerFunction.SellerId = ConversionUtils.ConvertStringToBytes32Array(sellerId);

            return ContractHandler.QueryDeserializingToObjectAsync<GetSellerFunction, GetSellerOutputDTO>(getSellerFunction, blockParameter);
        }
    }
}
