using Microsoft.Extensions.Logging;
using Nethereum.Commerce.Contracts.BuyerWallet;
using Nethereum.Commerce.Contracts.BuyerWallet.ContractDefinition;
using Nethereum.Web3;
using System.Threading.Tasks;

namespace Nethereum.Commerce.Contracts.Deployment
{
    /// <summary>
    /// Deploys a new BuyerWallet.sol contract or connects to an existing one.
    /// Throws ContractDeploymentException if contract is not setup correctly.
    /// Usage: call BuyerDeployment.CreateFromNewDeployment() or .CreateFromConnectExistingContract() to
    /// create new deployment object, then call InitializeAsync() to set it up.
    /// </summary>
    public class BuyerDeployment : ContractDeploymentBase
    {
        public BuyerWalletService BuyerWalletService { get; internal set; }

        private readonly string _businessPartnerStorageAddressGlobal;
        private readonly string _existingBuyerContractAddress;

        /// <summary>
        /// Deploy a new BuyerWallet.sol contract
        /// </summary>
        public static BuyerDeployment CreateFromNewDeployment(IWeb3 web3, string businessPartnerStorageAddressGlobal, ILogger logger = null)
        {
            return new BuyerDeployment(web3, businessPartnerStorageAddressGlobal, true, logger);
        }

        /// <summary>
        /// Connect to an existing BuyerWallet.sol contract
        /// </summary>
        public static BuyerDeployment CreateFromConnectExistingContract(IWeb3 web3, string existingBuyerContractAddress, ILogger logger = null)
        {
            return new BuyerDeployment(web3, existingBuyerContractAddress, false, logger);
        }

        private BuyerDeployment(
            IWeb3 web3,
            string address,
            bool isNewDeployment,
            ILogger logger = null)
            : base(web3, logger)
        {         
            if (string.IsNullOrWhiteSpace(address) || address.IsZeroAddress())
            {
                throw new ContractDeploymentException($"Failed to set up {GetType().Name}. Address must be specified.");
            }
            _isNewDeployment = isNewDeployment;
            if (_isNewDeployment)
            {
                // address represents the business partner storage address
                _businessPartnerStorageAddressGlobal = address;
            }
            else
            {
                // address represents the existing buyer wallet address
                _existingBuyerContractAddress = address;
            }
        }

        public async Task InitializeAsync()
        {
            var contractName = GetType().Name;
            if (_isNewDeployment)
            {
                LogHeader($"Deploying {contractName}...");
                var buyerWalletDeployment = new BuyerWalletDeployment()
                {
                    BusinessPartnerStorageAddressGlobal = _businessPartnerStorageAddressGlobal
                };
                BuyerWalletService = await BuyerWalletService.DeployContractAndGetServiceAsync(
                    _web3, buyerWalletDeployment).ConfigureAwait(false);
            }
            else
            {
                LogHeader($"Connecting to existing {contractName}...");
                BuyerWalletService = new BuyerWalletService(_web3, _existingBuyerContractAddress);
            }

            // Check global business partner storage address and contract
            var bpStorageAddress = await BuyerWalletService.BusinessPartnerStorageGlobalQueryAsync().ConfigureAwait(false);
            await ValidateBusinessPartnerStorageAddressAsync(bpStorageAddress).ConfigureAwait(false);

            // Check buyer wallet owner address
            var buyerWalletOwnerAddress = await BuyerWalletService.OwnerQueryAsync().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(buyerWalletOwnerAddress) || buyerWalletOwnerAddress.IsZeroAddress())
            {
                throw new ContractDeploymentException($"Failed to set up {contractName}. Owner address must have a value.");
            }
            Log($"{contractName} address is: {BuyerWalletService.ContractHandler.ContractAddress}");
            Log($"{contractName} owner is  : {buyerWalletOwnerAddress}");
            Owner = buyerWalletOwnerAddress;
            Log("Done");
        }
    }
}
