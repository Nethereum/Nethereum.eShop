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
        public Task<TransactionReceipt> SetSystemDescriptionRequestStringAndWaitForReceiptAsync(string systemId, string systemDescription, CancellationTokenSource cancellationToken = null)
        {
            var setSystemDescriptionFunction = new SetSystemDescriptionFunction();
            setSystemDescriptionFunction.SystemId = ConversionUtils.ConvertStringToBytes32Array(systemId);
            setSystemDescriptionFunction.SystemDescription = ConversionUtils.ConvertStringToBytes32Array(systemDescription);

            return ContractHandler.SendRequestAndWaitForReceiptAsync(setSystemDescriptionFunction, cancellationToken);
        }

        public Task<TransactionReceipt> SetWalletAddressRequestStringAndWaitForReceiptAsync(string systemId, string walletAddress, CancellationTokenSource cancellationToken = null)
        {
            var setWalletAddressFunction = new SetWalletAddressFunction();
            setWalletAddressFunction.SystemId = ConversionUtils.ConvertStringToBytes32Array(systemId);
            setWalletAddressFunction.WalletAddress = walletAddress;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(setWalletAddressFunction, cancellationToken);
        }
    }
}
