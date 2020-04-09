using Microsoft.Extensions.Logging;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition;
using Nethereum.Commerce.Contracts.SellerAdmin;
using Nethereum.Commerce.Contracts.SellerAdmin.ContractDefinition;
using Nethereum.Contracts;
using Nethereum.Web3;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.Commerce.Contracts.Deployment
{
    /// <summary>
    /// Deploys a new SellerAdmin.sol contract and creates its master data, or connects to an existing one.
    /// Throws ContractDeploymentException or SmartContractRevertException if contract is not setup correctly.
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
        private readonly string _sellerDescriptionDesired;

        /// <summary>
        /// Deploy and configure a new SellerAdmin.sol contract
        /// </summary>
        public static SellerDeployment CreateFromNewDeployment(IWeb3 web3, string businessPartnerStorageAddressGlobal, string sellerId, string sellerDescription = null, ILogger logger = null)
        {
            return new SellerDeployment(web3, businessPartnerStorageAddressGlobal, sellerId, sellerDescription, true, logger);
        }

        /// <summary>
        /// Connect to an existing SellerAdmin.sol contract
        /// </summary>
        public static SellerDeployment CreateFromConnectExistingContract(IWeb3 web3, string existingBuyerContractAddress, ILogger logger = null)
        {
            return new SellerDeployment(web3, existingBuyerContractAddress, null, null, false, logger);
        }

        private SellerDeployment(
            IWeb3 web3,
            string address,
            string sellerId,
            string sellerDescription,
            bool isNewDeployment,
            ILogger logger = null)
            : base(web3, logger)
        {
            if (string.IsNullOrWhiteSpace(address) || address.IsZeroAddress())
            {
                throw new ContractDeploymentException($"Failed to set up {GetType().Name}. Address must be specified.");
            }
            _sellerIdDesired = sellerId;
            _sellerDescriptionDesired = sellerDescription ?? sellerId;
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
        }

        public async Task InitializeAsync()
        {
            var contractName = GetType().Name;
            if (_isNewDeployment)
            {
                // Deploy
                LogHeader($"Deploying {contractName}...");
                var sellerAdminDeployment = new SellerAdminDeployment()
                {
                    SellerIdString = _sellerIdDesired
                };
                SellerAdminService = await SellerAdminService.DeployContractAndGetServiceAsync(
                    _web3, sellerAdminDeployment).ConfigureAwait(false);

                // Create master data for sellerIdDesired
                LogHeader($"Creating master data record in global business partner storage...");
                var bpss = new BusinessPartnerStorageService(_web3, _businessPartnerStorageAddressGlobal);
                var seller = new Seller()
                {
                    SellerId = _sellerIdDesired,
                    SellerDescription = _sellerDescriptionDesired,
                    AdminContractAddress = SellerAdminService.ContractHandler.ContractAddress,
                    IsActive = true,
                    CreatedByAddress = string.Empty // filled by contract
                };
                var txReceiptCreate = await bpss.SetSellerRequestAndWaitForReceiptAsync(seller);
                var logSellerCreateEvent = txReceiptCreate.DecodeAllEvents<SellerCreatedLogEventDTO>().FirstOrDefault();
                if (txReceiptCreate.Status.Value != 1 || logSellerCreateEvent == null)
                {
                    throw new ContractDeploymentException($"Failed to set up {contractName}. Could not create global business partner data for seller.");
                }
                Log($"Tx status: {txReceiptCreate.Status.Value}");

                // Configure SellerAdmin
                LogHeader($"Configuring {contractName}...");
                var txReceipt = await SellerAdminService.ConfigureRequestAndWaitForReceiptAsync(
                    _businessPartnerStorageAddressGlobal).ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");
            }
            else
            {
                LogHeader($"Connecting to existing {contractName}...");
                SellerAdminService = new SellerAdminService(_web3, _existingSellerContractAddress);
            }

            // Check global business partner storage address and contract
            var bpStorageAddress = await SellerAdminService.BusinessPartnerStorageGlobalQueryAsync().ConfigureAwait(false);
            await ValidateBusinessPartnerStorageAddressAsync(bpStorageAddress).ConfigureAwait(false);

            // Check seller admin owner address
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
