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
using Nethereum.Commerce.Contracts.IPurchasing.ContractDefinition;

namespace Nethereum.Commerce.Contracts.IPurchasing
{
    public partial class IPurchasingService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, IPurchasingDeployment iPurchasingDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<IPurchasingDeployment>().SendRequestAndWaitForReceiptAsync(iPurchasingDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, IPurchasingDeployment iPurchasingDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<IPurchasingDeployment>().SendRequestAsync(iPurchasingDeployment);
        }

        public static async Task<IPurchasingService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, IPurchasingDeployment iPurchasingDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, iPurchasingDeployment, cancellationTokenSource);
            return new IPurchasingService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public IPurchasingService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
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

        public Task<string> ConfigureRequestAsync(string nameOfPoStorage, string nameOfBusinessPartnerStorage, string nameOfFunding)
        {
            var configureFunction = new ConfigureFunction();
                configureFunction.NameOfPoStorage = nameOfPoStorage;
                configureFunction.NameOfBusinessPartnerStorage = nameOfBusinessPartnerStorage;
                configureFunction.NameOfFunding = nameOfFunding;
            
             return ContractHandler.SendRequestAsync(configureFunction);
        }

        public Task<TransactionReceipt> ConfigureRequestAndWaitForReceiptAsync(string nameOfPoStorage, string nameOfBusinessPartnerStorage, string nameOfFunding, CancellationTokenSource cancellationToken = null)
        {
            var configureFunction = new ConfigureFunction();
                configureFunction.NameOfPoStorage = nameOfPoStorage;
                configureFunction.NameOfBusinessPartnerStorage = nameOfBusinessPartnerStorage;
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

        public Task<string> SetPoItemAcceptedRequestAsync(SetPoItemAcceptedFunction setPoItemAcceptedFunction)
        {
             return ContractHandler.SendRequestAsync(setPoItemAcceptedFunction);
        }

        public Task<TransactionReceipt> SetPoItemAcceptedRequestAndWaitForReceiptAsync(SetPoItemAcceptedFunction setPoItemAcceptedFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemAcceptedFunction, cancellationToken);
        }

        public Task<string> SetPoItemAcceptedRequestAsync(BigInteger poNumber, byte poItemNumber, byte[] soNumber, byte[] soItemNumber)
        {
            var setPoItemAcceptedFunction = new SetPoItemAcceptedFunction();
                setPoItemAcceptedFunction.PoNumber = poNumber;
                setPoItemAcceptedFunction.PoItemNumber = poItemNumber;
                setPoItemAcceptedFunction.SoNumber = soNumber;
                setPoItemAcceptedFunction.SoItemNumber = soItemNumber;
            
             return ContractHandler.SendRequestAsync(setPoItemAcceptedFunction);
        }

        public Task<TransactionReceipt> SetPoItemAcceptedRequestAndWaitForReceiptAsync(BigInteger poNumber, byte poItemNumber, byte[] soNumber, byte[] soItemNumber, CancellationTokenSource cancellationToken = null)
        {
            var setPoItemAcceptedFunction = new SetPoItemAcceptedFunction();
                setPoItemAcceptedFunction.PoNumber = poNumber;
                setPoItemAcceptedFunction.PoItemNumber = poItemNumber;
                setPoItemAcceptedFunction.SoNumber = soNumber;
                setPoItemAcceptedFunction.SoItemNumber = soItemNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemAcceptedFunction, cancellationToken);
        }

        public Task<string> SetPoItemCompletedRequestAsync(SetPoItemCompletedFunction setPoItemCompletedFunction)
        {
             return ContractHandler.SendRequestAsync(setPoItemCompletedFunction);
        }

        public Task<TransactionReceipt> SetPoItemCompletedRequestAndWaitForReceiptAsync(SetPoItemCompletedFunction setPoItemCompletedFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemCompletedFunction, cancellationToken);
        }

        public Task<string> SetPoItemCompletedRequestAsync(BigInteger poNumber, byte poItemNumber)
        {
            var setPoItemCompletedFunction = new SetPoItemCompletedFunction();
                setPoItemCompletedFunction.PoNumber = poNumber;
                setPoItemCompletedFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAsync(setPoItemCompletedFunction);
        }

        public Task<TransactionReceipt> SetPoItemCompletedRequestAndWaitForReceiptAsync(BigInteger poNumber, byte poItemNumber, CancellationTokenSource cancellationToken = null)
        {
            var setPoItemCompletedFunction = new SetPoItemCompletedFunction();
                setPoItemCompletedFunction.PoNumber = poNumber;
                setPoItemCompletedFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemCompletedFunction, cancellationToken);
        }

        public Task<string> SetPoItemGoodsIssuedRequestAsync(SetPoItemGoodsIssuedFunction setPoItemGoodsIssuedFunction)
        {
             return ContractHandler.SendRequestAsync(setPoItemGoodsIssuedFunction);
        }

        public Task<TransactionReceipt> SetPoItemGoodsIssuedRequestAndWaitForReceiptAsync(SetPoItemGoodsIssuedFunction setPoItemGoodsIssuedFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemGoodsIssuedFunction, cancellationToken);
        }

        public Task<string> SetPoItemGoodsIssuedRequestAsync(BigInteger poNumber, byte poItemNumber)
        {
            var setPoItemGoodsIssuedFunction = new SetPoItemGoodsIssuedFunction();
                setPoItemGoodsIssuedFunction.PoNumber = poNumber;
                setPoItemGoodsIssuedFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAsync(setPoItemGoodsIssuedFunction);
        }

        public Task<TransactionReceipt> SetPoItemGoodsIssuedRequestAndWaitForReceiptAsync(BigInteger poNumber, byte poItemNumber, CancellationTokenSource cancellationToken = null)
        {
            var setPoItemGoodsIssuedFunction = new SetPoItemGoodsIssuedFunction();
                setPoItemGoodsIssuedFunction.PoNumber = poNumber;
                setPoItemGoodsIssuedFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemGoodsIssuedFunction, cancellationToken);
        }

        public Task<string> SetPoItemGoodsReceivedBuyerRequestAsync(SetPoItemGoodsReceivedBuyerFunction setPoItemGoodsReceivedBuyerFunction)
        {
             return ContractHandler.SendRequestAsync(setPoItemGoodsReceivedBuyerFunction);
        }

        public Task<TransactionReceipt> SetPoItemGoodsReceivedBuyerRequestAndWaitForReceiptAsync(SetPoItemGoodsReceivedBuyerFunction setPoItemGoodsReceivedBuyerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemGoodsReceivedBuyerFunction, cancellationToken);
        }

        public Task<string> SetPoItemGoodsReceivedBuyerRequestAsync(BigInteger poNumber, byte poItemNumber)
        {
            var setPoItemGoodsReceivedBuyerFunction = new SetPoItemGoodsReceivedBuyerFunction();
                setPoItemGoodsReceivedBuyerFunction.PoNumber = poNumber;
                setPoItemGoodsReceivedBuyerFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAsync(setPoItemGoodsReceivedBuyerFunction);
        }

        public Task<TransactionReceipt> SetPoItemGoodsReceivedBuyerRequestAndWaitForReceiptAsync(BigInteger poNumber, byte poItemNumber, CancellationTokenSource cancellationToken = null)
        {
            var setPoItemGoodsReceivedBuyerFunction = new SetPoItemGoodsReceivedBuyerFunction();
                setPoItemGoodsReceivedBuyerFunction.PoNumber = poNumber;
                setPoItemGoodsReceivedBuyerFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemGoodsReceivedBuyerFunction, cancellationToken);
        }

        public Task<string> SetPoItemGoodsReceivedSellerRequestAsync(SetPoItemGoodsReceivedSellerFunction setPoItemGoodsReceivedSellerFunction)
        {
             return ContractHandler.SendRequestAsync(setPoItemGoodsReceivedSellerFunction);
        }

        public Task<TransactionReceipt> SetPoItemGoodsReceivedSellerRequestAndWaitForReceiptAsync(SetPoItemGoodsReceivedSellerFunction setPoItemGoodsReceivedSellerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemGoodsReceivedSellerFunction, cancellationToken);
        }

        public Task<string> SetPoItemGoodsReceivedSellerRequestAsync(BigInteger poNumber, byte poItemNumber)
        {
            var setPoItemGoodsReceivedSellerFunction = new SetPoItemGoodsReceivedSellerFunction();
                setPoItemGoodsReceivedSellerFunction.PoNumber = poNumber;
                setPoItemGoodsReceivedSellerFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAsync(setPoItemGoodsReceivedSellerFunction);
        }

        public Task<TransactionReceipt> SetPoItemGoodsReceivedSellerRequestAndWaitForReceiptAsync(BigInteger poNumber, byte poItemNumber, CancellationTokenSource cancellationToken = null)
        {
            var setPoItemGoodsReceivedSellerFunction = new SetPoItemGoodsReceivedSellerFunction();
                setPoItemGoodsReceivedSellerFunction.PoNumber = poNumber;
                setPoItemGoodsReceivedSellerFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemGoodsReceivedSellerFunction, cancellationToken);
        }

        public Task<string> SetPoItemReadyForGoodsIssueRequestAsync(SetPoItemReadyForGoodsIssueFunction setPoItemReadyForGoodsIssueFunction)
        {
             return ContractHandler.SendRequestAsync(setPoItemReadyForGoodsIssueFunction);
        }

        public Task<TransactionReceipt> SetPoItemReadyForGoodsIssueRequestAndWaitForReceiptAsync(SetPoItemReadyForGoodsIssueFunction setPoItemReadyForGoodsIssueFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemReadyForGoodsIssueFunction, cancellationToken);
        }

        public Task<string> SetPoItemReadyForGoodsIssueRequestAsync(BigInteger poNumber, byte poItemNumber)
        {
            var setPoItemReadyForGoodsIssueFunction = new SetPoItemReadyForGoodsIssueFunction();
                setPoItemReadyForGoodsIssueFunction.PoNumber = poNumber;
                setPoItemReadyForGoodsIssueFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAsync(setPoItemReadyForGoodsIssueFunction);
        }

        public Task<TransactionReceipt> SetPoItemReadyForGoodsIssueRequestAndWaitForReceiptAsync(BigInteger poNumber, byte poItemNumber, CancellationTokenSource cancellationToken = null)
        {
            var setPoItemReadyForGoodsIssueFunction = new SetPoItemReadyForGoodsIssueFunction();
                setPoItemReadyForGoodsIssueFunction.PoNumber = poNumber;
                setPoItemReadyForGoodsIssueFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemReadyForGoodsIssueFunction, cancellationToken);
        }

        public Task<string> SetPoItemRejectedRequestAsync(SetPoItemRejectedFunction setPoItemRejectedFunction)
        {
             return ContractHandler.SendRequestAsync(setPoItemRejectedFunction);
        }

        public Task<TransactionReceipt> SetPoItemRejectedRequestAndWaitForReceiptAsync(SetPoItemRejectedFunction setPoItemRejectedFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemRejectedFunction, cancellationToken);
        }

        public Task<string> SetPoItemRejectedRequestAsync(BigInteger poNumber, byte poItemNumber)
        {
            var setPoItemRejectedFunction = new SetPoItemRejectedFunction();
                setPoItemRejectedFunction.PoNumber = poNumber;
                setPoItemRejectedFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAsync(setPoItemRejectedFunction);
        }

        public Task<TransactionReceipt> SetPoItemRejectedRequestAndWaitForReceiptAsync(BigInteger poNumber, byte poItemNumber, CancellationTokenSource cancellationToken = null)
        {
            var setPoItemRejectedFunction = new SetPoItemRejectedFunction();
                setPoItemRejectedFunction.PoNumber = poNumber;
                setPoItemRejectedFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemRejectedFunction, cancellationToken);
        }
    }
}
