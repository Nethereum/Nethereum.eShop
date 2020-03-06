using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Queries.Quotes
{
    public interface IQuoteQueries
    {
        Task<Paginated<QuoteExcerpt>> GetByBuyerIdAsync(string buyerId, string sortBy = null, int offset = 0, int fetch = 50);
    }
}
