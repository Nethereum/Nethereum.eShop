using Nethereum.BlockchainProcessing.ProgressRepositories;
using Nethereum.eShop.ApplicationCore.Interfaces;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;

namespace Nethereum.eShop.WebJobs
{
    public class JsonFileBlockProgressRepository : IBlockProgressRepository
    {
        private readonly ISettingRepository _settingRepository;
        private IBlockProgressRepository _innerRepo;

        public JsonFileBlockProgressRepository(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public async Task<BigInteger?> GetLastBlockNumberProcessedAsync()
        {
            await InitRepo();
            return await _innerRepo.GetLastBlockNumberProcessedAsync().ConfigureAwait(false);
        }
        public async Task UpsertProgressAsync(BigInteger blockNumber)
        {
            await InitRepo();
            await _innerRepo.UpsertProgressAsync(blockNumber).ConfigureAwait(false);
        }

        private async Task InitRepo()
        {
            if (_innerRepo == null)
            {
                var config = await _settingRepository.GetEShopConfigurationSettingsAsync().ConfigureAwait(false);
                _innerRepo = new PrivateJsonFileBlockProgressRepository(config.ProcessPurchaseOrderEvents.BlockProgressJsonFile);
            }
        }

        public class PrivateJsonFileBlockProgressRepository : JsonBlockProgressRepository
        {
            public PrivateJsonFileBlockProgressRepository(string jsonFile) : base(
                () => Task.FromResult(File.Exists(jsonFile)),
                        async (json) => await File.WriteAllTextAsync(jsonFile, json),
                        async () => await File.ReadAllTextAsync(jsonFile))
            {

            }
        }
    }

}
