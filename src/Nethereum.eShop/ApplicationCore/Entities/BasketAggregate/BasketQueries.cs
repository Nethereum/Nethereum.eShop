using Microsoft.EntityFrameworkCore;
using Nethereum.eShop.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Entities.BasketAggregate
{
    public static class BasketQueries
    {
        public static Task<Basket> GetBasketWithItemsOrDefault(this CatalogContext context, int basketId)
        {
            return context.Baskets.Include(b => b.Items).Where(b => b.Id == basketId).FirstOrDefaultAsync();
        }

        public static Task<Basket> GetBasketWithItemsOrDefault(this CatalogContext context, string buyerId)
        {
            return context.Baskets.Include(b => b.Items).Where(b => b.BuyerId == buyerId).FirstOrDefaultAsync();
        }
    }
}
