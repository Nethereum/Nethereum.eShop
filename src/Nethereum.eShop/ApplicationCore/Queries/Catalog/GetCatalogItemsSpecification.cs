namespace Nethereum.eShop.ApplicationCore.Queries.Catalog
{
    public class GetCatalogItemsSpecification: PaginatedQuerySpecification
    {
        public int? BrandId { get; set; }

        public int? TypeId { get; set; }

        public string SearchText { get; set; }
    }
}
