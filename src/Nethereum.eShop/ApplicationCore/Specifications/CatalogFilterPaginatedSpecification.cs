using Nethereum.eShop.ApplicationCore.Entities;

namespace Nethereum.eShop.ApplicationCore.Specifications
{
    public class CatalogFilterPaginatedSpecification : BaseSpecification<CatalogItem>
    {
        public CatalogFilterPaginatedSpecification(int skip, int take, int? brandId, int? typeId, string searchText = null)
            : base
            (i => (!brandId.HasValue || i.CatalogBrandId == brandId) &&
            (!typeId.HasValue || i.CatalogTypeId == typeId) && 
            (searchText == null || (i.Name.Contains(searchText) || i.CatalogBrand.Brand.Contains(searchText))))
        {
            AddInclude(c => c.CatalogBrand);
            ApplyOrderBy(c => c.Rank);
            ApplyPaging(skip, take);
        }
    }
}
