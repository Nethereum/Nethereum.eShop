using Nethereum.eShop.ApplicationCore.Entities;

namespace Nethereum.eShop.ApplicationCore.Specifications
{
    public class CatalogFilterPaginatedSpecification : BaseSpecification<CatalogItem>
    {
        public CatalogFilterPaginatedSpecification(int skip, int take, int? brandId, int? typeId)
            : base(i => (!brandId.HasValue || i.CatalogBrandId == brandId) &&
                (!typeId.HasValue || i.CatalogTypeId == typeId))
        {
            ApplyPaging(skip, take);
        }
    }
}
