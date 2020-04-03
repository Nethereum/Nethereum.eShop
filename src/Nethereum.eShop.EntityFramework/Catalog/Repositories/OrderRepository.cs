using Nethereum.eShop.ApplicationCore.Entities.OrderAggregate;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Nethereum.eShop.EntityFramework.Catalog.Repositories
{

    public class OrderRepository : EfRepository<Order>, IOrderRepository
    {
        public OrderRepository(CatalogContext dbContext) : base(dbContext)
        {
        }

        public Task<Order> GetByIdWithItemsAsync(int id)
        {
            return _dbContext.Orders
                .Include(o => o.OrderItems)
                .Include($"{nameof(Order.OrderItems)}.{nameof(OrderItem.ItemOrdered)}")
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
