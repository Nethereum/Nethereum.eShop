using Nethereum.eShop.ApplicationCore.Entities.ConfigurationAggregate;
using Nethereum.eShop.ApplicationCore.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.EntityFramework.Catalog.Repositories
{
    public class SettingRepository : EfRepository<Setting>, ISettingRepository
    {
        public SettingRepository(CatalogContext dbContext) : base(dbContext)
        {
        }

        public async Task<EShopConfigurationSettings> GetEShopConfigurationSettingsAsync()
        {
            var settings = await ListAllAsync().ConfigureAwait(false);
            return new EShopConfigurationSettings(settings);
        }

        public async Task UpdateAsync(EShopConfigurationSettings configurationSettings)
        {
            var settings = await ListAllAsync().ConfigureAwait(false);
            var list = settings.ToList();
            configurationSettings.UpdateSettings(list);
            _dbContext.UpdateRange(list);
        }
    }
}
