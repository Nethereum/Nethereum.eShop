using Nethereum.BlockchainProcessing.ProgressRepositories;
using Nethereum.eShop.WebJobs.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Nethereum.eShop.WebJobs
{
    public class JsonFileBlockProgressRepository: JsonBlockProgressRepository
    {
        public JsonFileBlockProgressRepository(EshopConfiguration eshopConfiguration):base(
                    () => Task.FromResult(File.Exists(eshopConfiguration.PurchaseOrderEventLogConfiguration.BlockProgressJsonFile)),
                    async (json) => await File.WriteAllTextAsync(eshopConfiguration.PurchaseOrderEventLogConfiguration.BlockProgressJsonFile, json),
                    async () => await File.ReadAllTextAsync(eshopConfiguration.PurchaseOrderEventLogConfiguration.BlockProgressJsonFile))
        {

        }
    }
}
