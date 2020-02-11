using Microsoft.EntityFrameworkCore;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using Nethereum.eShop.ApplicationCore.Interfaces;
using System.Threading.Tasks;

namespace Nethereum.eShop.Infrastructure.Data
{
    public class QuoteRepository : EfRepository<Quote>, IQuoteRepository
    {
        public QuoteRepository(CatalogContext dbContext) : base(dbContext)
        {
        }

        public Task<Quote> GetByIdWithItemsAsync(int id)
        {
            return _dbContext.Quotes
                .Include(o => o.QuoteItems)
                .Include($"{nameof(Quote.QuoteItems)}.{nameof(QuoteItem.ItemOrdered)}")
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
