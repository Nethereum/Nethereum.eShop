using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Queries.Orders
{
    public interface IOrderQueries
    {
        Task<Paginated<OrderExcerpt>> GetByBuyerIdAsync(string buyerId, string sortBy = null, int offset = 0, int fetch = 50);
    }
}
