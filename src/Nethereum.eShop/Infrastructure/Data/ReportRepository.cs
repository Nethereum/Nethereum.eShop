using Microsoft.EntityFrameworkCore;
using Nethereum.eShop.ApplicationCore.Entities.RulesEngine;
using Nethereum.eShop.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nethereum.eShop.Infrastructure.Data
{
    // NOTE: To be determined where reports will be stored in the database, if at all -
    // We may either need to 1.) alter EFRepository so that it has a DBContext supporting
    // the Reports or 2.) create a new Repository class that will support the Reports
    public class ReportRepository : EfRepository<RuleTreeReport>, IReportRepository
    {
        public ReportRepository(CatalogContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<RuleTreeReport>> GetByRuleTreeIdAsync(int ruleTreeId)
        {
            return new List<RuleTreeReport>();
        }
    }
}