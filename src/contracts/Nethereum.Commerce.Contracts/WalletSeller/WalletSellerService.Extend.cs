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
using Nethereum.Commerce.Contracts.WalletSeller.ContractDefinition;

namespace Nethereum.Commerce.Contracts.WalletSeller
{
    /// <summary>
    /// This partial class is manually maintained, it is not produced by code gen.
    /// Use it to add overloads to methods, eg so we can pass strings in that will
    /// get converted to byte[] (default c# for Solidity bytes32).
    /// </summary>
    public partial class WalletSellerService
    {
        public Task<TransactionReceipt> SetPoItemAcceptedRequestAndWaitForReceiptAsync(BigInteger poNumber, byte poItemNumber, string soNumber, string soItemNumber, CancellationTokenSource cancellationToken = null)
        {
            var setPoItemAcceptedFunction = new SetPoItemAcceptedFunction();
            setPoItemAcceptedFunction.PoNumber = poNumber;
            setPoItemAcceptedFunction.PoItemNumber = poItemNumber;
            setPoItemAcceptedFunction.SoNumber = soNumber.ConvertToBytes();
            setPoItemAcceptedFunction.SoItemNumber = soItemNumber.ConvertToBytes();

            return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemAcceptedFunction, cancellationToken);
        }
    }
}
