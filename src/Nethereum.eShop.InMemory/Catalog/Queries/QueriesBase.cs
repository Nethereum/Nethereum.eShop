using Nethereum.eShop.EntityFramework.Catalog;

namespace Nethereum.eShop.InMemory.Catalog.Queries
{
    public abstract class QueriesBase
    {
        protected readonly CatalogContext _dbContext;

        public QueriesBase(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
