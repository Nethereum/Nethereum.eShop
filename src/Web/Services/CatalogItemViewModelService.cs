using Nethereum.eShop.ApplicationCore.Entities;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.Web.Interfaces;
using Nethereum.eShop.Web.ViewModels;
using System.Threading.Tasks;

namespace Nethereum.eShop.Web.Services
{
    public class CatalogItemViewModelService : ICatalogItemViewModelService
    {
        private readonly ICatalogItemRepository _catalogItemRepository;

        public CatalogItemViewModelService(ICatalogItemRepository catalogItemRepository)
        {
            _catalogItemRepository = catalogItemRepository;
        }

        public async Task UpdateCatalogItem(CatalogItemViewModel viewModel)
        {
            //Get existing CatalogItem
            var existingCatalogItem = await _catalogItemRepository.GetByIdAsync(viewModel.Id);

            //Build updated CatalogItem
            var updatedCatalogItem = existingCatalogItem;
            updatedCatalogItem.Name = viewModel.Name;
            updatedCatalogItem.Price = viewModel.Price;

            await _catalogItemRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
