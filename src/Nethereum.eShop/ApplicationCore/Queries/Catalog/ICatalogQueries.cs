using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Queries.Catalog
{

    public interface ICatalogQueries
    {
        Task<PaginatedResult<CatalogExcerpt>> GetCatalogItemsAsync(GetCatalogItemsSpecification catalogQuerySpecification);
    }
}
