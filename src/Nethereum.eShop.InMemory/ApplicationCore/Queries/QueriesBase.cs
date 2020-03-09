using Nethereum.eShop.Infrastructure.Data;

namespace Nethereum.eShop.InMemory.ApplicationCore.Queries
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
