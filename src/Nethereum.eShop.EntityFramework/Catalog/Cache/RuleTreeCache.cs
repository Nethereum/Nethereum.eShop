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

        public Task<RuleTree> GetByIdAsync(string id)
        {
            return Task.FromResult(new RuleTree(new RuleTreeSeed()));
        }

        public Task<RuleTree> GetLastRuleTreeCreatedAsync()
        {
            return Task.FromResult(new RuleTree(new RuleTreeSeed()));
        }
    }
}
