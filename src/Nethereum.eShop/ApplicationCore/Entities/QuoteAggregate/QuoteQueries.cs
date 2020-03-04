using Microsoft.EntityFrameworkCore;
using Nethereum.eShop.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate
{
    public static class QuoteQueries
    {
        public static Task<Quote> GetQuoteWithItemsOrDefault(this CatalogContext context, int quoteId)
        {
            return context.Quotes.Include(b => b.QuoteItems).Where(b => b.Id == quoteId).FirstOrDefaultAsync();
        }
    }
}
