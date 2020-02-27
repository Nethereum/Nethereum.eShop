using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.ApplicationCore.Entities.RulesEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nethereum.eShop.Infrastructure.Data
{
    public class RuleTreeCache : IRuleTreeCache
    {
        public RuleTreeCache()
        {}

        public async Task<RuleTree> GetByIdAsync(string id)
        {
            return new RuleTree(new RuleTreeSeed());
        }

        public async Task<IReadOnlyList<RuleTree>> ListAllAsync()
        {
            return new List<RuleTree>();
        }

        public async Task<RuleTree> AddAsync(RuleTree entity)
        {
            return new RuleTree(new RuleTreeSeed());
        }

        public async Task DeleteAsync(RuleTree entity)
        {

        }

        public async Task<RuleTree> GetLastRuleTreeCreatedAsync()
        {
            return new RuleTree(new RuleTreeSeed());
        }
    }
}
