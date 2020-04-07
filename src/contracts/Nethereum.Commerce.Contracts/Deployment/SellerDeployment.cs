using Microsoft.Extensions.Logging;
using Nethereum.Commerce.Contracts.SellerAdmin;
using Nethereum.Commerce.Contracts.SellerAdmin.ContractDefinition;
using Nethereum.Web3;
using System.Threading.Tasks;

namespace Nethereum.Commerce.Contracts.Deployment
{
    /// <summary>
    /// Usage: call SellerDeployment.CreateFromNewDeployment() or .CreateFromConnectExistingContract() to
    /// create new deployment object, then call InitializeAsync() to set it up.
    /// </summary>
    public class SellerDeployment : ContractDeploymentBase
    {
        public SellerAdminService SellerAdminService { get; internal set; }
        public string SellerId { get; internal set; }

        private readonly string _businessPartnerStorageAddressGlobal;
        private readonly string _existingSellerContractAddress;
        private readonly string _sellerIdDesired;

        /// <summary>
        /// Deploy and configure a new SellerAdmin.sol contract
        /// </summary>
        public static SellerDeployment CreateFromNewDeployment(IWeb3 web3, string businessPartnerStorageAddressGlobal, string sellerId, ILogger logger = null)
        {
            return new SellerDeployment(web3, businessPartnerStorageAddressGlobal, sellerId, true, logger);
        }

        /// <summary>
        /// Connect to an existing SellerAdmin.sol contract
        /// </summary>
        public static SellerDeployment CreateFromConnectExistingContract(IWeb3 web3, string existingBuyerContractAddress, string sellerId, ILogger logger = null)
        {
            return new SellerDeployment(web3, existingBuyerContractAddress, sellerId, false, logger);
        }

        public SellerDeployment(
            IWeb3 web3,
            string address,
            string sellerId,
            bool isNewDeployment,
            ILogger logger = null)
            : base(web3, logger)
        {
            _isNewDeployment = isNewDeployment;
            if (_isNewDeployment)
            {
                // address represents the business partner storage address
                _businessPartnerStorageAddressGlobal = address;
            }
            else
            {
                // address represents the existing seller admin address
                _existingSellerContractAddress = address;
            }
            _sellerIdDesired = sellerId;
        }

        public async Task InitializeAsync()
        {
            var contractName = "SellerAdmin";
            if (_isNewDeployment)
            {
                // Deploy
                LogHeader($"Deploying {contractName}...");
                var sellerAdminDeployment = new SellerAdminDeployment()
                {
                    BusinessPartnerStorageAddressGlobal = _businessPartnerStorageAddressGlobal,
                    SellerIdString = _sellerIdDesired
                };
                SellerAdminService = await SellerAdminService.DeployContractAndGetServiceAsync(
                    _web3, sellerAdminDeployment).ConfigureAwait(false);
            }
            else
            {
                LogHeader($"Connecting to existing {contractName}...");
                SellerAdminService = new SellerAdminService(_web3, _existingSellerContractAddress);
            }

            // Check global business partner storage address
            var bpStorageAddress = await SellerAdminService.BusinessPartnerStorageGlobalQueryAsync().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(bpStorageAddress) || bpStorageAddress.IsZeroAddress())
            {
                throw new ContractDeploymentException($"Failed to set up {contractName}. Global business partner storage address must have a value.");
            }

            // Check owner address
            var sellerAdminOwner = await SellerAdminService.OwnerQueryAsync().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(sellerAdminOwner) || sellerAdminOwner.IsZeroAddress())
            {
                throw new ContractDeploymentException($"Failed to set up {contractName}. Owner address must have a value.");
            }
            Log($"{contractName} address is: {SellerAdminService.ContractHandler.ContractAddress}");
            Log($"{contractName} owner is  : {sellerAdminOwner}");
            Owner = sellerAdminOwner;

            // Check sellerId string
            SellerId = (await SellerAdminService.SellerIdQueryAsync().ConfigureAwait(false)).ConvertToString();
            if (string.IsNullOrWhiteSpace(SellerId))
            {
                throw new ContractDeploymentException($"Failed to set up {contractName}. SellerId must have a value.");
            }
            Log("Done");
        }
    }
}
