using Microsoft.Extensions.Logging;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage;
using Nethereum.Commerce.Contracts.BuyerWallet;
using Nethereum.Commerce.Contracts.BuyerWallet.ContractDefinition;
using Nethereum.Util;
using Nethereum.Web3;
using System.Threading.Tasks;

namespace Nethereum.Commerce.Contracts.Deployment
{
    public class BuyerDeployment : ContractDeploymentBase, IBuyerDeployment
    {
        public BuyerWalletService BuyerWalletService { get; internal set; }
        public BusinessPartnerStorageService BusinessPartnerStorageGlobalService { get; internal set; }
        public string Owner { get; internal set; }

        private readonly string _businessPartnerStorageAddressGlobal;
        private readonly string _existingBuyerContractAddress;

        /// <summary>
        /// Deploy a new BuyerWallet.sol contract
        /// </summary>
        public static IBuyerDeployment CreateFromNewDeployment(IWeb3 web3, string businessPartnerStorageAddressGlobal, ILogger logger = null)
        {
            return new BuyerDeployment(web3, businessPartnerStorageAddressGlobal, true, logger);
        }

        /// <summary>
        /// Connect to an existing BuyerWallet.sol contract
        /// </summary>
        public static IBuyerDeployment CreateFromConnectExistingContract(IWeb3 web3, string existingBuyerContractAddress, ILogger logger = null)
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
            if (!address.IsValidNonZeroAddress())
            {
                throw new ContractDeploymentException($"Failed to set up {GetType().Name}. Address {address} is zero or not valid hex format.");
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

            // Validate Global Business Partner Storage and get the service for it
            string bpStorageAddress;
            if (_isNewDeployment)
            {
                bpStorageAddress = _businessPartnerStorageAddressGlobal;
            }
            else
            {
                LogHeader($"Connecting to existing {contractName}...");
                BuyerWalletService = new BuyerWalletService(_web3, _existingBuyerContractAddress);
                bpStorageAddress = await BuyerWalletService.BusinessPartnerStorageGlobalQueryAsync().ConfigureAwait(false);
            }
            BusinessPartnerStorageGlobalService = await GetValidBusinessPartnerStorageServiceAsync(bpStorageAddress).ConfigureAwait(false);

            // Do new deployment
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

            // Check buyer wallet owner address
            var buyerWalletOwnerAddress = await BuyerWalletService.OwnerQueryAsync().ConfigureAwait(false);
            if (!buyerWalletOwnerAddress.IsValidNonZeroAddress())
            {
                throw new ContractDeploymentException($"Failed to set up {contractName}. Owner address {buyerWalletOwnerAddress} is zero or not valid hex format.");
            }
            Log($"{contractName} address is: {BuyerWalletService.ContractHandler.ContractAddress}");
            Log($"{contractName} owner is  : {buyerWalletOwnerAddress}");
            Owner = buyerWalletOwnerAddress;
            Log("Done");
        }
    }
}
