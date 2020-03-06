using Nethereum.eShop.ApplicationCore.Entities.BasketAggregate;
using Nethereum.eShop.ApplicationCore.Interfaces;
using System.Threading.Tasks;

namespace Nethereum.eShop.Infrastructure.Data
{
    public class BasketRepository : EfRepository<Basket>, IBasketRepository
    {
        public BasketRepository(CatalogContext dbContext) : base(dbContext) { }

        public IUnitOfWork UnitOfWork => _dbContext;
        public Basket Add(Basket basket) => _dbContext.Baskets.Add(basket).Entity;
        public Basket Update(Basket basket) => _dbContext.Baskets.Update(basket).Entity;
        public Task<Basket> GetByIdWithItemsAsync(int id) => _dbContext.GetBasketWithItemsOrDefault(id);

        public Task<Basket> GetByBuyerIdWithItemsAsync(string buyerId) => _dbContext.GetBasketWithItemsOrDefault(buyerId);

        public void Delete(Basket basket)
        {
            _dbContext.Baskets.Remove(basket);
        }

        public void Delete(int basketId)
        {
            _dbContext.Entry(Basket.CreateForDeletion(basketId)).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }
    }
}
