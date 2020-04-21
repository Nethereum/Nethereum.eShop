using Nethereum.Commerce.Contracts.BusinessPartnerStorage;
using System.Threading.Tasks;

namespace Nethereum.Commerce.Contracts.Deployment
{
    public interface IBusinessPartnersDeployment
    {
        BusinessPartnerStorageService BusinessPartnerStorageService { get; }
        string Owner { get; }

        Task InitializeAsync();
    }
}