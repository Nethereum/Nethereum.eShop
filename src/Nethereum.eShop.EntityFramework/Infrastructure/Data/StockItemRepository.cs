using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.ApplicationCore.Entities;

namespace Nethereum.eShop.Infrastructure.Data
{
    public class StockItemRepository : EfRepository<StockItem>, IStockItemRepository
    {
        public StockItemRepository(CatalogContext dbContext) : base(dbContext)
        {
        }
    }
}
