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
using Nethereum.Commerce.Contracts.WalletBuyer.ContractDefinition;

namespace Nethereum.Commerce.Contracts.WalletBuyer
{
    public partial class WalletBuyerService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, WalletBuyerDeployment walletBuyerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<WalletBuyerDeployment>().SendRequestAndWaitForReceiptAsync(walletBuyerDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, WalletBuyerDeployment walletBuyerDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<WalletBuyerDeployment>().SendRequestAsync(walletBuyerDeployment);
        }

        public static async Task<WalletBuyerService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, WalletBuyerDeployment walletBuyerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, walletBuyerDeployment, cancellationTokenSource);
            return new WalletBuyerService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public WalletBuyerService(Nethereum.Web3.Web3 web3, string contractAddress)
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

        public Task<string> CancelPurchaseOrderItemRequestAsync(CancelPurchaseOrderItemFunction cancelPurchaseOrderItemFunction)
        {
             return ContractHandler.SendRequestAsync(cancelPurchaseOrderItemFunction);
        }

        public Task<TransactionReceipt> CancelPurchaseOrderItemRequestAndWaitForReceiptAsync(CancelPurchaseOrderItemFunction cancelPurchaseOrderItemFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelPurchaseOrderItemFunction, cancellationToken);
        }

        public Task<string> CancelPurchaseOrderItemRequestAsync(BigInteger poNumber, byte poItemNumber)
        {
            var cancelPurchaseOrderItemFunction = new CancelPurchaseOrderItemFunction();
                cancelPurchaseOrderItemFunction.PoNumber = poNumber;
                cancelPurchaseOrderItemFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAsync(cancelPurchaseOrderItemFunction);
        }

        public Task<TransactionReceipt> CancelPurchaseOrderItemRequestAndWaitForReceiptAsync(BigInteger poNumber, byte poItemNumber, CancellationTokenSource cancellationToken = null)
        {
            var cancelPurchaseOrderItemFunction = new CancelPurchaseOrderItemFunction();
                cancelPurchaseOrderItemFunction.PoNumber = poNumber;
                cancelPurchaseOrderItemFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelPurchaseOrderItemFunction, cancellationToken);
        }

        public Task<string> ConfigureRequestAsync(ConfigureFunction configureFunction)
        {
             return ContractHandler.SendRequestAsync(configureFunction);
        }

        public Task<TransactionReceipt> ConfigureRequestAndWaitForReceiptAsync(ConfigureFunction configureFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(configureFunction, cancellationToken);
        }

        public Task<string> ConfigureRequestAsync(string nameOfPurchasing, string nameOfFunding)
        {
            var configureFunction = new ConfigureFunction();
                configureFunction.NameOfPurchasing = nameOfPurchasing;
                configureFunction.NameOfFunding = nameOfFunding;
            
             return ContractHandler.SendRequestAsync(configureFunction);
        }

        public Task<TransactionReceipt> ConfigureRequestAndWaitForReceiptAsync(string nameOfPurchasing, string nameOfFunding, CancellationTokenSource cancellationToken = null)
        {
            var configureFunction = new ConfigureFunction();
                configureFunction.NameOfPurchasing = nameOfPurchasing;
                configureFunction.NameOfFunding = nameOfFunding;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(configureFunction, cancellationToken);
        }

        public Task<string> CreatePurchaseOrderRequestAsync(CreatePurchaseOrderFunction createPurchaseOrderFunction)
        {
             return ContractHandler.SendRequestAsync(createPurchaseOrderFunction);
        }

        public Task<TransactionReceipt> CreatePurchaseOrderRequestAndWaitForReceiptAsync(CreatePurchaseOrderFunction createPurchaseOrderFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createPurchaseOrderFunction, cancellationToken);
        }

        public Task<string> CreatePurchaseOrderRequestAsync(Po po)
        {
            var createPurchaseOrderFunction = new CreatePurchaseOrderFunction();
                createPurchaseOrderFunction.Po = po;
            
             return ContractHandler.SendRequestAsync(createPurchaseOrderFunction);
        }

        public Task<TransactionReceipt> CreatePurchaseOrderRequestAndWaitForReceiptAsync(Po po, CancellationTokenSource cancellationToken = null)
        {
            var createPurchaseOrderFunction = new CreatePurchaseOrderFunction();
                createPurchaseOrderFunction.Po = po;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createPurchaseOrderFunction, cancellationToken);
        }

        public Task<string> FundingQueryAsync(FundingFunction fundingFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FundingFunction, string>(fundingFunction, blockParameter);
        }

        
        public Task<string> FundingQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FundingFunction, string>(null, blockParameter);
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

        public Task<GetPoBySellerAndQuoteOutputDTO> GetPoBySellerAndQuoteQueryAsync(GetPoBySellerAndQuoteFunction getPoBySellerAndQuoteFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetPoBySellerAndQuoteFunction, GetPoBySellerAndQuoteOutputDTO>(getPoBySellerAndQuoteFunction, blockParameter);
        }

        public Task<GetPoBySellerAndQuoteOutputDTO> GetPoBySellerAndQuoteQueryAsync(string sellerIdString, BigInteger quoteId, BlockParameter blockParameter = null)
        {
            var getPoBySellerAndQuoteFunction = new GetPoBySellerAndQuoteFunction();
                getPoBySellerAndQuoteFunction.SellerIdString = sellerIdString;
                getPoBySellerAndQuoteFunction.QuoteId = quoteId;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetPoBySellerAndQuoteFunction, GetPoBySellerAndQuoteOutputDTO>(getPoBySellerAndQuoteFunction, blockParameter);
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

        public Task<string> PurchasingQueryAsync(PurchasingFunction purchasingFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PurchasingFunction, string>(purchasingFunction, blockParameter);
        }

        
        public Task<string> PurchasingQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PurchasingFunction, string>(null, blockParameter);
        }

        public Task<string> SetPoItemGoodsReceivedRequestAsync(SetPoItemGoodsReceivedFunction setPoItemGoodsReceivedFunction)
        {
             return ContractHandler.SendRequestAsync(setPoItemGoodsReceivedFunction);
        }

        public Task<TransactionReceipt> SetPoItemGoodsReceivedRequestAndWaitForReceiptAsync(SetPoItemGoodsReceivedFunction setPoItemGoodsReceivedFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemGoodsReceivedFunction, cancellationToken);
        }

        public Task<string> SetPoItemGoodsReceivedRequestAsync(BigInteger poNumber, byte poItemNumber)
        {
            var setPoItemGoodsReceivedFunction = new SetPoItemGoodsReceivedFunction();
                setPoItemGoodsReceivedFunction.PoNumber = poNumber;
                setPoItemGoodsReceivedFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAsync(setPoItemGoodsReceivedFunction);
        }

        public Task<TransactionReceipt> SetPoItemGoodsReceivedRequestAndWaitForReceiptAsync(BigInteger poNumber, byte poItemNumber, CancellationTokenSource cancellationToken = null)
        {
            var setPoItemGoodsReceivedFunction = new SetPoItemGoodsReceivedFunction();
                setPoItemGoodsReceivedFunction.PoNumber = poNumber;
                setPoItemGoodsReceivedFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemGoodsReceivedFunction, cancellationToken);
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
