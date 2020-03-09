using Nethereum.BlockchainProcessing.ProgressRepositories;
using Nethereum.eShop.WebJobs.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Nethereum.eShop.WebJobs
{
    public class JsonFileBlockProgressRepository: JsonBlockProgressRepository
    {
        public JsonFileBlockProgressRepository(EshopConfiguration eshopConfiguration):
            this(eshopConfiguration.PurchaseOrderEventLogConfiguration.BlockProgressJsonFile)
        {

        }

        private JsonFileBlockProgressRepository(string jsonFile):base(
            () => Task.FromResult(File.Exists(jsonFile)),
                    async (json) => await File.WriteAllTextAsync(jsonFile, json),
                    async () => await File.ReadAllTextAsync(jsonFile))
        {

        }
    }
}
