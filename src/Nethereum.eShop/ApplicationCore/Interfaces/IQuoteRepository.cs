using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IQuoteRepository : IAsyncRepository<Quote>, IRepository
    {
        Quote Add(Quote quote);
        Quote Update(Quote quote);

        Task<Quote> GetByIdWithItemsAsync(int id);

        Task<List<Quote>> GetQuotesRequiringPurchaseOrderAsync();
    }
}
