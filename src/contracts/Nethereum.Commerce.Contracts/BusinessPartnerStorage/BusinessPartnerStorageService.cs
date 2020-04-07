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
    public partial class BusinessPartnerStorageService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, BusinessPartnerStorageDeployment businessPartnerStorageDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<BusinessPartnerStorageDeployment>().SendRequestAndWaitForReceiptAsync(businessPartnerStorageDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, BusinessPartnerStorageDeployment businessPartnerStorageDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<BusinessPartnerStorageDeployment>().SendRequestAsync(businessPartnerStorageDeployment);
        }

        public static async Task<BusinessPartnerStorageService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, BusinessPartnerStorageDeployment businessPartnerStorageDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, businessPartnerStorageDeployment, cancellationTokenSource);
            return new BusinessPartnerStorageService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public BusinessPartnerStorageService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
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

        public Task<string> EternalStorageQueryAsync(EternalStorageFunction eternalStorageFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<EternalStorageFunction, string>(eternalStorageFunction, blockParameter);
        }

        
        public Task<string> EternalStorageQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<EternalStorageFunction, string>(null, blockParameter);
        }

        public Task<GetEshopOutputDTO> GetEshopQueryAsync(GetEshopFunction getEshopFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetEshopFunction, GetEshopOutputDTO>(getEshopFunction, blockParameter);
        }

        public Task<GetEshopOutputDTO> GetEshopQueryAsync(byte[] eShopId, BlockParameter blockParameter = null)
        {
            var getEshopFunction = new GetEshopFunction();
                getEshopFunction.EShopId = eShopId;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetEshopFunction, GetEshopOutputDTO>(getEshopFunction, blockParameter);
        }

        public Task<GetSellerOutputDTO> GetSellerQueryAsync(GetSellerFunction getSellerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetSellerFunction, GetSellerOutputDTO>(getSellerFunction, blockParameter);
        }

        public Task<GetSellerOutputDTO> GetSellerQueryAsync(byte[] sellerId, BlockParameter blockParameter = null)
        {
            var getSellerFunction = new GetSellerFunction();
                getSellerFunction.SellerId = sellerId;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetSellerFunction, GetSellerOutputDTO>(getSellerFunction, blockParameter);
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

        public Task<string> ReconfigureRequestAsync(ReconfigureFunction reconfigureFunction)
        {
             return ContractHandler.SendRequestAsync(reconfigureFunction);
        }

        public Task<TransactionReceipt> ReconfigureRequestAndWaitForReceiptAsync(ReconfigureFunction reconfigureFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(reconfigureFunction, cancellationToken);
        }

        public Task<string> ReconfigureRequestAsync(string eternalStorageAddress)
        {
            var reconfigureFunction = new ReconfigureFunction();
                reconfigureFunction.EternalStorageAddress = eternalStorageAddress;
            
             return ContractHandler.SendRequestAsync(reconfigureFunction);
        }

        public Task<TransactionReceipt> ReconfigureRequestAndWaitForReceiptAsync(string eternalStorageAddress, CancellationTokenSource cancellationToken = null)
        {
            var reconfigureFunction = new ReconfigureFunction();
                reconfigureFunction.EternalStorageAddress = eternalStorageAddress;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(reconfigureFunction, cancellationToken);
        }

        public Task<string> SetEshopRequestAsync(SetEshopFunction setEshopFunction)
        {
             return ContractHandler.SendRequestAsync(setEshopFunction);
        }

        public Task<TransactionReceipt> SetEshopRequestAndWaitForReceiptAsync(SetEshopFunction setEshopFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setEshopFunction, cancellationToken);
        }

        public Task<string> SetEshopRequestAsync(Eshop eShop)
        {
            var setEshopFunction = new SetEshopFunction();
                setEshopFunction.EShop = eShop;
            
             return ContractHandler.SendRequestAsync(setEshopFunction);
        }

        public Task<TransactionReceipt> SetEshopRequestAndWaitForReceiptAsync(Eshop eShop, CancellationTokenSource cancellationToken = null)
        {
            var setEshopFunction = new SetEshopFunction();
                setEshopFunction.EShop = eShop;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setEshopFunction, cancellationToken);
        }

        public Task<string> SetSellerRequestAsync(SetSellerFunction setSellerFunction)
        {
             return ContractHandler.SendRequestAsync(setSellerFunction);
        }

        public Task<TransactionReceipt> SetSellerRequestAndWaitForReceiptAsync(SetSellerFunction setSellerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setSellerFunction, cancellationToken);
        }

        public Task<string> SetSellerRequestAsync(Seller seller)
        {
            var setSellerFunction = new SetSellerFunction();
                setSellerFunction.Seller = seller;
            
             return ContractHandler.SendRequestAsync(setSellerFunction);
        }

        public Task<TransactionReceipt> SetSellerRequestAndWaitForReceiptAsync(Seller seller, CancellationTokenSource cancellationToken = null)
        {
            var setSellerFunction = new SetSellerFunction();
                setSellerFunction.Seller = seller;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setSellerFunction, cancellationToken);
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
