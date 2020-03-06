using Nethereum.eShop.ApplicationCore.Entities.OrderAggregate;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Nethereum.eShop.Infrastructure.Data
{

    public class OrderRepository : EfRepository<Order>, IOrderRepository
    {

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _dbContext;
            }
        }

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

        public Order Add(Order order)
        {
            return _dbContext.Orders.Add(order).Entity;
        }

        public Order Update(Order order)
        {
            return _dbContext.Orders.Update(order).Entity;
        }

        /*
        public Buyer Add(Buyer buyer)
        {
            if (buyer.IsTransient())
            {
                return _context.Buyers
                    .Add(buyer)
                    .Entity;
            }
            else
            {
                return buyer;
            }           
        }

        public Buyer Update(Buyer buyer)
        {
            return _context.Buyers
                    .Update(buyer)
                    .Entity;
        }

        public async Task<Buyer> FindAsync(string identity)
        {
            var buyer = await _context.Buyers
                .Include(b => b.PaymentMethods)
                .Where(b => b.IdentityGuid == identity)
                .SingleOrDefaultAsync();

            return buyer;
        }

        public async Task<Buyer> FindByIdAsync(string id)
        {
            var buyer = await _context.Buyers
                .Include(b => b.PaymentMethods)
                .Where(b => b.Id == int.Parse(id))
                .SingleOrDefaultAsync();

            return buyer;
        }
         */
    }
}
