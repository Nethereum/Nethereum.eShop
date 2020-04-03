using System;
using System.Collections.Generic;

namespace Nethereum.eShop.ApplicationCore.Entities.ConfigurationAggregate
{
    public class EShopConfiguration // Value Object
    {
        public static class Keys
        {
            public const string PREFIX = "EShop";
            private static string PrefixedKey(string key) => $"{PREFIX}.{key}";

            public static readonly string Id = PrefixedKey("Id");
            public static readonly string Description = PrefixedKey("Description");
            public static readonly string QuoteSigners = PrefixedKey("QuoteSigners");
        }

        public EShopConfiguration(IEnumerable<Setting> settings)
        {
            Id = settings.GetString(Keys.Id);
            Description = settings.GetString(Keys.Description);
            QuoteSigners = settings.GetStringArray(Keys.QuoteSigners, Array.Empty<string>());
        }

        public string Id { get; set; }
        public string Description { get; set; }
        public string[] QuoteSigners { get; set; }

        internal void UpdateSettings(List<Setting> settings)
        {
            settings.SetOrCreateString(Keys.Id, Id);
            settings.SetOrCreateString(Keys.Description, Description);
            settings.SetOrCreateStringArray(Keys.QuoteSigners, QuoteSigners);
        }
    }
}
