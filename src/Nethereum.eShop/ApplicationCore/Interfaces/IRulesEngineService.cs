using Nethereum.eShop.ApplicationCore.Entities.RulesEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IRulesEngineService
    {
        Task<RulesDomain> CreateRulesDomainAsync(RulesDomainSeed domainSeed);

        Task<RuleTree> CreateRuleTreeAsync(RulesDomain domain, RuleTreeSeed origin);

        Task<RuleTreeReport> ExecuteAsync(RuleTree targetRuleTree, RuleTreeRecord targetRecord);

        Task<RuleTree> GetDefaultRuleTree();

        Task<IReadOnlyList<RuleTree>> ListRuleTreeCacheAsync();

        Task<RuleTreeRecord> Transform(object originalObject);
    }
}
