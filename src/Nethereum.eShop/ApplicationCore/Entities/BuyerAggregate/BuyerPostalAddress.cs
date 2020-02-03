namespace Nethereum.eShop.ApplicationCore.Entities.BuyerAggregate
{
    public class BuyerPostalAddress: BaseEntity
    {
        public string Name { get; set; }
        public PostalAddress PostalAddress { get; private set; }
    }
}
