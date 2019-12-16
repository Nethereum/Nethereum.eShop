using Nethereum.eShop.Web.ViewModels;
using System.Threading.Tasks;

namespace Nethereum.eShop.Web.Interfaces
{
    public interface ICatalogItemViewModelService
    {
        Task UpdateCatalogItem(CatalogItemViewModel viewModel);
    }
}
