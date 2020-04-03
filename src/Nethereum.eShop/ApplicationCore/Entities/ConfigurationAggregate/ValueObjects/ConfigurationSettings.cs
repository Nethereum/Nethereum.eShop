using System.Collections.Generic;

namespace Nethereum.eShop.ApplicationCore.Entities.ConfigurationAggregate
{
    public class EShopConfigurationSettings
    {
        public static class Keys
        {
            public const string PREFIX = "";
            private static string PrefixedKey(string key) => $"{PREFIX}{key}";

            public static readonly string BuyerWalletAddress = PrefixedKey("BuyerWalletAddress");
            public static readonly string PurchasingContractAddress = PrefixedKey("PurchasingContractAddress");
            public static readonly string CurrencySymbol = PrefixedKey("CurrencySymbol");
            public static readonly string CurrencyAddress = PrefixedKey("CurrencyAddress");
            public static readonly string QuantityAddress = PrefixedKey("QuantityAddress");

            public static readonly string AddressRegistryAddress = PrefixedKey("AddressRegistryAddress");
            public static readonly string PoStorageAddress = PrefixedKey("PoStorageAddress");
            public static readonly string FundingAddress = PrefixedKey("FundingAddress");
            public static readonly string BusinessPartnerStorageAddress = PrefixedKey("BusinessPartnerStorageAddress");
            public static readonly string SellerAdminAddress = PrefixedKey("SellerAdminAddress");
        }

        public EShopConfigurationSettings(IEnumerable<Setting> settings)
        {
            EShop = new EShopConfiguration(settings);
            Seller = new SellerConfiguration(settings);
            CreateFakePurchaseOrdersJob = new CreateFakePurchaseOrdersJobConfiguration(settings);
            ProcessPurchaseOrderEvents = new ProcessPurchaseOrderEventsJobConfiguration(settings);

            BuyerWalletAddress = settings.GetString(Keys.BuyerWalletAddress);
            PurchasingContractAddress = settings.GetString(Keys.PurchasingContractAddress);
            CurrencySymbol = settings.GetString(Keys.CurrencySymbol);
            CurrencyAddress = settings.GetString(Keys.CurrencyAddress);

            AddressRegistryAddress = settings.GetString(Keys.AddressRegistryAddress);
            PoStorageAddress = settings.GetString(Keys.PoStorageAddress);
            FundingAddress = settings.GetString(Keys.FundingAddress);
            BusinessPartnerStorageAddress = settings.GetString(Keys.BusinessPartnerStorageAddress);
            SellerAdminAddress = settings.GetString(Keys.SellerAdminAddress);

        }

        public void UpdateSettings(List<Setting> settings)
        {
            settings.SetOrCreateString(Keys.BuyerWalletAddress, BuyerWalletAddress);
            settings.SetOrCreateString(Keys.PurchasingContractAddress, PurchasingContractAddress);
            settings.SetOrCreateString(Keys.CurrencySymbol, CurrencySymbol);
            settings.SetOrCreateString(Keys.CurrencyAddress, CurrencyAddress);

            settings.SetOrCreateString(Keys.AddressRegistryAddress, AddressRegistryAddress);
            settings.SetOrCreateString(Keys.PoStorageAddress, PoStorageAddress);
            settings.SetOrCreateString(Keys.FundingAddress, FundingAddress);
            settings.SetOrCreateString(Keys.BusinessPartnerStorageAddress, BusinessPartnerStorageAddress);
            settings.SetOrCreateString(Keys.SellerAdminAddress, SellerAdminAddress);

            EShop.UpdateSettings(settings);
            Seller.UpdateSettings(settings);
            CreateFakePurchaseOrdersJob.UpdateSettings(settings);
            ProcessPurchaseOrderEvents.UpdateSettings(settings);
        }

        public EShopConfiguration EShop { get; set; }

        public SellerConfiguration Seller { get; set; }

        public CreateFakePurchaseOrdersJobConfiguration CreateFakePurchaseOrdersJob { get; set; }

        public ProcessPurchaseOrderEventsJobConfiguration ProcessPurchaseOrderEvents { get; set; }

        public string BuyerWalletAddress { get; set; }
        public string PurchasingContractAddress { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyAddress { get; set; }

        public string PoStorageAddress { get; set; }

        public string FundingAddress { get; set; }

        public string AddressRegistryAddress { get; set; }

        public string BusinessPartnerStorageAddress { get; set; }

        public string SellerAdminAddress { get; set; }
    }
}
