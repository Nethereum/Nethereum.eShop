using Ardalis.GuardClauses;
using Nethereum.eShop.ApplicationCore.Interfaces;

namespace Nethereum.eShop.ApplicationCore.Entities.BuyerAggregate
{
    public class Buyer : BaseEntity, IAggregateRoot
    {
        // Expected To Be Ethereum Address
        public string BuyerId { get; private set; }
        public PostalAddress ShipTo { get; private set; }
        public PostalAddress BillTo { get; private set; }

        private Buyer()
        {
            // required by EF
        }

        public Buyer(string identity, PostalAddress shipToAddress, PostalAddress billToAddress) : this()
        {
            Guard.Against.NullOrEmpty(identity, nameof(identity));
            Guard.Against.Null(shipToAddress, nameof(shipToAddress));
            Guard.Against.Null(billToAddress, nameof(billToAddress));

            BuyerId = identity;
            ShipTo = shipToAddress;
            BillTo = billToAddress;
        }
    }
}
