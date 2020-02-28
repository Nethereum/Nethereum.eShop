using Nethereum.eShop.ApplicationCore.Interfaces;

namespace Nethereum.eShop.ApplicationCore.Services
{
    public class RulesEngineInitializer : IRulesEngineInitializer
    {
        private readonly RulesEngineSettings _rulesEngineSettings;

        public RulesEngineInitializer(RulesEngineSettings rulesEngineSettings)
        {
            _rulesEngineSettings = rulesEngineSettings;
        }

        public string GetBizRulesFileUrl()
        {
            return _rulesEngineSettings.BizRulesFileUrl;
        }
    }
}