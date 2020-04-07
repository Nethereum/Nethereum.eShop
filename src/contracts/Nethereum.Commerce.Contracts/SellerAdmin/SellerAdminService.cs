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
using Nethereum.Commerce.Contracts.SellerAdmin.ContractDefinition;

namespace Nethereum.Commerce.Contracts.SellerAdmin
{
    public partial class SellerAdminService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, SellerAdminDeployment sellerAdminDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<SellerAdminDeployment>().SendRequestAndWaitForReceiptAsync(sellerAdminDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, SellerAdminDeployment sellerAdminDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<SellerAdminDeployment>().SendRequestAsync(sellerAdminDeployment);
        }

        public static async Task<SellerAdminService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, SellerAdminDeployment sellerAdminDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, sellerAdminDeployment, cancellationTokenSource);
            return new SellerAdminService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public SellerAdminService(Nethereum.Web3.Web3 web3, string contractAddress)
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

        public Task<string> EmitEventForNewPoRequestAsync(EmitEventForNewPoFunction emitEventForNewPoFunction)
        {
             return ContractHandler.SendRequestAsync(emitEventForNewPoFunction);
        }

        public Task<TransactionReceipt> EmitEventForNewPoRequestAndWaitForReceiptAsync(EmitEventForNewPoFunction emitEventForNewPoFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(emitEventForNewPoFunction, cancellationToken);
        }

        public Task<string> EmitEventForNewPoRequestAsync(Po po)
        {
            var emitEventForNewPoFunction = new EmitEventForNewPoFunction();
                emitEventForNewPoFunction.Po = po;
            
             return ContractHandler.SendRequestAsync(emitEventForNewPoFunction);
        }

        public Task<TransactionReceipt> EmitEventForNewPoRequestAndWaitForReceiptAsync(Po po, CancellationTokenSource cancellationToken = null)
        {
            var emitEventForNewPoFunction = new EmitEventForNewPoFunction();
                emitEventForNewPoFunction.Po = po;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(emitEventForNewPoFunction, cancellationToken);
        }

        public Task<GetPoOutputDTO> GetPoQueryAsync(GetPoFunction getPoFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetPoFunction, GetPoOutputDTO>(getPoFunction, blockParameter);
        }

        public Task<GetPoOutputDTO> GetPoQueryAsync(string eShopIdString, BigInteger poNumber, BlockParameter blockParameter = null)
        {
            var getPoFunction = new GetPoFunction();
                getPoFunction.EShopIdString = eShopIdString;
                getPoFunction.PoNumber = poNumber;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetPoFunction, GetPoOutputDTO>(getPoFunction, blockParameter);
        }

        public Task<GetPoByEshopIdAndQuoteOutputDTO> GetPoByEshopIdAndQuoteQueryAsync(GetPoByEshopIdAndQuoteFunction getPoByEshopIdAndQuoteFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetPoByEshopIdAndQuoteFunction, GetPoByEshopIdAndQuoteOutputDTO>(getPoByEshopIdAndQuoteFunction, blockParameter);
        }

        public Task<GetPoByEshopIdAndQuoteOutputDTO> GetPoByEshopIdAndQuoteQueryAsync(string eShopIdString, BigInteger quoteId, BlockParameter blockParameter = null)
        {
            var getPoByEshopIdAndQuoteFunction = new GetPoByEshopIdAndQuoteFunction();
                getPoByEshopIdAndQuoteFunction.EShopIdString = eShopIdString;
                getPoByEshopIdAndQuoteFunction.QuoteId = quoteId;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetPoByEshopIdAndQuoteFunction, GetPoByEshopIdAndQuoteOutputDTO>(getPoByEshopIdAndQuoteFunction, blockParameter);
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

        public Task<string> ReconfigureRequestAsync(string businessPartnerStorageAddressGlobal)
        {
            var reconfigureFunction = new ReconfigureFunction();
                reconfigureFunction.BusinessPartnerStorageAddressGlobal = businessPartnerStorageAddressGlobal;
            
             return ContractHandler.SendRequestAsync(reconfigureFunction);
        }

        public Task<TransactionReceipt> ReconfigureRequestAndWaitForReceiptAsync(string businessPartnerStorageAddressGlobal, CancellationTokenSource cancellationToken = null)
        {
            var reconfigureFunction = new ReconfigureFunction();
                reconfigureFunction.BusinessPartnerStorageAddressGlobal = businessPartnerStorageAddressGlobal;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(reconfigureFunction, cancellationToken);
        }

        public Task<byte[]> SellerIdQueryAsync(SellerIdFunction sellerIdFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SellerIdFunction, byte[]>(sellerIdFunction, blockParameter);
        }

        
        public Task<byte[]> SellerIdQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SellerIdFunction, byte[]>(null, blockParameter);
        }

        public Task<string> SetPoItemAcceptedRequestAsync(SetPoItemAcceptedFunction setPoItemAcceptedFunction)
        {
             return ContractHandler.SendRequestAsync(setPoItemAcceptedFunction);
        }

        public Task<TransactionReceipt> SetPoItemAcceptedRequestAndWaitForReceiptAsync(SetPoItemAcceptedFunction setPoItemAcceptedFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemAcceptedFunction, cancellationToken);
        }

        public Task<string> SetPoItemAcceptedRequestAsync(string eShopIdString, BigInteger poNumber, byte poItemNumber, byte[] soNumber, byte[] soItemNumber)
        {
            var setPoItemAcceptedFunction = new SetPoItemAcceptedFunction();
                setPoItemAcceptedFunction.EShopIdString = eShopIdString;
                setPoItemAcceptedFunction.PoNumber = poNumber;
                setPoItemAcceptedFunction.PoItemNumber = poItemNumber;
                setPoItemAcceptedFunction.SoNumber = soNumber;
                setPoItemAcceptedFunction.SoItemNumber = soItemNumber;
            
             return ContractHandler.SendRequestAsync(setPoItemAcceptedFunction);
        }

        public Task<TransactionReceipt> SetPoItemAcceptedRequestAndWaitForReceiptAsync(string eShopIdString, BigInteger poNumber, byte poItemNumber, byte[] soNumber, byte[] soItemNumber, CancellationTokenSource cancellationToken = null)
        {
            var setPoItemAcceptedFunction = new SetPoItemAcceptedFunction();
                setPoItemAcceptedFunction.EShopIdString = eShopIdString;
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

        public Task<string> SetPoItemCompletedRequestAsync(string eShopIdString, BigInteger poNumber, byte poItemNumber)
        {
            var setPoItemCompletedFunction = new SetPoItemCompletedFunction();
                setPoItemCompletedFunction.EShopIdString = eShopIdString;
                setPoItemCompletedFunction.PoNumber = poNumber;
                setPoItemCompletedFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAsync(setPoItemCompletedFunction);
        }

        public Task<TransactionReceipt> SetPoItemCompletedRequestAndWaitForReceiptAsync(string eShopIdString, BigInteger poNumber, byte poItemNumber, CancellationTokenSource cancellationToken = null)
        {
            var setPoItemCompletedFunction = new SetPoItemCompletedFunction();
                setPoItemCompletedFunction.EShopIdString = eShopIdString;
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

        public Task<string> SetPoItemGoodsIssuedRequestAsync(string eShopIdString, BigInteger poNumber, byte poItemNumber)
        {
            var setPoItemGoodsIssuedFunction = new SetPoItemGoodsIssuedFunction();
                setPoItemGoodsIssuedFunction.EShopIdString = eShopIdString;
                setPoItemGoodsIssuedFunction.PoNumber = poNumber;
                setPoItemGoodsIssuedFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAsync(setPoItemGoodsIssuedFunction);
        }

        public Task<TransactionReceipt> SetPoItemGoodsIssuedRequestAndWaitForReceiptAsync(string eShopIdString, BigInteger poNumber, byte poItemNumber, CancellationTokenSource cancellationToken = null)
        {
            var setPoItemGoodsIssuedFunction = new SetPoItemGoodsIssuedFunction();
                setPoItemGoodsIssuedFunction.EShopIdString = eShopIdString;
                setPoItemGoodsIssuedFunction.PoNumber = poNumber;
                setPoItemGoodsIssuedFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemGoodsIssuedFunction, cancellationToken);
        }

        public Task<string> SetPoItemGoodsReceivedRequestAsync(SetPoItemGoodsReceivedFunction setPoItemGoodsReceivedFunction)
        {
             return ContractHandler.SendRequestAsync(setPoItemGoodsReceivedFunction);
        }

        public Task<TransactionReceipt> SetPoItemGoodsReceivedRequestAndWaitForReceiptAsync(SetPoItemGoodsReceivedFunction setPoItemGoodsReceivedFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemGoodsReceivedFunction, cancellationToken);
        }

        public Task<string> SetPoItemGoodsReceivedRequestAsync(string eShopIdString, BigInteger poNumber, byte poItemNumber)
        {
            var setPoItemGoodsReceivedFunction = new SetPoItemGoodsReceivedFunction();
                setPoItemGoodsReceivedFunction.EShopIdString = eShopIdString;
                setPoItemGoodsReceivedFunction.PoNumber = poNumber;
                setPoItemGoodsReceivedFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAsync(setPoItemGoodsReceivedFunction);
        }

        public Task<TransactionReceipt> SetPoItemGoodsReceivedRequestAndWaitForReceiptAsync(string eShopIdString, BigInteger poNumber, byte poItemNumber, CancellationTokenSource cancellationToken = null)
        {
            var setPoItemGoodsReceivedFunction = new SetPoItemGoodsReceivedFunction();
                setPoItemGoodsReceivedFunction.EShopIdString = eShopIdString;
                setPoItemGoodsReceivedFunction.PoNumber = poNumber;
                setPoItemGoodsReceivedFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemGoodsReceivedFunction, cancellationToken);
        }

        public Task<string> SetPoItemReadyForGoodsIssueRequestAsync(SetPoItemReadyForGoodsIssueFunction setPoItemReadyForGoodsIssueFunction)
        {
             return ContractHandler.SendRequestAsync(setPoItemReadyForGoodsIssueFunction);
        }

        public Task<TransactionReceipt> SetPoItemReadyForGoodsIssueRequestAndWaitForReceiptAsync(SetPoItemReadyForGoodsIssueFunction setPoItemReadyForGoodsIssueFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPoItemReadyForGoodsIssueFunction, cancellationToken);
        }

        public Task<string> SetPoItemReadyForGoodsIssueRequestAsync(string eShopIdString, BigInteger poNumber, byte poItemNumber)
        {
            var setPoItemReadyForGoodsIssueFunction = new SetPoItemReadyForGoodsIssueFunction();
                setPoItemReadyForGoodsIssueFunction.EShopIdString = eShopIdString;
                setPoItemReadyForGoodsIssueFunction.PoNumber = poNumber;
                setPoItemReadyForGoodsIssueFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAsync(setPoItemReadyForGoodsIssueFunction);
        }

        public Task<TransactionReceipt> SetPoItemReadyForGoodsIssueRequestAndWaitForReceiptAsync(string eShopIdString, BigInteger poNumber, byte poItemNumber, CancellationTokenSource cancellationToken = null)
        {
            var setPoItemReadyForGoodsIssueFunction = new SetPoItemReadyForGoodsIssueFunction();
                setPoItemReadyForGoodsIssueFunction.EShopIdString = eShopIdString;
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

        public Task<string> SetPoItemRejectedRequestAsync(string eShopIdString, BigInteger poNumber, byte poItemNumber)
        {
            var setPoItemRejectedFunction = new SetPoItemRejectedFunction();
                setPoItemRejectedFunction.EShopIdString = eShopIdString;
                setPoItemRejectedFunction.PoNumber = poNumber;
                setPoItemRejectedFunction.PoItemNumber = poItemNumber;
            
             return ContractHandler.SendRequestAsync(setPoItemRejectedFunction);
        }

        public Task<TransactionReceipt> SetPoItemRejectedRequestAndWaitForReceiptAsync(string eShopIdString, BigInteger poNumber, byte poItemNumber, CancellationTokenSource cancellationToken = null)
        {
            var setPoItemRejectedFunction = new SetPoItemRejectedFunction();
                setPoItemRejectedFunction.EShopIdString = eShopIdString;
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
