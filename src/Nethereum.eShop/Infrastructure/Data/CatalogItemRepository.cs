using Nethereum.eShop.ApplicationCore.Entities;
using Nethereum.eShop.ApplicationCore.Interfaces;

namespace Nethereum.eShop.Infrastructure.Data
{
    public class CatalogItemRepository : EfRepository<CatalogItem>, ICatalogItemRepository
    {
        public CatalogItemRepository(CatalogContext dbContext) : base(dbContext){}

        public IUnitOfWork UnitOfWork => _dbContext;
    }
}
