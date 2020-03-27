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
using Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;

namespace Nethereum.Commerce.Contracts.PoStorage
{
    public partial class PoStorageService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, PoStorageDeployment poStorageDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<PoStorageDeployment>().SendRequestAndWaitForReceiptAsync(poStorageDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, PoStorageDeployment poStorageDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<PoStorageDeployment>().SendRequestAsync(poStorageDeployment);
        }

        public static async Task<PoStorageService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, PoStorageDeployment poStorageDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, poStorageDeployment, cancellationTokenSource);
            return new PoStorageService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public PoStorageService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<BigInteger> BoundAddressCountQueryAsync(BoundAddressCountFunction boundAddressCountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BoundAddressCountFunction, BigInteger>(boundAddressCountFunction, blockParameter);
        }

        
        public Task<BigInteger> BoundAddressCountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BoundAddressCountFunction, BigInteger>(null, blockParameter);
        }

        public Task<bool> BoundAddressesQueryAsync(BoundAddressesFunction boundAddressesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BoundAddressesFunction, bool>(boundAddressesFunction, blockParameter);
        }

        
        public Task<bool> BoundAddressesQueryAsync(string returnValue1, BlockParameter blockParameter = null)
        {
            var boundAddressesFunction = new BoundAddressesFunction();
                boundAddressesFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<BoundAddressesFunction, bool>(boundAddressesFunction, blockParameter);
        }

        public Task<string> AddressRegistryQueryAsync(AddressRegistryFunction addressRegistryFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AddressRegistryFunction, string>(addressRegistryFunction, blockParameter);
        }

        
        public Task<string> AddressRegistryQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AddressRegistryFunction, string>(null, blockParameter);
        }

        public Task<string> BindAddressRequestAsync(BindAddressFunction bindAddressFunction)
        {
             return ContractHandler.SendRequestAsync(bindAddressFunction);
        }

        public Task<TransactionReceipt> BindAddressRequestAndWaitForReceiptAsync(BindAddressFunction bindAddressFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(bindAddressFunction, cancellationToken);
        }

        public Task<string> BindAddressRequestAsync(string a)
        {
            var bindAddressFunction = new BindAddressFunction();
                bindAddressFunction.A = a;
            
             return ContractHandler.SendRequestAsync(bindAddressFunction);
        }

        public Task<TransactionReceipt> BindAddressRequestAndWaitForReceiptAsync(string a, CancellationTokenSource cancellationToken = null)
        {
            var bindAddressFunction = new BindAddressFunction();
                bindAddressFunction.A = a;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(bindAddressFunction, cancellationToken);
        }

        public Task<string> Bytes32ToStringQueryAsync(Bytes32ToStringFunction bytes32ToStringFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<Bytes32ToStringFunction, string>(bytes32ToStringFunction, blockParameter);
        }

        
        public Task<string> Bytes32ToStringQueryAsync(byte[] x, BigInteger truncateToLength, BlockParameter blockParameter = null)
        {
            var bytes32ToStringFunction = new Bytes32ToStringFunction();
                bytes32ToStringFunction.X = x;
                bytes32ToStringFunction.TruncateToLength = truncateToLength;
            
            return ContractHandler.QueryAsync<Bytes32ToStringFunction, string>(bytes32ToStringFunction, blockParameter);
        }

        public Task<string> ConfigureRequestAsync(ConfigureFunction configureFunction)
        {
             return ContractHandler.SendRequestAsync(configureFunction);
        }

        public Task<TransactionReceipt> ConfigureRequestAndWaitForReceiptAsync(ConfigureFunction configureFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(configureFunction, cancellationToken);
        }

        public Task<string> ConfigureRequestAsync(string nameOfEternalStorage)
        {
            var configureFunction = new ConfigureFunction();
                configureFunction.NameOfEternalStorage = nameOfEternalStorage;
            
             return ContractHandler.SendRequestAsync(configureFunction);
        }

        public Task<TransactionReceipt> ConfigureRequestAndWaitForReceiptAsync(string nameOfEternalStorage, CancellationTokenSource cancellationToken = null)
        {
            var configureFunction = new ConfigureFunction();
                configureFunction.NameOfEternalStorage = nameOfEternalStorage;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(configureFunction, cancellationToken);
        }

        public Task<string> EternalStorageQueryAsync(EternalStorageFunction eternalStorageFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<EternalStorageFunction, string>(eternalStorageFunction, blockParameter);
        }

        
        public Task<string> EternalStorageQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<EternalStorageFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> GetCurrentPoNumberQueryAsync(GetCurrentPoNumberFunction getCurrentPoNumberFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetCurrentPoNumberFunction, BigInteger>(getCurrentPoNumberFunction, blockParameter);
        }

        
        public Task<BigInteger> GetCurrentPoNumberQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetCurrentPoNumberFunction, BigInteger>(null, blockParameter);
        }

        public Task<GetPoOutputDTO> GetPoQueryAsync(GetPoFunction getPoFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetPoFunction, GetPoOutputDTO>(getPoFunction, blockParameter);
        }

        public Task<GetPoOutputDTO> GetPoQueryAsync(BigInteger poNumber, BlockParameter blockParameter = null)
        {
            var getPoFunction = new GetPoFunction();
                getPoFunction.PoNumber = poNumber;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetPoFunction, GetPoOutputDTO>(getPoFunction, blockParameter);
        }

        public Task<BigInteger> GetPoNumberByEshopIdAndQuoteQueryAsync(GetPoNumberByEshopIdAndQuoteFunction getPoNumberByEshopIdAndQuoteFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetPoNumberByEshopIdAndQuoteFunction, BigInteger>(getPoNumberByEshopIdAndQuoteFunction, blockParameter);
        }

        
        public Task<BigInteger> GetPoNumberByEshopIdAndQuoteQueryAsync(byte[] eShopId, BigInteger quoteId, BlockParameter blockParameter = null)
        {
            var getPoNumberByEshopIdAndQuoteFunction = new GetPoNumberByEshopIdAndQuoteFunction();
                getPoNumberByEshopIdAndQuoteFunction.EShopId = eShopId;
                getPoNumberByEshopIdAndQuoteFunction.QuoteId = quoteId;
            
            return ContractHandler.QueryAsync<GetPoNumberByEshopIdAndQuoteFunction, BigInteger>(getPoNumberByEshopIdAndQuoteFunction, blockParameter);
        }

        public Task<string> IncrementPoNumberRequestAsync(IncrementPoNumberFunction incrementPoNumberFunction)
        {
             return ContractHandler.SendRequestAsync(incrementPoNumberFunction);
        }

        public Task<string> IncrementPoNumberRequestAsync()
        {
             return ContractHandler.SendRequestAsync<IncrementPoNumberFunction>();
        }

        public Task<TransactionReceipt> IncrementPoNumberRequestAndWaitForReceiptAsync(IncrementPoNumberFunction incrementPoNumberFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(incrementPoNumberFunction, cancellationToken);
        }

        public Task<TransactionReceipt> IncrementPoNumberRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<IncrementPoNumberFunction>(null, cancellationToken);
        }

        public Task<bool> IsOwnerQueryAsync(IsOwnerFunction isOwnerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IsOwnerFunction, bool>(isOwnerFunction, blockParameter);
        }

        
        public Task<bool> IsOwnerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IsOwnerFunction, bool>(null, blockParameter);
        }

        public Task<string> OwnerQueryAsync(OwnerFunction ownerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(ownerFunction, blockParameter);
        }

        
        public Task<string> OwnerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(null, blockParameter);
        }

        public Task<string> SetPoRequestAsync(SetPoFunction setPoFunction)
        {
             return ContractHandler.SendRequestAsync(setPoFunction);
        }

        public Task<TransactionReceipt> SetPoRequestAndWaitForReceiptAsync(SetPoFunction setPoFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoFunction, cancellationToken);
        }

        public Task<string> SetPoRequestAsync(Po po)
        {
            var setPoFunction = new SetPoFunction();
                setPoFunction.Po = po;
            
             return ContractHandler.SendRequestAsync(setPoFunction);
        }

        public Task<TransactionReceipt> SetPoRequestAndWaitForReceiptAsync(Po po, CancellationTokenSource cancellationToken = null)
        {
            var setPoFunction = new SetPoFunction();
                setPoFunction.Po = po;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoFunction, cancellationToken);
        }

        public Task<byte[]> StringToBytes32QueryAsync(StringToBytes32Function stringToBytes32Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<StringToBytes32Function, byte[]>(stringToBytes32Function, blockParameter);
        }

        
        public Task<byte[]> StringToBytes32QueryAsync(string source, BlockParameter blockParameter = null)
        {
            var stringToBytes32Function = new StringToBytes32Function();
                stringToBytes32Function.Source = source;
            
            return ContractHandler.QueryAsync<StringToBytes32Function, byte[]>(stringToBytes32Function, blockParameter);
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

        public Task<string> UnBindAddressRequestAsync(UnBindAddressFunction unBindAddressFunction)
        {
             return ContractHandler.SendRequestAsync(unBindAddressFunction);
        }

        public Task<TransactionReceipt> UnBindAddressRequestAndWaitForReceiptAsync(UnBindAddressFunction unBindAddressFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(unBindAddressFunction, cancellationToken);
        }

        public Task<string> UnBindAddressRequestAsync(string a)
        {
            var unBindAddressFunction = new UnBindAddressFunction();
                unBindAddressFunction.A = a;
            
             return ContractHandler.SendRequestAsync(unBindAddressFunction);
        }

        public Task<TransactionReceipt> UnBindAddressRequestAndWaitForReceiptAsync(string a, CancellationTokenSource cancellationToken = null)
        {
            var unBindAddressFunction = new UnBindAddressFunction();
                unBindAddressFunction.A = a;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(unBindAddressFunction, cancellationToken);
        }
    }
}
