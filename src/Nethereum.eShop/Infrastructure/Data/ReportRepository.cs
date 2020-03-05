using Microsoft.EntityFrameworkCore;
using Nethereum.eShop.ApplicationCore.Entities.RulesEngine;
using Nethereum.eShop.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nethereum.eShop.Infrastructure.Data
{
    public class ReportRepository : EfRepository<RuleTreeReport>, IReportRepository
    {
        public ReportRepository(CatalogContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<RuleTreeReport>> GetByRuleTreeIdAsync(int ruleTreeId)
        {
            // NOTE: To be determined where these reports will be stored in the database, if at all
            return new List<RuleTreeReport>();
        }
    }
}