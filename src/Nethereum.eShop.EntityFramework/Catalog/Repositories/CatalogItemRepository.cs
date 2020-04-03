using Nethereum.eShop.ApplicationCore.Entities;
using Nethereum.eShop.ApplicationCore.Interfaces;

namespace Nethereum.eShop.EntityFramework.Catalog.Repositories
{
    public class CatalogItemRepository : EfRepository<CatalogItem>, ICatalogItemRepository
    {
        public CatalogItemRepository(CatalogContext dbContext) : base(dbContext){}
    }
}
