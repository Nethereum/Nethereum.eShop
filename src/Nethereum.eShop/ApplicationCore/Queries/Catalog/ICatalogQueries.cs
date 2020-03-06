using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Queries.Catalog
{

    public interface ICatalogQueries
    {
        Task<Paginated<CatalogExcerpt>> GetCatalogItemsAsync(GetCatalogItemsSpecification catalogQuerySpecification);
    }
}
