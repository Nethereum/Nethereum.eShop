using Nethereum.eShop.ApplicationCore.Entities.RulesEngine;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IRulesEngineService
    {
        Task<RuleTree> CreateRuleTreeAsync(RulesDomain domain, RuleTreeOrigin origin);

        Task Execute(RuleTree TargetRuleTree);
    }
}
