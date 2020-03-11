using Nethereum.eShop.ApplicationCore.Entities.RulesEngine;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.Infrastructure.Cache;
using System.Threading.Tasks;

namespace Nethereum.eShop.EntityFramework.Catalog.Cache
{
    public class RuleTreeCache : GeneralCache<RuleTree>, IRuleTreeCache
    {
        public RuleTreeCache()
        {}

        public async Task<RuleTree> GetByIdAsync(string id)
        {
            return new RuleTree(new RuleTreeSeed());
        }

        public async Task<RuleTree> GetLastRuleTreeCreatedAsync()
        {
            return new RuleTree(new RuleTreeSeed());
        }
    }
}
