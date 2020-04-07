//using Microsoft.Extensions.Logging;
//using Nethereum.Commerce.Contracts.BuyerWallet;
//using Nethereum.Commerce.Contracts.BuyerWallet.ContractDefinition;
//using Nethereum.Web3;
//using System.Threading.Tasks;

//namespace Nethereum.Commerce.Contracts.Deployment
//{
//    public class BuyerDeployment : ContractDeploymentBase
//    {
//        public BuyerWalletService BuyerWalletService { get; internal set; }

//        private readonly string _addressOfAddressRegistryGlobal;
//        private readonly string _nameOfBusinessPartnerStorageGlobal;
//        private readonly string _addressOfExistingBuyerContract;

//        /// <summary>
//        /// Deploy and configure a new BuyerWallet.sol contract
//        /// </summary>
//        public BuyerDeployment(
//            IWeb3 web3,
//            string addressOfAddressRegistryGlobal,
//            string nameOfBusinessPartnerStorageGlobal,
//            ILogger logger = null)
//            : base(web3, logger)
//        {
//            _isNewDeployment = true;
//            _addressOfAddressRegistryGlobal = addressOfAddressRegistryGlobal;
//            _nameOfBusinessPartnerStorageGlobal = nameOfBusinessPartnerStorageGlobal;
//        }

//        /// <summary>
//        /// Connect to an existing BuyerWallet.sol contract
//        /// </summary>
//        public BuyerDeployment(
//            IWeb3 web3,
//            string addressOfExistingBuyerContract,
//            ILogger logger = null)
//            : base(web3, logger)
//        {
//            _isNewDeployment = false;
//            _addressOfExistingBuyerContract = addressOfExistingBuyerContract;
//        }

//        public async Task InitializeAsync()
//        {
//            var contractName = "BuyerWallet";
//            if (_isNewDeployment)
//            {
//                // Deploy
//                LogHeader($"Deploying {contractName}...");
//                var buyerWalletDeployment = new BuyerWalletDeployment()
//                {
//                    ContractAddressOfRegistryGlobal = _addressOfAddressRegistryGlobal
//                };
//                BuyerWalletService = await BuyerWalletService.DeployContractAndGetServiceAsync(
//                    _web3, buyerWalletDeployment).ConfigureAwait(false);

//                // Configure
//                Log();
//                Log($"Configuring {contractName}...");
//                var txReceipt = await BuyerWalletService.ConfigureRequestAndWaitForReceiptAsync(
//                    _nameOfBusinessPartnerStorageGlobal).ConfigureAwait(false);
//                Log($"Tx status: {txReceipt.Status.Value}");
//            }
//            else
//            {
//                LogHeader($"Connecting to existing {contractName}...");
//                BuyerWalletService = new BuyerWalletService(_web3, _addressOfExistingBuyerContract);
//            }

//            // Check owner
//            var buyerWalletOwner = await BuyerWalletService.OwnerQueryAsync().ConfigureAwait(false);
//            if (string.IsNullOrWhiteSpace(buyerWalletOwner))
//            {
//                throw new ContractDeploymentException($"Failed to set up {contractName}");
//            }
//            Log($"{contractName} address is: {BuyerWalletService.ContractHandler.ContractAddress}");
//            Log($"{contractName} owner is  : {buyerWalletOwner}");
//            Owner = buyerWalletOwner;
//            Log("Done");
//        }
//    }
//}
