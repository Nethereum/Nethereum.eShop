using Nethereum.eShop.ApplicationCore.Interfaces;

namespace Nethereum.eShop.ApplicationCore.Entities
{
    public class CatalogBrand : BaseEntity, IAggregateRoot
    {
        public string Brand { get; set; }
    }
}
