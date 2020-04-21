using Microsoft.Extensions.Logging;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition;
using Nethereum.Commerce.Contracts.EternalStorage;
using Nethereum.Commerce.Contracts.EternalStorage.ContractDefinition;
using Nethereum.Util;
using Nethereum.Web3;
using System.Threading.Tasks;

namespace Nethereum.Commerce.Contracts.Deployment
{
    /// <summary>
    /// Deploys a new BusinessPartnerStorage.sol contract along with its Eternal Storage, or connects to an existing one.
    /// Throws if contract is not setup correctly.
    /// Usage: call BusinessPartnerStorageDeployment.CreateFromNewDeployment() or .CreateFromConnectExistingContract() to
    /// create new deployment object, then call InitializeAsync() to set it up.
    /// </summary>
    public class BusinessPartnersDeployment : ContractDeploymentBase, IBusinessPartnersDeployment
    {
        public BusinessPartnerStorageService BusinessPartnerStorageService { get; internal set; }
        public string Owner { get; internal set; }

        private readonly string _existingBusinessPartnerContractAddress;

        /// <summary>
        /// Deploy a new BusinessPartnerStorageDeployment.sol contract and its supporting EternalStorage.sol.
        /// </summary>
        public static IBusinessPartnersDeployment CreateFromNewDeployment(IWeb3 web3, ILogger logger = null)
        {
            return new BusinessPartnersDeployment(web3, logger);
        }

        /// <summary>
        /// Connect to an existing BusinessPartnerStorageDeployment.sol contract
        /// </summary>
        public static IBusinessPartnersDeployment CreateFromConnectExistingContract(IWeb3 web3, string existingBusinessPartnerContractAddress, ILogger logger = null)
        {
            return new BusinessPartnersDeployment(web3, existingBusinessPartnerContractAddress, logger);
        }

        private BusinessPartnersDeployment(IWeb3 web3, ILogger logger = null)
            : base(web3, logger)
        {
            _isNewDeployment = true;
        }

        private BusinessPartnersDeployment(IWeb3 web3, string address, ILogger logger = null)
            : base(web3, logger)
        {
            _isNewDeployment = false;
            _existingBusinessPartnerContractAddress = address;
        }

        public async Task InitializeAsync()
        {
            var contractName = "BusinessPartnerStorage";
            if (_isNewDeployment)
            {
                // Deploy Eternal Storage
                LogHeader($"Deploying EternalStorage for {contractName}...");
                var eternalStorageDeployment = new EternalStorageDeployment();
                var eternalStorageService = await EternalStorageService.DeployContractAndGetServiceAsync(
                    _web3, eternalStorageDeployment).ConfigureAwait(false);

                // Deploy Business Partner Storage       
                LogHeader($"Deploying {contractName}...");
                var businessPartnerStorageDeployment = new BusinessPartnerStorageDeployment()
                {
                    EternalStorageAddress = eternalStorageService.ContractHandler.ContractAddress
                };
                BusinessPartnerStorageService = await BusinessPartnerStorageService.DeployContractAndGetServiceAsync(
                    _web3, businessPartnerStorageDeployment).ConfigureAwait(false);

                // Bind business partner storage as a user of eternal storage
                LogHeader($"Authorisations for Global Eternal Storage...");
                Log($"Configuring Global Eternal Storage, binding {contractName}...");
                var txReceipt = await eternalStorageService.BindAddressRequestAndWaitForReceiptAsync(
                    BusinessPartnerStorageService.ContractHandler.ContractAddress).ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");
            }
            else
            {
                LogHeader($"Connecting to existing {contractName}...");
                BusinessPartnerStorageService = new BusinessPartnerStorageService(_web3, _existingBusinessPartnerContractAddress);
            }

            // Check eternal storage address
            var eternalStorageAddress = await BusinessPartnerStorageService.EternalStorageQueryAsync().ConfigureAwait(false);
            if (!eternalStorageAddress.IsValidNonZeroAddress())
            {
                throw new ContractDeploymentException($"Failed to set up {contractName}. Eternal storage address {eternalStorageAddress} not valid hex format.");
            }

            // Check owner address
            var bpStorageOwner = await BusinessPartnerStorageService.OwnerQueryAsync().ConfigureAwait(false);
            if (!bpStorageOwner.IsValidNonZeroAddress())
            {
                throw new ContractDeploymentException($"Failed to set up {contractName}. Owner address {bpStorageOwner} not valid hex format.");
            }
            Log($"{contractName} address is: {BusinessPartnerStorageService.ContractHandler.ContractAddress}");
            Log($"{contractName} owner is  : {bpStorageOwner}");
            Owner = bpStorageOwner;

            Log("Done");
        }
    }
}
