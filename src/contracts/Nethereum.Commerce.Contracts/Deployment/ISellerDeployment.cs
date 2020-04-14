using Nethereum.Commerce.Contracts.BusinessPartnerStorage;
using Nethereum.Commerce.Contracts.SellerAdmin;
using System.Threading.Tasks;

namespace Nethereum.Commerce.Contracts.Deployment
{
    /// <summary>
    /// Deploys a new SellerAdmin.sol contract and creates its master data, or connects to an existing one.
    /// Throws ContractDeploymentException or SmartContractRevertException if contract is not setup correctly.
    /// Usage: call SellerDeployment.CreateFromNewDeployment() or .CreateFromConnectExistingContract() to
    /// create new deployment object, then call InitializeAsync() to set it up.
    /// </summary>
    public interface ISellerDeployment
    {
        SellerAdminService SellerAdminService { get; }
        BusinessPartnerStorageService BusinessPartnerStorageGlobalService { get; }
        string SellerId { get; }
        string Owner { get; }

        Task InitializeAsync();
    }
}