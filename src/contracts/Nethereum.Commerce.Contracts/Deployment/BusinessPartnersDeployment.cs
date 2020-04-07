using Microsoft.Extensions.Logging;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition;
using Nethereum.Commerce.Contracts.EternalStorage;
using Nethereum.Commerce.Contracts.EternalStorage.ContractDefinition;
using Nethereum.Web3;
using System.Threading.Tasks;

namespace Nethereum.Commerce.Contracts.Deployment
{
    /// <summary>
    /// Usage: call BusinessPartnerStorageDeployment.CreateFromNewDeployment() or .CreateFromConnectExistingContract() to
    /// create new deployment object, then call InitializeAsync() to set it up.
    /// </summary>
    public class BusinessPartnersDeployment : ContractDeploymentBase
    {
        public BusinessPartnerStorageService BusinessPartnerStorageService { get; internal set; }

        private readonly string _existingBusinessPartnerContractAddress;

        /// <summary>
        /// Deploy a new BusinessPartnerStorageDeployment.sol contract and its supporting EternalStorage.sol.
        /// </summary>
        public static BusinessPartnersDeployment CreateFromNewDeployment(IWeb3 web3, ILogger logger = null)
        {
            return new BusinessPartnersDeployment(web3, logger);
        }

        /// <summary>
        /// Connect to an existing BusinessPartnerStorageDeployment.sol contract
        /// </summary>
        public static BusinessPartnersDeployment CreateFromConnectExistingContract(IWeb3 web3, string existingBusinessPartnerContractAddress, ILogger logger = null)
        {
            return new BusinessPartnersDeployment(web3, existingBusinessPartnerContractAddress, logger);
        }

        public BusinessPartnersDeployment(IWeb3 web3, ILogger logger = null)
            : base(web3, logger)
        {
            _isNewDeployment = true;
        }

        public BusinessPartnersDeployment(IWeb3 web3, string address, ILogger logger = null)
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
            }
            else
            {
                LogHeader($"Connecting to existing {contractName}...");
                BusinessPartnerStorageService = new BusinessPartnerStorageService(_web3, _existingBusinessPartnerContractAddress);
            }

            // Check eternal storage address
            var eternalStorageAddress = await BusinessPartnerStorageService.EternalStorageQueryAsync().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(eternalStorageAddress) || eternalStorageAddress.IsZeroAddress())
            {
                throw new ContractDeploymentException($"Failed to set up {contractName}. Eternal storage address must have a value.");
            }

            // Check owner address
            var bpStorageOwner = await BusinessPartnerStorageService.OwnerQueryAsync().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(bpStorageOwner) || bpStorageOwner.IsZeroAddress())
            {
                throw new ContractDeploymentException($"Failed to set up {contractName}. Owner address must have a value.");
            }
            Log($"{contractName} address is: {BusinessPartnerStorageService.ContractHandler.ContractAddress}");
            Log($"{contractName} owner is  : {bpStorageOwner}");
            Owner = bpStorageOwner;

            Log("Done");
        }
    }
}
