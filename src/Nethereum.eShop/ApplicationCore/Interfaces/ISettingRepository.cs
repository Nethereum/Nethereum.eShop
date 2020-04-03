using Nethereum.eShop.ApplicationCore.Entities.ConfigurationAggregate;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{

    public interface ISettingRepository : IAsyncRepository<Setting>, IRepository
    {
        Setting Add(Setting setting);
        Setting Update(Setting setting);
        Task<EShopConfigurationSettings> GetEShopConfigurationSettingsAsync();
        Task UpdateAsync(EShopConfigurationSettings configurationSettings);
    }
}
