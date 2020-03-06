using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Queries.Quotes
{
    public interface IQuoteQueries
    {
        Task<PaginatedResult<QuoteExcerpt>> GetByBuyerIdAsync(string buyerId, PaginationArgs paginationArgs);
    }
}
