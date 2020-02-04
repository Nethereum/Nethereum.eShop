using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nethereum.eShop.ApplicationCore.Constants;
using Nethereum.eShop.Web.Interfaces;
using Nethereum.eShop.Web.ViewModels;
using System.Threading.Tasks;

namespace Nethereum.eShop.Web.Pages.Admin
{
    [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
    public class EditCatalogItemModel : PageModel
    {
        private readonly ICatalogItemViewModelService _catalogItemViewModelService;

        public EditCatalogItemModel(ICatalogItemViewModelService catalogItemViewModelService)
        {
            _catalogItemViewModelService = catalogItemViewModelService;
        }

        [BindProperty]
        public CatalogItemViewModel CatalogModel { get; set; } = new CatalogItemViewModel();

        public Task OnGet(CatalogItemViewModel catalogModel)
        {
            CatalogModel = catalogModel;
            return Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _catalogItemViewModelService.UpdateCatalogItem(CatalogModel);
            }

            return RedirectToPage("/Admin/Index");
        }
    }
}
