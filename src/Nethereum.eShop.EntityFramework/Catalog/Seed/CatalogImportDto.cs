using Nethereum.eShop.ApplicationCore.Entities;
using System.Collections.Generic;

namespace Nethereum.eShop.EntityFramework.Catalog.Seed
{
    public class CatalogImportDto
    {
        public List<CatalogBrand> CatalogBrands { get; set; } = new List<CatalogBrand>();
        public List<CatalogType> CatalogTypes { get; set; } = new List<CatalogType>();
        public List<CatalogItem> CatalogItems { get; set; } = new List<CatalogItem>();
    }
}
