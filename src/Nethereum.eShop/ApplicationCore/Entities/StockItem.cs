using Nethereum.eShop.ApplicationCore.Interfaces;

namespace Nethereum.eShop.ApplicationCore.Entities
{
    public class StockItem: BaseEntity, IAggregateRoot
    {
        public CatalogItem CatalogItem { get; set; }
        public int CatalogItemId { get; set; }
        public string Location { get; set; }
        public int Quantity { get; set; }
    }
}
