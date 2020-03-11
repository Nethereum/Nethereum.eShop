using Microsoft.EntityFrameworkCore;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using Nethereum.eShop.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.EntityFramework.Catalog.Repositories
{
    public class QuoteRepository : EfRepository<Quote>, IQuoteRepository
    {
        public QuoteRepository(CatalogContext dbContext) : base(dbContext){}

        public Task<Quote> GetByIdWithItemsAsync(int id) =>
            _dbContext.Quotes
            .Include(b => b.QuoteItems)
            .Where(b => b.Id == id)
            .FirstOrDefaultAsync();

        public Task<List<Quote>> GetQuotesRequiringPurchaseOrderAsync() =>
            _dbContext.Quotes.Where(quote => quote.Status == QuoteStatus.Pending && quote.PoNumber == null)
            .Include(q => q.QuoteItems)
            .ToListAsync();
    }
}
