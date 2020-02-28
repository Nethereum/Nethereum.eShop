using Nethereum.eShop.ApplicationCore.Entities.RulesEngine;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IRuleTreeCache
    {
        Task<RuleTree> GetByIdAsync(string id);

        Task<RuleTree> GetLastRuleTreeCreatedAsync();
    }
}
