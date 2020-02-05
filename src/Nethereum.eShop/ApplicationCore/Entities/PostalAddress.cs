using System;

namespace Nethereum.eShop.ApplicationCore.Entities
{
    public class PostalAddress // ValueObject
    {
        public String Name { get; private set; }

        public String Street { get; private set; }

        public String City { get; private set; }

        public String State { get; private set; }

        public String Country { get; private set; }

        public String ZipCode { get; private set; }

        private PostalAddress() { }

        public PostalAddress(string name, string street, string city, string state, string country, string zipcode)
        {
            Name = name;
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipcode;
        }
    }
}
