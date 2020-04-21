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
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;

namespace Nethereum.Commerce.Contracts.Purchasing
{
    public partial class PurchasingService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, PurchasingDeployment purchasingDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<PurchasingDeployment>().SendRequestAndWaitForReceiptAsync(purchasingDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, PurchasingDeployment purchasingDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<PurchasingDeployment>().SendRequestAsync(purchasingDeployment);
        }

        public static async Task<PurchasingService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, PurchasingDeployment purchasingDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, purchasingDeployment, cancellationTokenSource);
            return new PurchasingService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public PurchasingService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> AddressRegistryLocalQueryAsync(AddressRegistryLocalFunction addressRegistryLocalFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AddressRegistryLocalFunction, string>(addressRegistryLocalFunction, blockParameter);
        }

        
        public Task<string> AddressRegistryLocalQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AddressRegistryLocalFunction, string>(null, blockParameter);
        }

        public Task<string> BusinessPartnerStorageGlobalQueryAsync(BusinessPartnerStorageGlobalFunction businessPartnerStorageGlobalFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BusinessPartnerStorageGlobalFunction, string>(businessPartnerStorageGlobalFunction, blockParameter);
        }

        
        public Task<string> BusinessPartnerStorageGlobalQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BusinessPartnerStorageGlobalFunction, string>(null, blockParameter);
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

        public Task<string> ConfigureRequestAsync(string businessPartnerStorageAddressGlobal, string nameOfPoStorageLocal, string nameOfFundingLocal)
        {
            var configureFunction = new ConfigureFunction();
                configureFunction.BusinessPartnerStorageAddressGlobal = businessPartnerStorageAddressGlobal;
                configureFunction.NameOfPoStorageLocal = nameOfPoStorageLocal;
                configureFunction.NameOfFundingLocal = nameOfFundingLocal;
            
             return ContractHandler.SendRequestAsync(configureFunction);
        }

        public Task<TransactionReceipt> ConfigureRequestAndWaitForReceiptAsync(string businessPartnerStorageAddressGlobal, string nameOfPoStorageLocal, string nameOfFundingLocal, CancellationTokenSource cancellationToken = null)
        {
            var configureFunction = new ConfigureFunction();
                configureFunction.BusinessPartnerStorageAddressGlobal = businessPartnerStorageAddressGlobal;
                configureFunction.NameOfPoStorageLocal = nameOfPoStorageLocal;
                configureFunction.NameOfFundingLocal = nameOfFundingLocal;
            
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

        public Task<string> CreatePurchaseOrderRequestAsync(Po po, byte[] signature)
        {
            var createPurchaseOrderFunction = new CreatePurchaseOrderFunction();
                createPurchaseOrderFunction.Po = po;
                createPurchaseOrderFunction.Signature = signature;
            
             return ContractHandler.SendRequestAsync(createPurchaseOrderFunction);
        }

        public Task<TransactionReceipt> CreatePurchaseOrderRequestAndWaitForReceiptAsync(Po po, byte[] signature, CancellationTokenSource cancellationToken = null)
        {
            var createPurchaseOrderFunction = new CreatePurchaseOrderFunction();
                createPurchaseOrderFunction.Po = po;
                createPurchaseOrderFunction.Signature = signature;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createPurchaseOrderFunction, cancellationToken);
        }

        public Task<byte[]> EShopIdQueryAsync(EShopIdFunction eShopIdFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<EShopIdFunction, byte[]>(eShopIdFunction, blockParameter);
        }

        
        public Task<byte[]> EShopIdQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<EShopIdFunction, byte[]>(null, blockParameter);
        }

        public Task<string> FundingLocalQueryAsync(FundingLocalFunction fundingLocalFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FundingLocalFunction, string>(fundingLocalFunction, blockParameter);
        }

        
        public Task<string> FundingLocalQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FundingLocalFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> GetEscrowTimeoutDaysQueryAsync(GetEscrowTimeoutDaysFunction getEscrowTimeoutDaysFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetEscrowTimeoutDaysFunction, BigInteger>(getEscrowTimeoutDaysFunction, blockParameter);
        }

        
        public Task<BigInteger> GetEscrowTimeoutDaysQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetEscrowTimeoutDaysFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> GetFeeBasisPointsQueryAsync(GetFeeBasisPointsFunction getFeeBasisPointsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetFeeBasisPointsFunction, BigInteger>(getFeeBasisPointsFunction, blockParameter);
        }

        
        public Task<BigInteger> GetFeeBasisPointsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetFeeBasisPointsFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> GetFundingQueryAsync(GetFundingFunction getFundingFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetFundingFunction, string>(getFundingFunction, blockParameter);
        }

        
        public Task<string> GetFundingQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetFundingFunction, string>(null, blockParameter);
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

        public Task<GetPoByQuoteOutputDTO> GetPoByQuoteQueryAsync(GetPoByQuoteFunction getPoByQuoteFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetPoByQuoteFunction, GetPoByQuoteOutputDTO>(getPoByQuoteFunction, blockParameter);
        }

        public Task<GetPoByQuoteOutputDTO> GetPoByQuoteQueryAsync(BigInteger quoteId, BlockParameter blockParameter = null)
        {
            var getPoByQuoteFunction = new GetPoByQuoteFunction();
                getPoByQuoteFunction.QuoteId = quoteId;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetPoByQuoteFunction, GetPoByQuoteOutputDTO>(getPoByQuoteFunction, blockParameter);
        }

        public Task<string> GetSignerAddressFromPoAndSignatureQueryAsync(GetSignerAddressFromPoAndSignatureFunction getSignerAddressFromPoAndSignatureFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetSignerAddressFromPoAndSignatureFunction, string>(getSignerAddressFromPoAndSignatureFunction, blockParameter);
        }

        
        public Task<string> GetSignerAddressFromPoAndSignatureQueryAsync(Po po, byte[] signature, BlockParameter blockParameter = null)
        {
            var getSignerAddressFromPoAndSignatureFunction = new GetSignerAddressFromPoAndSignatureFunction();
                getSignerAddressFromPoAndSignatureFunction.Po = po;
                getSignerAddressFromPoAndSignatureFunction.Signature = signature;
            
            return ContractHandler.QueryAsync<GetSignerAddressFromPoAndSignatureFunction, string>(getSignerAddressFromPoAndSignatureFunction, blockParameter);
        }

        public Task<bool> IsConfiguredQueryAsync(IsConfiguredFunction isConfiguredFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IsConfiguredFunction, bool>(isConfiguredFunction, blockParameter);
        }

        
        public Task<bool> IsConfiguredQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IsConfiguredFunction, bool>(null, blockParameter);
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

        public Task<string> PoStorageLocalQueryAsync(PoStorageLocalFunction poStorageLocalFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PoStorageLocalFunction, string>(poStorageLocalFunction, blockParameter);
        }

        
        public Task<string> PoStorageLocalQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PoStorageLocalFunction, string>(null, blockParameter);
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
