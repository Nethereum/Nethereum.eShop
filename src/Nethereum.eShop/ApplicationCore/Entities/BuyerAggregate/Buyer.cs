using Ardalis.GuardClauses;
using Nethereum.eShop.ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace Nethereum.eShop.ApplicationCore.Entities.BuyerAggregate
{
    public class Buyer : BaseEntity, IAggregateRoot
    {
        private readonly List<BuyerPostalAddress> _postalAddresses = new List<BuyerPostalAddress>();

        public IReadOnlyCollection<BuyerPostalAddress> PostalAddresses => _postalAddresses.AsReadOnly();

        public string BuyerId { get; private set; }

        // Expected To Be Ethereum Address
        public string BuyerAddress { get; private set; }

        private Buyer()
        {
            // required by EF
        }

        public Buyer(string identity) : this()
        {
            Guard.Against.NullOrEmpty(identity, nameof(identity));

            BuyerId = identity;
        }
    }
}
