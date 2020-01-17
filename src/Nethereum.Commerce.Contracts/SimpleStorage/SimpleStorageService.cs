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
using Nethereum.Commerce.Contracts.SimpleStorage.ContractDefinition;

namespace Nethereum.Commerce.Contracts.SimpleStorage
{
    public partial class SimpleStorageService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, SimpleStorageDeployment simpleStorageDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<SimpleStorageDeployment>().SendRequestAndWaitForReceiptAsync(simpleStorageDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, SimpleStorageDeployment simpleStorageDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<SimpleStorageDeployment>().SendRequestAsync(simpleStorageDeployment);
        }

        public static async Task<SimpleStorageService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, SimpleStorageDeployment simpleStorageDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, simpleStorageDeployment, cancellationTokenSource);
            return new SimpleStorageService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public SimpleStorageService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> SetRequestAsync(SetFunction setFunction)
        {
             return ContractHandler.SendRequestAsync(setFunction);
        }

        public Task<TransactionReceipt> SetRequestAndWaitForReceiptAsync(SetFunction setFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setFunction, cancellationToken);
        }

        public Task<string> SetRequestAsync(BigInteger x)
        {
            var setFunction = new SetFunction();
                setFunction.X = x;
            
             return ContractHandler.SendRequestAsync(setFunction);
        }

        public Task<TransactionReceipt> SetRequestAndWaitForReceiptAsync(BigInteger x, CancellationTokenSource cancellationToken = null)
        {
            var setFunction = new SetFunction();
                setFunction.X = x;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setFunction, cancellationToken);
        }

        public Task<BigInteger> GetQueryAsync(GetFunction getFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetFunction, BigInteger>(getFunction, blockParameter);
        }

        
        public Task<BigInteger> GetQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetFunction, BigInteger>(null, blockParameter);
        }
    }
}
