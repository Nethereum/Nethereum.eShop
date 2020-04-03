using System;
using System.Collections.Generic;

namespace Nethereum.eShop.ApplicationCore.Entities.ConfigurationAggregate
{
    public class SellerConfiguration // Value Object
    {
        public static class Keys
        {
            public const string PREFIX = "Seller";
            private static string PrefixedKey(string key) => $"{PREFIX}.{key}";

            public static readonly string Id = PrefixedKey("Id");
            public static readonly string Description = PrefixedKey("Description");
            public static readonly string AdminContractAddress = PrefixedKey("AdminContractAddress");
        }

        public SellerConfiguration(IEnumerable<Setting> settings)
        {
            Id = settings.GetString(Keys.Id);
            Description = settings.GetString(Keys.Description);
            AdminContractAddress = settings.GetString(Keys.AdminContractAddress);
        }

        public void UpdateSettings(List<Setting> settings)
        {
            settings.SetOrCreateString(Keys.Id, Id);
            settings.SetOrCreateString(Keys.Description, Description);
            settings.SetOrCreateString(Keys.AdminContractAddress, AdminContractAddress);
        }

        public string Id { get; set; }
        public string Description { get; set; }
        public string AdminContractAddress { get; set; }
    }
}
