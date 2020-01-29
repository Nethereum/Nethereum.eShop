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
using Nethereum.Commerce.Contracts.AddressRegistry.ContractDefinition;

namespace Nethereum.Commerce.Contracts.AddressRegistry
{
    public partial class AddressRegistryService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, AddressRegistryDeployment addressRegistryDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<AddressRegistryDeployment>().SendRequestAndWaitForReceiptAsync(addressRegistryDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, AddressRegistryDeployment addressRegistryDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<AddressRegistryDeployment>().SendRequestAsync(addressRegistryDeployment);
        }

        public static async Task<AddressRegistryService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, AddressRegistryDeployment addressRegistryDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, addressRegistryDeployment, cancellationTokenSource);
            return new AddressRegistryService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public AddressRegistryService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> AddressMapQueryAsync(AddressMapFunction addressMapFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AddressMapFunction, string>(addressMapFunction, blockParameter);
        }

        
        public Task<string> AddressMapQueryAsync(byte[] returnValue1, BlockParameter blockParameter = null)
        {
            var addressMapFunction = new AddressMapFunction();
                addressMapFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<AddressMapFunction, string>(addressMapFunction, blockParameter);
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

        public Task<string> GetAddressQueryAsync(GetAddressFunction getAddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetAddressFunction, string>(getAddressFunction, blockParameter);
        }

        
        public Task<string> GetAddressQueryAsync(byte[] contractName, BlockParameter blockParameter = null)
        {
            var getAddressFunction = new GetAddressFunction();
                getAddressFunction.ContractName = contractName;
            
            return ContractHandler.QueryAsync<GetAddressFunction, string>(getAddressFunction, blockParameter);
        }

        public Task<string> GetAddressStringQueryAsync(GetAddressStringFunction getAddressStringFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetAddressStringFunction, string>(getAddressStringFunction, blockParameter);
        }

        
        public Task<string> GetAddressStringQueryAsync(string contractName, BlockParameter blockParameter = null)
        {
            var getAddressStringFunction = new GetAddressStringFunction();
                getAddressStringFunction.ContractName = contractName;
            
            return ContractHandler.QueryAsync<GetAddressStringFunction, string>(getAddressStringFunction, blockParameter);
        }

        public Task<GetAllAddressesOutputDTO> GetAllAddressesQueryAsync(GetAllAddressesFunction getAllAddressesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetAllAddressesFunction, GetAllAddressesOutputDTO>(getAllAddressesFunction, blockParameter);
        }

        public Task<GetAllAddressesOutputDTO> GetAllAddressesQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetAllAddressesFunction, GetAllAddressesOutputDTO>(null, blockParameter);
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

        public Task<string> RegisterAddressRequestAsync(RegisterAddressFunction registerAddressFunction)
        {
             return ContractHandler.SendRequestAsync(registerAddressFunction);
        }

        public Task<TransactionReceipt> RegisterAddressRequestAndWaitForReceiptAsync(RegisterAddressFunction registerAddressFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerAddressFunction, cancellationToken);
        }

        public Task<string> RegisterAddressRequestAsync(byte[] contractName, string a)
        {
            var registerAddressFunction = new RegisterAddressFunction();
                registerAddressFunction.ContractName = contractName;
                registerAddressFunction.A = a;
            
             return ContractHandler.SendRequestAsync(registerAddressFunction);
        }

        public Task<TransactionReceipt> RegisterAddressRequestAndWaitForReceiptAsync(byte[] contractName, string a, CancellationTokenSource cancellationToken = null)
        {
            var registerAddressFunction = new RegisterAddressFunction();
                registerAddressFunction.ContractName = contractName;
                registerAddressFunction.A = a;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerAddressFunction, cancellationToken);
        }

        public Task<string> RegisterAddressStringRequestAsync(RegisterAddressStringFunction registerAddressStringFunction)
        {
             return ContractHandler.SendRequestAsync(registerAddressStringFunction);
        }

        public Task<TransactionReceipt> RegisterAddressStringRequestAndWaitForReceiptAsync(RegisterAddressStringFunction registerAddressStringFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerAddressStringFunction, cancellationToken);
        }

        public Task<string> RegisterAddressStringRequestAsync(string contractName, string a)
        {
            var registerAddressStringFunction = new RegisterAddressStringFunction();
                registerAddressStringFunction.ContractName = contractName;
                registerAddressStringFunction.A = a;
            
             return ContractHandler.SendRequestAsync(registerAddressStringFunction);
        }

        public Task<TransactionReceipt> RegisterAddressStringRequestAndWaitForReceiptAsync(string contractName, string a, CancellationTokenSource cancellationToken = null)
        {
            var registerAddressStringFunction = new RegisterAddressStringFunction();
                registerAddressStringFunction.ContractName = contractName;
                registerAddressStringFunction.A = a;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerAddressStringFunction, cancellationToken);
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
    }
}
