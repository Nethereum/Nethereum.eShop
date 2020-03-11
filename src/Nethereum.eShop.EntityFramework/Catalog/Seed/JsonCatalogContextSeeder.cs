using Microsoft.Extensions.Logging;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.EntityFramework.Catalog.Seed
{

    public class JsonCatalogContextSeeder : ICatalogContextSeeder
    {
        private readonly string _productImportJsonFile;
        private readonly CatalogContext catalogContext;

        public JsonCatalogContextSeeder(CatalogSettings catalogSettings, CatalogContext catalogContext)
        {
            this.catalogContext = catalogContext;
            _productImportJsonFile = catalogSettings.CatalogSeedJsonFile;
        }

        private CatalogImportDto GetImportDataFromJsonFile()
        {
            using (var textReader = File.OpenText(_productImportJsonFile))
            {
                using (var jsonTextReader = new JsonTextReader(textReader))
                {
                    var serializer = new JsonSerializer();
                    var importData = serializer.Deserialize<CatalogImportDto>(jsonTextReader);
                    return importData;
                }
            }
        }

        public async Task SeedAsync(ILoggerFactory loggerFactory, int? retry = 0)
        {

            int retryForAvailability = retry.Value;
            try
            {

                var importData = GetImportDataFromJsonFile();
                Cleanse(importData);

                // TODO: Only run this if using a real database
                // context.Database.Migrate();

                // if we are not running an in memory db - we may need allow id's to be inserted intead of auto generated
                // context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Students ON");

                if (!catalogContext.CatalogBrands.Any())
                {
                    catalogContext.CatalogBrands.AddRange(
                        importData.CatalogBrands);

                    await catalogContext.SaveChangesAsync();
                }

                if (!catalogContext.CatalogTypes.Any())
                {
                    catalogContext.CatalogTypes.AddRange(
                        importData.CatalogTypes);

                    await catalogContext.SaveChangesAsync();
                }

                if (!catalogContext.CatalogItems.Any())
                {
                    catalogContext.CatalogItems.AddRange(
                        importData.CatalogItems);

                    await catalogContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<JsonCatalogContextSeeder>();
                    log.LogError(ex.Message);
                    await SeedAsync(loggerFactory, retryForAvailability);
                }
            }
        }

        private void Cleanse(CatalogImportDto importData)
        {
            foreach(var catalogItem in importData.CatalogItems)
            {
                if(catalogItem.Name.Length > 50)
                {
                    catalogItem.Name = catalogItem.Name.Substring(0, 50);
                }
            }
        }
    }
}
