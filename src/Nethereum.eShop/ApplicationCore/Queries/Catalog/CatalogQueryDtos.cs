using System;
using System.Collections.Generic;
using System.Text;

namespace Nethereum.eShop.ApplicationCore.Queries.Catalog
{
    public class CatalogExcerpt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string PictureUri { get; set; }
        public decimal Price { get; set; }
    }
}
