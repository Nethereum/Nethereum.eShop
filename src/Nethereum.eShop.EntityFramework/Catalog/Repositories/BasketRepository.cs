using Microsoft.EntityFrameworkCore;
using Nethereum.eShop.ApplicationCore.Entities.BasketAggregate;
using Nethereum.eShop.ApplicationCore.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.EntityFramework.Catalog.Repositories
{
    public class BasketRepository : EfRepository<Basket>, IBasketRepository
    {
        public BasketRepository(CatalogContext dbContext) : base(dbContext) { }

        public Task<Basket> GetByIdWithItemsAsync(int id) => 
            _dbContext.Baskets
            .Include(b => b.Items)
            .Where(b => b.Id == id)
            .FirstOrDefaultAsync();

        public Task<Basket> GetByBuyerIdWithItemsAsync(string buyerId) =>
            _dbContext.Baskets
            .Include(b => b.Items)
            .Where(b => b.BuyerId == buyerId)
            .FirstOrDefaultAsync();

        public void Delete(int basketId)
        {
            _dbContext.Entry(Basket.CreateForDeletion(basketId)).State = EntityState.Deleted;
        }
    }
}
