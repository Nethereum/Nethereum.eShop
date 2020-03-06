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

        public string GetQuoteBizRulesFileUrl()
        {
            return _rulesEngineSettings.QuoteBizRulesFileUrl;
        }

        public string GetQuoteItemBizRulesFileUrl()
        {
            return _rulesEngineSettings.QuoteItemBizRulesFileUrl;
        }

        public int GetBizEngineRetriesOnFailure()
        {
            return _rulesEngineSettings.BizEngineRetriesUponFailure;
        }
    }
}