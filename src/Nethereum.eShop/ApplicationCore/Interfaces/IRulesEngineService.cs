using Nethereum.eShop.ApplicationCore.Entities.RulesEngine;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IRulesEngineService
    {
        Task<RulesDomain> CreateRulesDomainAsync();

        Task<RuleTree> CreateRuleTreeAsync(RulesDomain domain, RuleTreeOrigin origin);

        Task<RuleTreeReport> ExecuteAsync(RuleTree TargetRuleTree);
    }
}
