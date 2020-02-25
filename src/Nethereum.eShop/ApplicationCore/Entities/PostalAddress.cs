using System;

namespace Nethereum.eShop.ApplicationCore.Entities
{
    public class PostalAddress // ValueObject
    {
        public String RecipientName { get; private set; }

        public String Street { get; private set; }

        public String City { get; private set; }

        public String State { get; private set; }

        public String Country { get; private set; }

        public String ZipCode { get; private set; }

        private PostalAddress() { }

        public PostalAddress(string name, string street, string city, string state, string country, string zipcode)
        {
            RecipientName = name;
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipcode;
        }
    }
}
