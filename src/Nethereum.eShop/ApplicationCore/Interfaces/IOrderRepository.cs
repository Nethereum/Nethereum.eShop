using Nethereum.eShop.ApplicationCore.Entities.OrderAggregate;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{

    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<Order> GetByIdWithItemsAsync(int id);
    }
}
