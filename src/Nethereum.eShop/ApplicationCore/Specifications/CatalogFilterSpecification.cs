using Nethereum.eShop.ApplicationCore.Entities;

namespace Nethereum.eShop.ApplicationCore.Specifications
{

    public class CatalogFilterSpecification : BaseSpecification<CatalogItem>
    {
        public CatalogFilterSpecification(int? brandId, int? typeId, string searchText = null)
            : base(i => (!brandId.HasValue || i.CatalogBrandId == brandId) &&
                (!typeId.HasValue || i.CatalogTypeId == typeId) &&
                (searchText == null || (i.Name.Contains(searchText) || i.CatalogBrand.Brand.Contains(searchText))))
        {
        }
    }
}
