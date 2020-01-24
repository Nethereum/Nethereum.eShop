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
using Nethereum.Commerce.Contracts.DynamicArrayTest.ContractDefinition;

namespace Nethereum.Commerce.Contracts.DynamicArrayTest
{
    public partial class DynamicArrayTestService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, DynamicArrayTestDeployment dynamicArrayTestDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<DynamicArrayTestDeployment>().SendRequestAndWaitForReceiptAsync(dynamicArrayTestDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, DynamicArrayTestDeployment dynamicArrayTestDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<DynamicArrayTestDeployment>().SendRequestAsync(dynamicArrayTestDeployment);
        }

        public static async Task<DynamicArrayTestService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, DynamicArrayTestDeployment dynamicArrayTestDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, dynamicArrayTestDeployment, cancellationTokenSource);
            return new DynamicArrayTestService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public DynamicArrayTestService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> TestEmittingWholePoInLogRequestAsync(TestEmittingWholePoInLogFunction testEmittingWholePoInLogFunction)
        {
             return ContractHandler.SendRequestAsync(testEmittingWholePoInLogFunction);
        }

        public Task<string> TestEmittingWholePoInLogRequestAsync()
        {
             return ContractHandler.SendRequestAsync<TestEmittingWholePoInLogFunction>();
        }

        public Task<TransactionReceipt> TestEmittingWholePoInLogRequestAndWaitForReceiptAsync(TestEmittingWholePoInLogFunction testEmittingWholePoInLogFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(testEmittingWholePoInLogFunction, cancellationToken);
        }

        public Task<TransactionReceipt> TestEmittingWholePoInLogRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<TestEmittingWholePoInLogFunction>(null, cancellationToken);
        }

        public Task<TestGettingPoItemOutputDTO> TestGettingPoItemQueryAsync(TestGettingPoItemFunction testGettingPoItemFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<TestGettingPoItemFunction, TestGettingPoItemOutputDTO>(testGettingPoItemFunction, blockParameter);
        }

        public Task<TestGettingPoItemOutputDTO> TestGettingPoItemQueryAsync(BigInteger i, BlockParameter blockParameter = null)
        {
            var testGettingPoItemFunction = new TestGettingPoItemFunction();
                testGettingPoItemFunction.I = i;
            
            return ContractHandler.QueryDeserializingToObjectAsync<TestGettingPoItemFunction, TestGettingPoItemOutputDTO>(testGettingPoItemFunction, blockParameter);
        }

        public Task<BigInteger> TestReadingPoItemCountQueryAsync(TestReadingPoItemCountFunction testReadingPoItemCountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TestReadingPoItemCountFunction, BigInteger>(testReadingPoItemCountFunction, blockParameter);
        }

        
        public Task<BigInteger> TestReadingPoItemCountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TestReadingPoItemCountFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> TestStoringADummyPoRequestAsync(TestStoringADummyPoFunction testStoringADummyPoFunction)
        {
             return ContractHandler.SendRequestAsync(testStoringADummyPoFunction);
        }

        public Task<string> TestStoringADummyPoRequestAsync()
        {
             return ContractHandler.SendRequestAsync<TestStoringADummyPoFunction>();
        }

        public Task<TransactionReceipt> TestStoringADummyPoRequestAndWaitForReceiptAsync(TestStoringADummyPoFunction testStoringADummyPoFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(testStoringADummyPoFunction, cancellationToken);
        }

        public Task<TransactionReceipt> TestStoringADummyPoRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<TestStoringADummyPoFunction>(null, cancellationToken);
        }

        public Task<string> TestStoringAPoRequestAsync(TestStoringAPoFunction testStoringAPoFunction)
        {
             return ContractHandler.SendRequestAsync(testStoringAPoFunction);
        }

        public Task<TransactionReceipt> TestStoringAPoRequestAndWaitForReceiptAsync(TestStoringAPoFunction testStoringAPoFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(testStoringAPoFunction, cancellationToken);
        }

        public Task<string> TestStoringAPoRequestAsync(Po po)
        {
            var testStoringAPoFunction = new TestStoringAPoFunction();
                testStoringAPoFunction.Po = po;
            
             return ContractHandler.SendRequestAsync(testStoringAPoFunction);
        }

        public Task<TransactionReceipt> TestStoringAPoRequestAndWaitForReceiptAsync(Po po, CancellationTokenSource cancellationToken = null)
        {
            var testStoringAPoFunction = new TestStoringAPoFunction();
                testStoringAPoFunction.Po = po;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(testStoringAPoFunction, cancellationToken);
        }
    }
}
