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
using Nethereum.Commerce.Contracts.EternalStorage.ContractDefinition;

namespace Nethereum.Commerce.Contracts.EternalStorage
{
    public partial class EternalStorageService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, EternalStorageDeployment eternalStorageDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<EternalStorageDeployment>().SendRequestAndWaitForReceiptAsync(eternalStorageDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, EternalStorageDeployment eternalStorageDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<EternalStorageDeployment>().SendRequestAsync(eternalStorageDeployment);
        }

        public static async Task<EternalStorageService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, EternalStorageDeployment eternalStorageDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, eternalStorageDeployment, cancellationTokenSource);
            return new EternalStorageService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public EternalStorageService(Nethereum.Web3.Web3 web3, string contractAddress)
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

        public Task<string> GetAddressValueQueryAsync(GetAddressValueFunction getAddressValueFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetAddressValueFunction, string>(getAddressValueFunction, blockParameter);
        }

        
        public Task<string> GetAddressValueQueryAsync(byte[] key, BlockParameter blockParameter = null)
        {
            var getAddressValueFunction = new GetAddressValueFunction();
                getAddressValueFunction.Key = key;
            
            return ContractHandler.QueryAsync<GetAddressValueFunction, string>(getAddressValueFunction, blockParameter);
        }

        public Task<bool> GetBooleanValueQueryAsync(GetBooleanValueFunction getBooleanValueFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetBooleanValueFunction, bool>(getBooleanValueFunction, blockParameter);
        }

        
        public Task<bool> GetBooleanValueQueryAsync(byte[] key, BlockParameter blockParameter = null)
        {
            var getBooleanValueFunction = new GetBooleanValueFunction();
                getBooleanValueFunction.Key = key;
            
            return ContractHandler.QueryAsync<GetBooleanValueFunction, bool>(getBooleanValueFunction, blockParameter);
        }

        public Task<byte[]> GetBytes32ValueQueryAsync(GetBytes32ValueFunction getBytes32ValueFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetBytes32ValueFunction, byte[]>(getBytes32ValueFunction, blockParameter);
        }

        
        public Task<byte[]> GetBytes32ValueQueryAsync(byte[] key, BlockParameter blockParameter = null)
        {
            var getBytes32ValueFunction = new GetBytes32ValueFunction();
                getBytes32ValueFunction.Key = key;
            
            return ContractHandler.QueryAsync<GetBytes32ValueFunction, byte[]>(getBytes32ValueFunction, blockParameter);
        }

        public Task<BigInteger> GetInt256ValueQueryAsync(GetInt256ValueFunction getInt256ValueFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetInt256ValueFunction, BigInteger>(getInt256ValueFunction, blockParameter);
        }

        
        public Task<BigInteger> GetInt256ValueQueryAsync(byte[] key, BlockParameter blockParameter = null)
        {
            var getInt256ValueFunction = new GetInt256ValueFunction();
                getInt256ValueFunction.Key = key;
            
            return ContractHandler.QueryAsync<GetInt256ValueFunction, BigInteger>(getInt256ValueFunction, blockParameter);
        }

        public Task<BigInteger> GetMappingAddressToUint256ValueQueryAsync(GetMappingAddressToUint256ValueFunction getMappingAddressToUint256ValueFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetMappingAddressToUint256ValueFunction, BigInteger>(getMappingAddressToUint256ValueFunction, blockParameter);
        }

        
        public Task<BigInteger> GetMappingAddressToUint256ValueQueryAsync(byte[] storageKey, string mappingKey, BlockParameter blockParameter = null)
        {
            var getMappingAddressToUint256ValueFunction = new GetMappingAddressToUint256ValueFunction();
                getMappingAddressToUint256ValueFunction.StorageKey = storageKey;
                getMappingAddressToUint256ValueFunction.MappingKey = mappingKey;
            
            return ContractHandler.QueryAsync<GetMappingAddressToUint256ValueFunction, BigInteger>(getMappingAddressToUint256ValueFunction, blockParameter);
        }

        public Task<string> GetMappingBytes32ToAddressValueQueryAsync(GetMappingBytes32ToAddressValueFunction getMappingBytes32ToAddressValueFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetMappingBytes32ToAddressValueFunction, string>(getMappingBytes32ToAddressValueFunction, blockParameter);
        }

        
        public Task<string> GetMappingBytes32ToAddressValueQueryAsync(byte[] storageKey, byte[] mappingKey, BlockParameter blockParameter = null)
        {
            var getMappingBytes32ToAddressValueFunction = new GetMappingBytes32ToAddressValueFunction();
                getMappingBytes32ToAddressValueFunction.StorageKey = storageKey;
                getMappingBytes32ToAddressValueFunction.MappingKey = mappingKey;
            
            return ContractHandler.QueryAsync<GetMappingBytes32ToAddressValueFunction, string>(getMappingBytes32ToAddressValueFunction, blockParameter);
        }

        public Task<bool> GetMappingBytes32ToBoolValueQueryAsync(GetMappingBytes32ToBoolValueFunction getMappingBytes32ToBoolValueFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetMappingBytes32ToBoolValueFunction, bool>(getMappingBytes32ToBoolValueFunction, blockParameter);
        }

        
        public Task<bool> GetMappingBytes32ToBoolValueQueryAsync(byte[] storageKey, byte[] mappingKey, BlockParameter blockParameter = null)
        {
            var getMappingBytes32ToBoolValueFunction = new GetMappingBytes32ToBoolValueFunction();
                getMappingBytes32ToBoolValueFunction.StorageKey = storageKey;
                getMappingBytes32ToBoolValueFunction.MappingKey = mappingKey;
            
            return ContractHandler.QueryAsync<GetMappingBytes32ToBoolValueFunction, bool>(getMappingBytes32ToBoolValueFunction, blockParameter);
        }

        public Task<byte[]> GetMappingBytes32ToBytes32ValueQueryAsync(GetMappingBytes32ToBytes32ValueFunction getMappingBytes32ToBytes32ValueFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetMappingBytes32ToBytes32ValueFunction, byte[]>(getMappingBytes32ToBytes32ValueFunction, blockParameter);
        }

        
        public Task<byte[]> GetMappingBytes32ToBytes32ValueQueryAsync(byte[] storageKey, byte[] mappingKey, BlockParameter blockParameter = null)
        {
            var getMappingBytes32ToBytes32ValueFunction = new GetMappingBytes32ToBytes32ValueFunction();
                getMappingBytes32ToBytes32ValueFunction.StorageKey = storageKey;
                getMappingBytes32ToBytes32ValueFunction.MappingKey = mappingKey;
            
            return ContractHandler.QueryAsync<GetMappingBytes32ToBytes32ValueFunction, byte[]>(getMappingBytes32ToBytes32ValueFunction, blockParameter);
        }

        public Task<BigInteger> GetMappingBytes32ToUint256ValueQueryAsync(GetMappingBytes32ToUint256ValueFunction getMappingBytes32ToUint256ValueFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetMappingBytes32ToUint256ValueFunction, BigInteger>(getMappingBytes32ToUint256ValueFunction, blockParameter);
        }

        
        public Task<BigInteger> GetMappingBytes32ToUint256ValueQueryAsync(byte[] storageKey, byte[] mappingKey, BlockParameter blockParameter = null)
        {
            var getMappingBytes32ToUint256ValueFunction = new GetMappingBytes32ToUint256ValueFunction();
                getMappingBytes32ToUint256ValueFunction.StorageKey = storageKey;
                getMappingBytes32ToUint256ValueFunction.MappingKey = mappingKey;
            
            return ContractHandler.QueryAsync<GetMappingBytes32ToUint256ValueFunction, BigInteger>(getMappingBytes32ToUint256ValueFunction, blockParameter);
        }

        public Task<string> GetStringValueQueryAsync(GetStringValueFunction getStringValueFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetStringValueFunction, string>(getStringValueFunction, blockParameter);
        }

        
        public Task<string> GetStringValueQueryAsync(byte[] key, BlockParameter blockParameter = null)
        {
            var getStringValueFunction = new GetStringValueFunction();
                getStringValueFunction.Key = key;
            
            return ContractHandler.QueryAsync<GetStringValueFunction, string>(getStringValueFunction, blockParameter);
        }

        public Task<BigInteger> GetUint256ValueQueryAsync(GetUint256ValueFunction getUint256ValueFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetUint256ValueFunction, BigInteger>(getUint256ValueFunction, blockParameter);
        }

        
        public Task<BigInteger> GetUint256ValueQueryAsync(byte[] key, BlockParameter blockParameter = null)
        {
            var getUint256ValueFunction = new GetUint256ValueFunction();
                getUint256ValueFunction.Key = key;
            
            return ContractHandler.QueryAsync<GetUint256ValueFunction, BigInteger>(getUint256ValueFunction, blockParameter);
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

        public Task<string> SetAddressValueRequestAsync(SetAddressValueFunction setAddressValueFunction)
        {
             return ContractHandler.SendRequestAsync(setAddressValueFunction);
        }

        public Task<TransactionReceipt> SetAddressValueRequestAndWaitForReceiptAsync(SetAddressValueFunction setAddressValueFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setAddressValueFunction, cancellationToken);
        }

        public Task<string> SetAddressValueRequestAsync(byte[] key, string value)
        {
            var setAddressValueFunction = new SetAddressValueFunction();
                setAddressValueFunction.Key = key;
                setAddressValueFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setAddressValueFunction);
        }

        public Task<TransactionReceipt> SetAddressValueRequestAndWaitForReceiptAsync(byte[] key, string value, CancellationTokenSource cancellationToken = null)
        {
            var setAddressValueFunction = new SetAddressValueFunction();
                setAddressValueFunction.Key = key;
                setAddressValueFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setAddressValueFunction, cancellationToken);
        }

        public Task<string> SetBooleanValueRequestAsync(SetBooleanValueFunction setBooleanValueFunction)
        {
             return ContractHandler.SendRequestAsync(setBooleanValueFunction);
        }

        public Task<TransactionReceipt> SetBooleanValueRequestAndWaitForReceiptAsync(SetBooleanValueFunction setBooleanValueFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setBooleanValueFunction, cancellationToken);
        }

        public Task<string> SetBooleanValueRequestAsync(byte[] key, bool value)
        {
            var setBooleanValueFunction = new SetBooleanValueFunction();
                setBooleanValueFunction.Key = key;
                setBooleanValueFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setBooleanValueFunction);
        }

        public Task<TransactionReceipt> SetBooleanValueRequestAndWaitForReceiptAsync(byte[] key, bool value, CancellationTokenSource cancellationToken = null)
        {
            var setBooleanValueFunction = new SetBooleanValueFunction();
                setBooleanValueFunction.Key = key;
                setBooleanValueFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setBooleanValueFunction, cancellationToken);
        }

        public Task<string> SetBytes32ValueRequestAsync(SetBytes32ValueFunction setBytes32ValueFunction)
        {
             return ContractHandler.SendRequestAsync(setBytes32ValueFunction);
        }

        public Task<TransactionReceipt> SetBytes32ValueRequestAndWaitForReceiptAsync(SetBytes32ValueFunction setBytes32ValueFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setBytes32ValueFunction, cancellationToken);
        }

        public Task<string> SetBytes32ValueRequestAsync(byte[] key, byte[] value)
        {
            var setBytes32ValueFunction = new SetBytes32ValueFunction();
                setBytes32ValueFunction.Key = key;
                setBytes32ValueFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setBytes32ValueFunction);
        }

        public Task<TransactionReceipt> SetBytes32ValueRequestAndWaitForReceiptAsync(byte[] key, byte[] value, CancellationTokenSource cancellationToken = null)
        {
            var setBytes32ValueFunction = new SetBytes32ValueFunction();
                setBytes32ValueFunction.Key = key;
                setBytes32ValueFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setBytes32ValueFunction, cancellationToken);
        }

        public Task<string> SetInt256ValueRequestAsync(SetInt256ValueFunction setInt256ValueFunction)
        {
             return ContractHandler.SendRequestAsync(setInt256ValueFunction);
        }

        public Task<TransactionReceipt> SetInt256ValueRequestAndWaitForReceiptAsync(SetInt256ValueFunction setInt256ValueFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setInt256ValueFunction, cancellationToken);
        }

        public Task<string> SetInt256ValueRequestAsync(byte[] key, BigInteger value)
        {
            var setInt256ValueFunction = new SetInt256ValueFunction();
                setInt256ValueFunction.Key = key;
                setInt256ValueFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setInt256ValueFunction);
        }

        public Task<TransactionReceipt> SetInt256ValueRequestAndWaitForReceiptAsync(byte[] key, BigInteger value, CancellationTokenSource cancellationToken = null)
        {
            var setInt256ValueFunction = new SetInt256ValueFunction();
                setInt256ValueFunction.Key = key;
                setInt256ValueFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setInt256ValueFunction, cancellationToken);
        }

        public Task<string> SetMappingAddressToUint256ValueRequestAsync(SetMappingAddressToUint256ValueFunction setMappingAddressToUint256ValueFunction)
        {
             return ContractHandler.SendRequestAsync(setMappingAddressToUint256ValueFunction);
        }

        public Task<TransactionReceipt> SetMappingAddressToUint256ValueRequestAndWaitForReceiptAsync(SetMappingAddressToUint256ValueFunction setMappingAddressToUint256ValueFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMappingAddressToUint256ValueFunction, cancellationToken);
        }

        public Task<string> SetMappingAddressToUint256ValueRequestAsync(byte[] storageKey, string mappingKey, BigInteger value)
        {
            var setMappingAddressToUint256ValueFunction = new SetMappingAddressToUint256ValueFunction();
                setMappingAddressToUint256ValueFunction.StorageKey = storageKey;
                setMappingAddressToUint256ValueFunction.MappingKey = mappingKey;
                setMappingAddressToUint256ValueFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setMappingAddressToUint256ValueFunction);
        }

        public Task<TransactionReceipt> SetMappingAddressToUint256ValueRequestAndWaitForReceiptAsync(byte[] storageKey, string mappingKey, BigInteger value, CancellationTokenSource cancellationToken = null)
        {
            var setMappingAddressToUint256ValueFunction = new SetMappingAddressToUint256ValueFunction();
                setMappingAddressToUint256ValueFunction.StorageKey = storageKey;
                setMappingAddressToUint256ValueFunction.MappingKey = mappingKey;
                setMappingAddressToUint256ValueFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMappingAddressToUint256ValueFunction, cancellationToken);
        }

        public Task<string> SetMappingBytes32ToAddressValueRequestAsync(SetMappingBytes32ToAddressValueFunction setMappingBytes32ToAddressValueFunction)
        {
             return ContractHandler.SendRequestAsync(setMappingBytes32ToAddressValueFunction);
        }

        public Task<TransactionReceipt> SetMappingBytes32ToAddressValueRequestAndWaitForReceiptAsync(SetMappingBytes32ToAddressValueFunction setMappingBytes32ToAddressValueFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMappingBytes32ToAddressValueFunction, cancellationToken);
        }

        public Task<string> SetMappingBytes32ToAddressValueRequestAsync(byte[] storageKey, byte[] mappingKey, string value)
        {
            var setMappingBytes32ToAddressValueFunction = new SetMappingBytes32ToAddressValueFunction();
                setMappingBytes32ToAddressValueFunction.StorageKey = storageKey;
                setMappingBytes32ToAddressValueFunction.MappingKey = mappingKey;
                setMappingBytes32ToAddressValueFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setMappingBytes32ToAddressValueFunction);
        }

        public Task<TransactionReceipt> SetMappingBytes32ToAddressValueRequestAndWaitForReceiptAsync(byte[] storageKey, byte[] mappingKey, string value, CancellationTokenSource cancellationToken = null)
        {
            var setMappingBytes32ToAddressValueFunction = new SetMappingBytes32ToAddressValueFunction();
                setMappingBytes32ToAddressValueFunction.StorageKey = storageKey;
                setMappingBytes32ToAddressValueFunction.MappingKey = mappingKey;
                setMappingBytes32ToAddressValueFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMappingBytes32ToAddressValueFunction, cancellationToken);
        }

        public Task<string> SetMappingBytes32ToBoolValueRequestAsync(SetMappingBytes32ToBoolValueFunction setMappingBytes32ToBoolValueFunction)
        {
             return ContractHandler.SendRequestAsync(setMappingBytes32ToBoolValueFunction);
        }

        public Task<TransactionReceipt> SetMappingBytes32ToBoolValueRequestAndWaitForReceiptAsync(SetMappingBytes32ToBoolValueFunction setMappingBytes32ToBoolValueFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMappingBytes32ToBoolValueFunction, cancellationToken);
        }

        public Task<string> SetMappingBytes32ToBoolValueRequestAsync(byte[] storageKey, byte[] mappingKey, bool value)
        {
            var setMappingBytes32ToBoolValueFunction = new SetMappingBytes32ToBoolValueFunction();
                setMappingBytes32ToBoolValueFunction.StorageKey = storageKey;
                setMappingBytes32ToBoolValueFunction.MappingKey = mappingKey;
                setMappingBytes32ToBoolValueFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setMappingBytes32ToBoolValueFunction);
        }

        public Task<TransactionReceipt> SetMappingBytes32ToBoolValueRequestAndWaitForReceiptAsync(byte[] storageKey, byte[] mappingKey, bool value, CancellationTokenSource cancellationToken = null)
        {
            var setMappingBytes32ToBoolValueFunction = new SetMappingBytes32ToBoolValueFunction();
                setMappingBytes32ToBoolValueFunction.StorageKey = storageKey;
                setMappingBytes32ToBoolValueFunction.MappingKey = mappingKey;
                setMappingBytes32ToBoolValueFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMappingBytes32ToBoolValueFunction, cancellationToken);
        }

        public Task<string> SetMappingBytes32ToBytes32ValueRequestAsync(SetMappingBytes32ToBytes32ValueFunction setMappingBytes32ToBytes32ValueFunction)
        {
             return ContractHandler.SendRequestAsync(setMappingBytes32ToBytes32ValueFunction);
        }

        public Task<TransactionReceipt> SetMappingBytes32ToBytes32ValueRequestAndWaitForReceiptAsync(SetMappingBytes32ToBytes32ValueFunction setMappingBytes32ToBytes32ValueFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMappingBytes32ToBytes32ValueFunction, cancellationToken);
        }

        public Task<string> SetMappingBytes32ToBytes32ValueRequestAsync(byte[] storageKey, byte[] mappingKey, byte[] value)
        {
            var setMappingBytes32ToBytes32ValueFunction = new SetMappingBytes32ToBytes32ValueFunction();
                setMappingBytes32ToBytes32ValueFunction.StorageKey = storageKey;
                setMappingBytes32ToBytes32ValueFunction.MappingKey = mappingKey;
                setMappingBytes32ToBytes32ValueFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setMappingBytes32ToBytes32ValueFunction);
        }

        public Task<TransactionReceipt> SetMappingBytes32ToBytes32ValueRequestAndWaitForReceiptAsync(byte[] storageKey, byte[] mappingKey, byte[] value, CancellationTokenSource cancellationToken = null)
        {
            var setMappingBytes32ToBytes32ValueFunction = new SetMappingBytes32ToBytes32ValueFunction();
                setMappingBytes32ToBytes32ValueFunction.StorageKey = storageKey;
                setMappingBytes32ToBytes32ValueFunction.MappingKey = mappingKey;
                setMappingBytes32ToBytes32ValueFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMappingBytes32ToBytes32ValueFunction, cancellationToken);
        }

        public Task<string> SetMappingBytes32ToUint256ValueRequestAsync(SetMappingBytes32ToUint256ValueFunction setMappingBytes32ToUint256ValueFunction)
        {
             return ContractHandler.SendRequestAsync(setMappingBytes32ToUint256ValueFunction);
        }

        public Task<TransactionReceipt> SetMappingBytes32ToUint256ValueRequestAndWaitForReceiptAsync(SetMappingBytes32ToUint256ValueFunction setMappingBytes32ToUint256ValueFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMappingBytes32ToUint256ValueFunction, cancellationToken);
        }

        public Task<string> SetMappingBytes32ToUint256ValueRequestAsync(byte[] storageKey, byte[] mappingKey, BigInteger value)
        {
            var setMappingBytes32ToUint256ValueFunction = new SetMappingBytes32ToUint256ValueFunction();
                setMappingBytes32ToUint256ValueFunction.StorageKey = storageKey;
                setMappingBytes32ToUint256ValueFunction.MappingKey = mappingKey;
                setMappingBytes32ToUint256ValueFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setMappingBytes32ToUint256ValueFunction);
        }

        public Task<TransactionReceipt> SetMappingBytes32ToUint256ValueRequestAndWaitForReceiptAsync(byte[] storageKey, byte[] mappingKey, BigInteger value, CancellationTokenSource cancellationToken = null)
        {
            var setMappingBytes32ToUint256ValueFunction = new SetMappingBytes32ToUint256ValueFunction();
                setMappingBytes32ToUint256ValueFunction.StorageKey = storageKey;
                setMappingBytes32ToUint256ValueFunction.MappingKey = mappingKey;
                setMappingBytes32ToUint256ValueFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMappingBytes32ToUint256ValueFunction, cancellationToken);
        }

        public Task<string> SetStringValueRequestAsync(SetStringValueFunction setStringValueFunction)
        {
             return ContractHandler.SendRequestAsync(setStringValueFunction);
        }

        public Task<TransactionReceipt> SetStringValueRequestAndWaitForReceiptAsync(SetStringValueFunction setStringValueFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setStringValueFunction, cancellationToken);
        }

        public Task<string> SetStringValueRequestAsync(byte[] key, string value)
        {
            var setStringValueFunction = new SetStringValueFunction();
                setStringValueFunction.Key = key;
                setStringValueFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setStringValueFunction);
        }

        public Task<TransactionReceipt> SetStringValueRequestAndWaitForReceiptAsync(byte[] key, string value, CancellationTokenSource cancellationToken = null)
        {
            var setStringValueFunction = new SetStringValueFunction();
                setStringValueFunction.Key = key;
                setStringValueFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setStringValueFunction, cancellationToken);
        }

        public Task<string> SetUint256ValueRequestAsync(SetUint256ValueFunction setUint256ValueFunction)
        {
             return ContractHandler.SendRequestAsync(setUint256ValueFunction);
        }

        public Task<TransactionReceipt> SetUint256ValueRequestAndWaitForReceiptAsync(SetUint256ValueFunction setUint256ValueFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setUint256ValueFunction, cancellationToken);
        }

        public Task<string> SetUint256ValueRequestAsync(byte[] key, BigInteger value)
        {
            var setUint256ValueFunction = new SetUint256ValueFunction();
                setUint256ValueFunction.Key = key;
                setUint256ValueFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setUint256ValueFunction);
        }

        public Task<TransactionReceipt> SetUint256ValueRequestAndWaitForReceiptAsync(byte[] key, BigInteger value, CancellationTokenSource cancellationToken = null)
        {
            var setUint256ValueFunction = new SetUint256ValueFunction();
                setUint256ValueFunction.Key = key;
                setUint256ValueFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setUint256ValueFunction, cancellationToken);
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
