using Nethereum.eShop.ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System.Collections.Generic;

namespace Nethereum.eShop.ApplicationCore.Entities.BuyerAggregate
{
    public class Buyer : BaseEntity, IAggregateRoot
    {
        // Expected To Be Ethereum Address
        public string IdentityGuid { get; private set; }

        public Address ShipToAddress { get; private set; }
        public Address BillToAddress { get; private set; }

        private Buyer()
        {
            // required by EF
        }

        public Buyer(string identity, Address shipToAddress, Address billToAddress) : this()
        {
            Guard.Against.NullOrEmpty(identity, nameof(identity));
            Guard.Against.Null(shipToAddress, nameof(shipToAddress));
            Guard.Against.Null(billToAddress, nameof(billToAddress));

            IdentityGuid = identity;
            ShipToAddress = shipToAddress;
            BillToAddress = billToAddress;
        }
    }
}
