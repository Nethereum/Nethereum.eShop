using Nethereum.eShop.ApplicationCore.Entities.RulesEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IRulesEngineService
    {
        Task<RulesDomain> CreateRulesDomainAsync(RulesDomainSeed domainSeed);

        Task<RuleTree> CreateRuleTreeAsync(RulesDomain domain, RuleTreeOrigin origin);

        Task<RuleTreeReport> ExecuteAsync(RuleTree targetRuleTree, RuleTreeRecord targetRecord);

        Task<IReadOnlyList<RuleTree>> ListRuleTreeCacheAsync();
    }
}
