using Nethereum.eShop.ApplicationCore.Entities;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IRulesEngineService
    {
        Task<RuleTree> CreateRuleTreeAsync(RuleTreeOrigin origin);

        Task Execute(RuleTree TargetRuleTree);
    }
}
