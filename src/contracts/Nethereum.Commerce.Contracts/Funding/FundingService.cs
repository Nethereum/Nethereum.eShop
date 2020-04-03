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
using Nethereum.Commerce.Contracts.Funding.ContractDefinition;

namespace Nethereum.Commerce.Contracts.Funding
{
    public partial class FundingService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, FundingDeployment fundingDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<FundingDeployment>().SendRequestAndWaitForReceiptAsync(fundingDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, FundingDeployment fundingDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<FundingDeployment>().SendRequestAsync(fundingDeployment);
        }

        public static async Task<FundingService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, FundingDeployment fundingDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, fundingDeployment, cancellationTokenSource);
            return new FundingService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public FundingService(Nethereum.Web3.Web3 web3, string contractAddress)
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

        public Task<string> AddressRegistryGlobalQueryAsync(AddressRegistryGlobalFunction addressRegistryGlobalFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AddressRegistryGlobalFunction, string>(addressRegistryGlobalFunction, blockParameter);
        }

        
        public Task<string> AddressRegistryGlobalQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AddressRegistryGlobalFunction, string>(null, blockParameter);
        }

        public Task<string> AddressRegistryLocalQueryAsync(AddressRegistryLocalFunction addressRegistryLocalFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AddressRegistryLocalFunction, string>(addressRegistryLocalFunction, blockParameter);
        }

        
        public Task<string> AddressRegistryLocalQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AddressRegistryLocalFunction, string>(null, blockParameter);
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

        public Task<string> BpStorageGlobalQueryAsync(BpStorageGlobalFunction bpStorageGlobalFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BpStorageGlobalFunction, string>(bpStorageGlobalFunction, blockParameter);
        }

        
        public Task<string> BpStorageGlobalQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BpStorageGlobalFunction, string>(null, blockParameter);
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

        public Task<string> ConfigureRequestAsync(string nameOfBusinessPartnerStorageGlobal, string nameOfPurchasingLocal)
        {
            var configureFunction = new ConfigureFunction();
                configureFunction.NameOfBusinessPartnerStorageGlobal = nameOfBusinessPartnerStorageGlobal;
                configureFunction.NameOfPurchasingLocal = nameOfPurchasingLocal;
            
             return ContractHandler.SendRequestAsync(configureFunction);
        }

        public Task<TransactionReceipt> ConfigureRequestAndWaitForReceiptAsync(string nameOfBusinessPartnerStorageGlobal, string nameOfPurchasingLocal, CancellationTokenSource cancellationToken = null)
        {
            var configureFunction = new ConfigureFunction();
                configureFunction.NameOfBusinessPartnerStorageGlobal = nameOfBusinessPartnerStorageGlobal;
                configureFunction.NameOfPurchasingLocal = nameOfPurchasingLocal;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(configureFunction, cancellationToken);
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

        public Task<string> PurchasingContractAddressLocalQueryAsync(PurchasingContractAddressLocalFunction purchasingContractAddressLocalFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PurchasingContractAddressLocalFunction, string>(purchasingContractAddressLocalFunction, blockParameter);
        }

        
        public Task<string> PurchasingContractAddressLocalQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PurchasingContractAddressLocalFunction, string>(null, blockParameter);
        }

        public Task<string> PurchasingLocalQueryAsync(PurchasingLocalFunction purchasingLocalFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PurchasingLocalFunction, string>(purchasingLocalFunction, blockParameter);
        }

        
        public Task<string> PurchasingLocalQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PurchasingLocalFunction, string>(null, blockParameter);
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

        public Task<string> TransferInFundsForPoFromBuyerWalletRequestAsync(TransferInFundsForPoFromBuyerWalletFunction transferInFundsForPoFromBuyerWalletFunction)
        {
             return ContractHandler.SendRequestAsync(transferInFundsForPoFromBuyerWalletFunction);
        }

        public Task<TransactionReceipt> TransferInFundsForPoFromBuyerWalletRequestAndWaitForReceiptAsync(TransferInFundsForPoFromBuyerWalletFunction transferInFundsForPoFromBuyerWalletFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferInFundsForPoFromBuyerWalletFunction, cancellationToken);
        }

        public Task<string> TransferInFundsForPoFromBuyerWalletRequestAsync(BigInteger poNumber)
        {
            var transferInFundsForPoFromBuyerWalletFunction = new TransferInFundsForPoFromBuyerWalletFunction();
                transferInFundsForPoFromBuyerWalletFunction.PoNumber = poNumber;
            
             return ContractHandler.SendRequestAsync(transferInFundsForPoFromBuyerWalletFunction);
        }

        public Task<TransactionReceipt> TransferInFundsForPoFromBuyerWalletRequestAndWaitForReceiptAsync(BigInteger poNumber, CancellationTokenSource cancellationToken = null)
        {
            var transferInFundsForPoFromBuyerWalletFunction = new TransferInFundsForPoFromBuyerWalletFunction();
                transferInFundsForPoFromBuyerWalletFunction.PoNumber = poNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferInFundsForPoFromBuyerWalletFunction, cancellationToken);
        }

        public Task<string> TransferOutFundsForPoItemToBuyerRequestAsync(TransferOutFundsForPoItemToBuyerFunction transferOutFundsForPoItemToBuyerFunction)
        {
             return ContractHandler.SendRequestAsync(transferOutFundsForPoItemToBuyerFunction);
        }

        public Task<TransactionReceipt> TransferOutFundsForPoItemToBuyerRequestAndWaitForReceiptAsync(TransferOutFundsForPoItemToBuyerFunction transferOutFundsForPoItemToBuyerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOutFundsForPoItemToBuyerFunction, cancellationToken);
        }

        public Task<string> TransferOutFundsForPoItemToBuyerRequestAsync(BigInteger poNumber, byte poItemNumber)
        {
            var transferOutFundsForPoItemToBuyerFunction = new TransferOutFundsForPoItemToBuyerFunction();
                transferOutFundsForPoItemToBuyerFunction.PoNumber = poNumber;
                transferOutFundsForPoItemToBuyerFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAsync(transferOutFundsForPoItemToBuyerFunction);
        }

        public Task<TransactionReceipt> TransferOutFundsForPoItemToBuyerRequestAndWaitForReceiptAsync(BigInteger poNumber, byte poItemNumber, CancellationTokenSource cancellationToken = null)
        {
            var transferOutFundsForPoItemToBuyerFunction = new TransferOutFundsForPoItemToBuyerFunction();
                transferOutFundsForPoItemToBuyerFunction.PoNumber = poNumber;
                transferOutFundsForPoItemToBuyerFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOutFundsForPoItemToBuyerFunction, cancellationToken);
        }

        public Task<string> TransferOutFundsForPoItemToSellerRequestAsync(TransferOutFundsForPoItemToSellerFunction transferOutFundsForPoItemToSellerFunction)
        {
             return ContractHandler.SendRequestAsync(transferOutFundsForPoItemToSellerFunction);
        }

        public Task<TransactionReceipt> TransferOutFundsForPoItemToSellerRequestAndWaitForReceiptAsync(TransferOutFundsForPoItemToSellerFunction transferOutFundsForPoItemToSellerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOutFundsForPoItemToSellerFunction, cancellationToken);
        }

        public Task<string> TransferOutFundsForPoItemToSellerRequestAsync(BigInteger poNumber, byte poItemNumber)
        {
            var transferOutFundsForPoItemToSellerFunction = new TransferOutFundsForPoItemToSellerFunction();
                transferOutFundsForPoItemToSellerFunction.PoNumber = poNumber;
                transferOutFundsForPoItemToSellerFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAsync(transferOutFundsForPoItemToSellerFunction);
        }

        public Task<TransactionReceipt> TransferOutFundsForPoItemToSellerRequestAndWaitForReceiptAsync(BigInteger poNumber, byte poItemNumber, CancellationTokenSource cancellationToken = null)
        {
            var transferOutFundsForPoItemToSellerFunction = new TransferOutFundsForPoItemToSellerFunction();
                transferOutFundsForPoItemToSellerFunction.PoNumber = poNumber;
                transferOutFundsForPoItemToSellerFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOutFundsForPoItemToSellerFunction, cancellationToken);
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
