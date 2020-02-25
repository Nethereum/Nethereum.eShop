using Nethereum.BlockchainProcessing.ProgressRepositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Nethereum.eShop.WebJobs
{
    public class JsonFileBlockProgressRepository: JsonBlockProgressRepository
    {
        public JsonFileBlockProgressRepository(string jsonFile):base(
                    () => Task.FromResult(File.Exists(jsonFile)),
                    async (json) => await File.WriteAllTextAsync(jsonFile, json),
                    async () => await File.ReadAllTextAsync(jsonFile))
        {

        }
    }
}
