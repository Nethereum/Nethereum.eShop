using Nethereum.Commerce.Contracts.BusinessPartnerStorage;
using Nethereum.Commerce.Contracts.BuyerWallet;
using System.Threading.Tasks;

namespace Nethereum.Commerce.Contracts.Deployment
{
    /// <summary>
    /// Deploys a new BuyerWallet.sol contract or connects to an existing one.
    /// Throws ContractDeploymentException if contract is not setup correctly.
    /// Usage: call BuyerDeployment.CreateFromNewDeployment() or .CreateFromConnectExistingContract() to
    /// create new deployment object, then call InitializeAsync() to set it up.
    /// </summary>
    public interface IBuyerDeployment
    {
        BuyerWalletService BuyerWalletService { get; }
        BusinessPartnerStorageService BusinessPartnerStorageGlobalService { get; }
        string Owner { get; }

        Task InitializeAsync();
    }
}