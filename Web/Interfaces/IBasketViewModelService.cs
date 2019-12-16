using Nethereum.eShop.Web.Pages.Basket;
using System.Threading.Tasks;

namespace Nethereum.eShop.Web.Interfaces
{
    public interface IBasketViewModelService
    {
        Task<BasketViewModel> GetOrCreateBasketForUser(string userName);
    }
}
