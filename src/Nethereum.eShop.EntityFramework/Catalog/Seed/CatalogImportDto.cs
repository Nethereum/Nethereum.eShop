using Nethereum.eShop.ApplicationCore.Entities;
using System.Collections.Generic;

namespace Nethereum.eShop.EntityFramework.Catalog.Seed
{
    public class CatalogImportDto
    {
        public List<CatalogBrandForImport> CatalogBrands { get; set; } = new List<CatalogBrandForImport>();
        public List<CatalogTypeForImport> CatalogTypes { get; set; } = new List<CatalogTypeForImport>();
        public List<CatalogItemForImport> CatalogItems { get; set; } = new List<CatalogItemForImport>();
    }

    public class CatalogBrandForImport : CatalogBrand
    {
        public new int Id
        {
            get => base.Id;
            set => base.Id = value;
        }
    }

    public class CatalogTypeForImport : CatalogType
    {
        public new int Id
        {
            get => base.Id;
            set => base.Id = value;
        }
    }

    public class CatalogItemForImport : CatalogItem
    {
        public new int Id
        {
            get => base.Id;
            set => base.Id = value;
        }
    }
}
