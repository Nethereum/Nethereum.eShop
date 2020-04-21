using Nethereum.Commerce.Contracts.BusinessPartnerStorage;
using Nethereum.Commerce.Contracts.Funding;
using Nethereum.Commerce.Contracts.Purchasing;
using System.Threading.Tasks;

namespace Nethereum.Commerce.Contracts.Deployment
{
    public interface IEshopDeployment
    {
        BusinessPartnerStorageService BusinessPartnerStorageGlobalService { get; }
        string EshopId { get; }
        FundingService FundingService { get; }
        string Owner { get; }
        PurchasingService PurchasingService { get; }

        Task InitializeAsync();
    }
}