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
using Nethereum.Commerce.Contracts.Owned.ContractDefinition;

namespace Nethereum.Commerce.Contracts.Owned
{
    public partial class OwnedService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, OwnedDeployment ownedDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<OwnedDeployment>().SendRequestAndWaitForReceiptAsync(ownedDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, OwnedDeployment ownedDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<OwnedDeployment>().SendRequestAsync(ownedDeployment);
        }

        public static async Task<OwnedService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, OwnedDeployment ownedDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, ownedDeployment, cancellationTokenSource);
            return new OwnedService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public OwnedService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> AcceptOwnershipRequestAsync(AcceptOwnershipFunction acceptOwnershipFunction)
        {
             return ContractHandler.SendRequestAsync(acceptOwnershipFunction);
        }

        public Task<string> AcceptOwnershipRequestAsync()
        {
             return ContractHandler.SendRequestAsync<AcceptOwnershipFunction>();
        }

        public Task<TransactionReceipt> AcceptOwnershipRequestAndWaitForReceiptAsync(AcceptOwnershipFunction acceptOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(acceptOwnershipFunction, cancellationToken);
        }

        public Task<TransactionReceipt> AcceptOwnershipRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<AcceptOwnershipFunction>(null, cancellationToken);
        }

        public Task<string> NewOwnerQueryAsync(NewOwnerFunction newOwnerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NewOwnerFunction, string>(newOwnerFunction, blockParameter);
        }

        
        public Task<string> NewOwnerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NewOwnerFunction, string>(null, blockParameter);
        }

        public Task<string> OwnerQueryAsync(OwnerFunction ownerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(ownerFunction, blockParameter);
        }

        
        public Task<string> OwnerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(null, blockParameter);
        }

        public Task<string> TransferOwnershipRequestAsync(TransferOwnershipFunction transferOwnershipFunction)
        {
             return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }

        public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(TransferOwnershipFunction transferOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }

        public Task<string> TransferOwnershipRequestAsync(string newOwner)
        {
            var transferOwnershipFunction = new TransferOwnershipFunction();
                transferOwnershipFunction.NewOwner = newOwner;
            
             return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }

        public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(string newOwner, CancellationTokenSource cancellationToken = null)
        {
            var transferOwnershipFunction = new TransferOwnershipFunction();
                transferOwnershipFunction.NewOwner = newOwner;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }
    }
}
