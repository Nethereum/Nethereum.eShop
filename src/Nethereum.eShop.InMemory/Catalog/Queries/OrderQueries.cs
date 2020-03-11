using Microsoft.EntityFrameworkCore;
using Nethereum.eShop.ApplicationCore.Entities.OrderAggregate;
using Nethereum.eShop.ApplicationCore.Queries;
using Nethereum.eShop.ApplicationCore.Queries.Orders;
using Nethereum.eShop.EntityFramework.Catalog;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.InMemory.Catalog.Queries
{
    public class OrderQueries : QueriesBase, IOrderQueries
    {
        public OrderQueries(CatalogContext dbContext) : base(dbContext)
        {
        }

        public async Task<PaginatedResult<OrderExcerpt>> GetByBuyerIdAsync(string buyerId, PaginationArgs paginationArgs)
        {
            var query = Where(buyerId, paginationArgs).AsNoTracking();

            var totalCount = await query.CountAsync();

            var items = await query.
                // Include(o => o.OrderItems).
                Include($"{nameof(Order.OrderItems)}.{nameof(OrderItem.ItemOrdered)}")
                .Skip(paginationArgs.Offset)
                .Take(paginationArgs.Fetch)
                .Select(o => new OrderExcerpt
                {
                    BillTo_RecipientName = o.BillTo.RecipientName,
                    BillTo_ZipCode = o.BillTo.ZipCode,
                    BuyerAddress = o.BuyerAddress,
                    BuyerId = o.BuyerId,
                    CurrencySymbol = o.CurrencySymbol,
                    ItemCount = o.ItemCount(),
                    OrderDate = o.OrderDate,
                    OrderId = o.Id,
                    PoNumber = o.PoNumber.ToString(),
                    PoType = o.PoType.ToString(),
                    QuoteId = o.QuoteId ?? 0,
                    ShipTo_RecipientName = o.ShipTo.RecipientName,
                    ShipTo_ZipCode = o.ShipTo.ZipCode,
                    Status = o.Status.ToString(),
                    Total = o.Total(),
                    TransactionHash = o.TransactionHash
                })
                .ToListAsync();

            return new PaginatedResult<OrderExcerpt>(totalCount, items, paginationArgs);
        }

        private IQueryable<Order> Where(string buyerId, PaginationArgs paginationArgs)
        {
            var query = _dbContext.Orders.Where(o => o.BuyerId == buyerId);

            //TODO: implement other sort columns
            switch (paginationArgs.SortBy)
            {
                default:
                    return paginationArgs.SortDescending ? query.OrderByDescending(c => c.Id) : query.OrderBy(c => c.Id);
            }
        }
    }
}
