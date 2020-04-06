using Microsoft.Extensions.Logging;
using Nethereum.Commerce.Contracts.SellerAdmin;
using Nethereum.Commerce.Contracts.SellerAdmin.ContractDefinition;
using Nethereum.Web3;
using System.Threading.Tasks;

namespace Nethereum.Commerce.Contracts.Deployment
{
    public class SellerDeployment : ContractDeploymentBase
    {
        public SellerAdminService SellerAdminService { get; internal set; }
        public string SellerId { get; internal set; }

        private readonly string _addressOfAddressRegistryGlobal;
        private readonly string _nameOfBusinessPartnerStorageGlobal;
        private readonly string _addressOfExistingSellerContract;
        private readonly string _sellerIdDesired;

        public SellerDeployment(
            IWeb3 web3,
            string addressOfAddressRegistryGlobal,
            string nameOfBusinessPartnerStorageGlobal,
            string sellerId,
            ILogger logger = null)
            : base(web3, logger)
        {
            _isNewDeployment = true;
            _addressOfAddressRegistryGlobal = addressOfAddressRegistryGlobal;
            _nameOfBusinessPartnerStorageGlobal = nameOfBusinessPartnerStorageGlobal;
            _sellerIdDesired = sellerId;
        }

        public SellerDeployment(
            IWeb3 web3,
            string addressOfExistingSellerContract,
            ILogger logger = null)
            : base(web3, logger)
        {
            _isNewDeployment = false;
            _addressOfExistingSellerContract = addressOfExistingSellerContract;
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
                    ContractAddressOfRegistryGlobal = _addressOfAddressRegistryGlobal,
                    SellerIdString = _sellerIdDesired
                };
                SellerAdminService = await SellerAdminService.DeployContractAndGetServiceAsync(
                    _web3, sellerAdminDeployment).ConfigureAwait(false);

                // Configure
                Log();
                Log($"Configuring {contractName}...");
                var txReceipt = await SellerAdminService.ConfigureRequestAndWaitForReceiptAsync(
                    _nameOfBusinessPartnerStorageGlobal).ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");
            }
            else
            {
                LogHeader($"Connecting to existing {contractName}...");
                SellerAdminService = new SellerAdminService(_web3, _addressOfExistingSellerContract);
            }

            // Check owner
            var sellerAdminOwner = await SellerAdminService.OwnerQueryAsync().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(sellerAdminOwner))
            {
                throw new ContractDeploymentException($"Failed to set up {contractName}, missing owner");
            }
            Log($"{contractName} address is: {SellerAdminService.ContractHandler.ContractAddress}");
            Log($"{contractName} owner is  : {sellerAdminOwner}");
            Owner = sellerAdminOwner;
            // Check sellerId
            SellerId = (await SellerAdminService.SellerIdQueryAsync().ConfigureAwait(false)).ConvertToString();
            if (string.IsNullOrWhiteSpace(SellerId))
            {
                throw new ContractDeploymentException($"Failed to set up {contractName}, missing sellerId");
            }
            Log("Done");
        }
    }
}
