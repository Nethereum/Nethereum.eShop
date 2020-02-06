using Nethereum.eShop.ApplicationCore.Entities;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IRuleTreeService
    {
        Task<RuleTree> CreateRuleTreeAsync(RuleTreeOrigin origin);

        Task Execute(RuleTree TargetRuleTree);
    }
}
