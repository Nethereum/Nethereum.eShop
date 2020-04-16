using Microsoft.Extensions.Logging;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition;
using Nethereum.Commerce.Contracts.MockDai;
using Nethereum.Commerce.Contracts.SellerAdmin;
using Nethereum.Commerce.Contracts.SellerAdmin.ContractDefinition;
using Nethereum.Contracts;
using Nethereum.Web3;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.Commerce.Contracts.Deployment
{
    /// <summary>
    /// Deploys a new complete sample Eshop along with a new global business partners
    /// storage, two buyers, two sellers and a mock DAI token.
    /// </summary>
    public class CompleteSampleDeployment : ContractDeploymentBase
    {
        public IEshopDeployment EshopDeployment { get; internal set; }
        public IBuyerDeployment BuyerDeployment { get; internal set; }
        public IBuyerDeployment BuyerDeployment02 { get; internal set; }
        public ISellerDeployment SellerDeployment { get; internal set; }
        public ISellerDeployment SellerDeployment02 { get; internal set; }
        public IBusinessPartnersDeployment BusinessPartnersDeployment { get; internal set; }
        public MockDaiService MockDaiService { get; internal set; }

        private readonly string _businessPartnerStorageAddressGlobal;
        private readonly string _existingSellerContractAddress;
        private readonly string _sellerIdDesired;
        private readonly string _sellerDescriptionDesired;

        /// <summary>
        /// Deploy and configure a new SellerAdmin.sol contract
        /// </summary>
        public static CompleteSampleDeployment CreateFromNewDeployment(
            IWeb3 web3,
            CompleteSampleDeploymentConfig completeSampleDeployment, 
            ILogger logger = null)
        {
            return new CompleteSampleDeployment(web3, completeSampleDeployment, true, logger);
        }

        private CompleteSampleDeployment(
            IWeb3 web3,
            CompleteSampleDeploymentConfig businessPartnerStorageAddressGlobal,
            bool isNewDeployment,
            ILogger logger = null)
            : base(web3, logger)
        {
            //if (!address.IsValidNonZeroAddress())
            //{
            //    throw new ContractDeploymentException($"Failed to set up {GetType().Name}. Address {address} is zero or not valid hex format.");
            //}
            //_sellerIdDesired = sellerId;
            //_sellerDescriptionDesired = sellerDescription ?? sellerId;
            //_isNewDeployment = isNewDeployment;
            //if (_isNewDeployment)
            //{
            //    // address represents the business partner storage address
            //    _businessPartnerStorageAddressGlobal = address;
            //}
            //else
            //{
            //    // address represents the existing seller admin address
            //    _existingSellerContractAddress = address;
            //}
        }

        public async Task InitializeAsync()
        {
            //var contractName = GetType().Name;

            //// Validate Global Business Partner Storage and get the service for it
            //string bpStorageAddress;
            //if (_isNewDeployment)
            //{
            //    bpStorageAddress = _businessPartnerStorageAddressGlobal;
            //}
            //else
            //{
            //    LogHeader($"Connecting to existing {contractName}...");
            //    SellerAdminService = new SellerAdminService(_web3, _existingSellerContractAddress);
            //    bpStorageAddress = await SellerAdminService.BusinessPartnerStorageGlobalQueryAsync().ConfigureAwait(false);
            //}
            //BusinessPartnerStorageGlobalService = await GetValidBusinessPartnerStorageServiceAsync(bpStorageAddress).ConfigureAwait(false);

            //// Do new deployment
            //if (_isNewDeployment)
            //{
            //    // Deploy
            //    LogHeader($"Deploying {contractName}...");
            //    var sellerAdminDeployment = new SellerAdminDeployment()
            //    {
            //        SellerIdString = _sellerIdDesired
            //    };
            //    SellerAdminService = await SellerAdminService.DeployContractAndGetServiceAsync(
            //        _web3, sellerAdminDeployment).ConfigureAwait(false);

            //    // Create master data for sellerIdDesired
            //    LogHeader($"Creating master data record in global business partner storage...");
            //    BusinessPartnerStorageGlobalService = new BusinessPartnerStorageService(_web3, _businessPartnerStorageAddressGlobal);
            //    var seller = new Seller()
            //    {
            //        SellerId = _sellerIdDesired,
            //        SellerDescription = _sellerDescriptionDesired,
            //        AdminContractAddress = SellerAdminService.ContractHandler.ContractAddress,
            //        IsActive = true,
            //        CreatedByAddress = string.Empty // filled by contract
            //    };
            //    var txReceiptCreate = await BusinessPartnerStorageGlobalService.SetSellerRequestAndWaitForReceiptAsync(seller);
            //    var logSellerCreateEvent = txReceiptCreate.DecodeAllEvents<SellerCreatedLogEventDTO>().FirstOrDefault();
            //    if (txReceiptCreate.Status.Value != 1 || logSellerCreateEvent == null)
            //    {
            //        throw new ContractDeploymentException($"Failed to set up {contractName}. Could not create global business partner data for seller.");
            //    }
            //    Log($"Tx status: {txReceiptCreate.Status.Value}");

            //    // Configure SellerAdmin
            //    LogHeader($"Configuring {contractName}...");
            //    var txReceipt = await SellerAdminService.ConfigureRequestAndWaitForReceiptAsync(
            //        _businessPartnerStorageAddressGlobal).ConfigureAwait(false);
            //    Log($"Tx status: {txReceipt.Status.Value}");
            //}
            
            //// Check seller admin owner address
            //var sellerAdminOwner = await SellerAdminService.OwnerQueryAsync().ConfigureAwait(false);
            //if (!sellerAdminOwner.IsValidNonZeroAddress())
            //{
            //    throw new ContractDeploymentException($"Failed to set up {contractName}. Owner address {sellerAdminOwner} is zero or not valid hex format.");
            //}
            //Log($"{contractName} address is: {SellerAdminService.ContractHandler.ContractAddress}");
            //Log($"{contractName} owner is  : {sellerAdminOwner}");
            //Owner = sellerAdminOwner;

            //// Check sellerId string
            //SellerId = (await SellerAdminService.SellerIdQueryAsync().ConfigureAwait(false)).ConvertToString();
            //if (string.IsNullOrWhiteSpace(SellerId))
            //{
            //    throw new ContractDeploymentException($"Failed to set up {contractName}. SellerId must have a value.");
            //}
            //Log("Done");
        }
    }
}
