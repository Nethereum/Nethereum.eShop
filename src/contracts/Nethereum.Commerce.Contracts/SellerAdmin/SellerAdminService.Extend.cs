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
using Nethereum.Commerce.Contracts.SellerAdmin.ContractDefinition;

namespace Nethereum.Commerce.Contracts.SellerAdmin
{
    /// <summary>
    /// This partial class is manually maintained, it is not produced by code gen.
    /// Use it to add overloads to methods, eg so we can pass strings in that will
    /// get converted to byte[] (default c# for Solidity bytes32).
    /// </summary>
    public partial class SellerAdminService
    {
        public Task<TransactionReceipt> SetPoItemAcceptedRequestAndWaitForReceiptAsync(string eShopIdString, BigInteger poNumber, byte poItemNumber, string soNumber, string soItemNumber, CancellationTokenSource cancellationToken = null)
        {
            var setPoItemAcceptedFunction = new SetPoItemAcceptedFunction();
            setPoItemAcceptedFunction.EShopIdString = eShopIdString;
            setPoItemAcceptedFunction.PoNumber = poNumber;
            setPoItemAcceptedFunction.PoItemNumber = poItemNumber;
            setPoItemAcceptedFunction.SoNumber = soNumber.ConvertToBytes32();
            setPoItemAcceptedFunction.SoItemNumber = soItemNumber.ConvertToBytes32();

            return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemAcceptedFunction, cancellationToken);
        }
    }
}
