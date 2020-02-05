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
using Nethereum.Commerce.Contracts.WalletSeller.ContractDefinition;

namespace Nethereum.Commerce.Contracts.WalletSeller
{
    public partial class WalletSellerService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, WalletSellerDeployment walletSellerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<WalletSellerDeployment>().SendRequestAndWaitForReceiptAsync(walletSellerDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, WalletSellerDeployment walletSellerDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<WalletSellerDeployment>().SendRequestAsync(walletSellerDeployment);
        }

        public static async Task<WalletSellerService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, WalletSellerDeployment walletSellerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, walletSellerDeployment, cancellationTokenSource);
            return new WalletSellerService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public WalletSellerService(Nethereum.Web3.Web3 web3, string contractAddress)
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

        public Task<string> ConfigureRequestAsync(string sellerIdString, string nameOfPurchasing, string nameOfFunding)
        {
            var configureFunction = new ConfigureFunction();
                configureFunction.SellerIdString = sellerIdString;
                configureFunction.NameOfPurchasing = nameOfPurchasing;
                configureFunction.NameOfFunding = nameOfFunding;
            
             return ContractHandler.SendRequestAsync(configureFunction);
        }

        public Task<TransactionReceipt> ConfigureRequestAndWaitForReceiptAsync(string sellerIdString, string nameOfPurchasing, string nameOfFunding, CancellationTokenSource cancellationToken = null)
        {
            var configureFunction = new ConfigureFunction();
                configureFunction.SellerIdString = sellerIdString;
                configureFunction.NameOfPurchasing = nameOfPurchasing;
                configureFunction.NameOfFunding = nameOfFunding;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(configureFunction, cancellationToken);
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
