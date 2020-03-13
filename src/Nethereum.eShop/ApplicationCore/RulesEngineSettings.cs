using System;
using System.Collections.Generic;
using System.Text;

namespace Nethereum.eShop
{
    public class RulesEngineSettings
    {
        public string QuoteBizRulesFileUrl { get; set; }

        public string QuoteItemBizRulesFileUrl { get; set; }

        public int BizEngineRetriesUponFailure { get; set; }
    }
}
