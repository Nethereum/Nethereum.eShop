using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.Infrastructure.Identity;
using Nethereum.eShop.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.Web.Pages.Basket
{
    public class CheckoutModel : PageModel
    {
        private readonly IBasketService _basketService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IQuoteService _quoteService;
        private string _username = null;
        private readonly IBasketViewModelService _basketViewModelService;

        public CheckoutModel(IBasketService basketService,
            IBasketViewModelService basketViewModelService,
            SignInManager<ApplicationUser> signInManager,
            IQuoteService quoteService)
        {
            _basketService = basketService;
            _signInManager = signInManager;
            _quoteService = quoteService;
            _basketViewModelService = basketViewModelService;
        }

        public BasketViewModel BasketModel { get; set; } = new BasketViewModel();

        public async Task<IActionResult> OnGet()
        {
            if(await IsFromASignInRedirect())
            {
                await _basketService.TransferBasketAsync(Request.Cookies[Constants.BASKET_COOKIENAME], User.Identity.Name);
                SetBasketCookieName(_username);
                await SetBasketModelAsync();
                await _quoteService.CreateQuoteAsync(BasketModel.Id);
                await _basketService.DeleteBasketAsync(BasketModel.Id);
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(Dictionary<string, int> items)
        {
            await SetBasketModelAsync();

            await _basketService.SetQuantities(BasketModel.Id, items);
            await _quoteService.CreateQuoteAsync(BasketModel.Id);
            await _basketService.DeleteBasketAsync(BasketModel.Id);

            return RedirectToPage();
        }

        private async Task<bool> IsFromASignInRedirect()
        {
            if (!Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME)) return false;

            var userNameFromCookie = Request.Cookies[Constants.BASKET_COOKIENAME];

            if (_signInManager.IsSignedIn(HttpContext.User) && (User.Identity.Name != userNameFromCookie))
            {
                var basketFromCookieName = await _basketViewModelService.GetOrCreateBasketForUser(userNameFromCookie);
                if (basketFromCookieName.Items?.Count == 0) return false;

                var signedInBasket = await _basketViewModelService.GetOrCreateBasketForUser(User.Identity.Name);
                return signedInBasket.Items?.Count == 0;
            }

            return false;
        }

        private async Task SetBasketModelAsync()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                BasketModel = await _basketViewModelService.GetOrCreateBasketForUser(User.Identity.Name);
            }
            else
            {
                GetOrSetBasketCookieAndUserName();
                BasketModel = await _basketViewModelService.GetOrCreateBasketForUser(_username);
            }
        }

        private void GetOrSetBasketCookieAndUserName()
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                _username = Request.Cookies[Constants.BASKET_COOKIENAME];
            }
            if (_username != null) return;

            _username = Guid.NewGuid().ToString();
            SetBasketCookieName(_username);
        }

        private void SetBasketCookieName(string userName)
        {
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Today.AddYears(10);
            Response.Cookies.Append(Constants.BASKET_COOKIENAME, userName, cookieOptions);
        }
    }
}
