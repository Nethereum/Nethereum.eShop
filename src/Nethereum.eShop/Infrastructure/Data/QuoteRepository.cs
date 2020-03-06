using Microsoft.EntityFrameworkCore;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using Nethereum.eShop.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.Infrastructure.Data
{
    public class QuoteRepository : EfRepository<Quote>, IQuoteRepository
    {
        public QuoteRepository(CatalogContext dbContext) : base(dbContext){}

        public IUnitOfWork UnitOfWork => _dbContext;
        public Quote Add(Quote quote) => _dbContext.Quotes.Add(quote).Entity;
        public Quote Update(Quote quote) => _dbContext.Quotes.Update(quote).Entity;
        public Task<Quote> GetByIdWithItemsAsync(int id) => _dbContext.GetQuoteWithItemsOrDefault(id);

        public Task<List<Quote>> GetQuotesRequiringPurchaseOrderAsync() =>
            _dbContext.Quotes.Where(quote => quote.Status == QuoteStatus.Pending && quote.PoNumber == null)
            .Include(q => q.QuoteItems)
            .ToListAsync();
    }
}
