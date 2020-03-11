using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.ApplicationCore.Entities;

namespace Nethereum.eShop.EntityFramework.Catalog.Repositories
{
    public class StockItemRepository : EfRepository<StockItem>, IStockItemRepository
    {
        public StockItemRepository(CatalogContext dbContext) : base(dbContext)
        {
        }
    }
}
