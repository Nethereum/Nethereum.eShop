using Nethereum.eShop.ApplicationCore.Entities.OrderAggregate;
using Nethereum.eShop.Infrastructure.Data;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{

    public interface IOrderRepository : IAsyncRepository<Order>, IRepository
    {
        Order Add(Order order);
        Order Update(Order order);
        Task<Order> GetByIdWithItemsAsync(int id);
    }
}
