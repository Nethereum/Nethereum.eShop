using Microsoft.EntityFrameworkCore;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using Nethereum.eShop.ApplicationCore.Queries;
using Nethereum.eShop.ApplicationCore.Queries.Quotes;
using Nethereum.eShop.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.InMemory.ApplicationCore.Queries.Quotes
{
    public class QuoteQueries : QueriesBase, IQuoteQueries
    {
        public QuoteQueries(CatalogContext dbContext) : base(dbContext)
        {
        }

        public async Task<PaginatedResult<QuoteExcerpt>> GetByBuyerIdAsync(string buyerId, PaginationArgs paginationArgs)
        {
            var query = Where(buyerId, paginationArgs).AsNoTracking();

            var totalCount = await query.CountAsync();

            var items = await query.
                 Include(o => o.QuoteItems)
                .Skip(paginationArgs.Offset)
                .Take(paginationArgs.Fetch)
                .Select(o => new QuoteExcerpt
                {
                    BillTo_RecipientName = o.BillTo.RecipientName,
                    BillTo_ZipCode = o.BillTo.ZipCode,
                    BuyerAddress = o.BuyerAddress,
                    BuyerId = o.BuyerId,
                    CurrencySymbol = o.CurrencySymbol,
                    ItemCount = o.ItemCount(),
                    QuoteDate = o.Date,
                    QuoteId = o.Id,
                    PoNumber = o.PoNumber.ToString(),
                    PoType = o.PoType.ToString(),
                    Expiry = o.Expiry,
                    ShipTo_RecipientName = o.ShipTo.RecipientName,
                    ShipTo_ZipCode = o.ShipTo.ZipCode,
                    Status = o.Status.ToString(),
                    Total = o.Total(),
                    TransactionHash = o.TransactionHash
                })
                .ToListAsync();

            return new PaginatedResult<QuoteExcerpt>(totalCount, items, paginationArgs);
        }

        private IQueryable<Quote> Where(string buyerId, PaginationArgs paginationArgs)
        {
            var query = _dbContext.Quotes.Where(o => o.BuyerId == buyerId);

            //TODO: implement other sort columns
            switch (paginationArgs.SortBy)
            {
                default:
                    return paginationArgs.SortDescending ? query.OrderByDescending(c => c.Id) : query.OrderBy(c => c.Id);
            }
        }
    }
}
