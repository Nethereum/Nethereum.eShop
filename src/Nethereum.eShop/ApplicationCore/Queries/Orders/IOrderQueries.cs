using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Queries.Orders
{
    public interface IOrderQueries
    {
        Task<PaginatedResult<OrderExcerpt>> GetByBuyerIdAsync(string buyerId, PaginationArgs paginationArgs);
    }
}
