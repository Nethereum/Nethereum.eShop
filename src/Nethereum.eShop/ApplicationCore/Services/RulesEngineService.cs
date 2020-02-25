using Ardalis.GuardClauses;
using Nethereum.eShop.ApplicationCore.Entities.RulesEngine;
using Nethereum.eShop.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Services
{
    public class RulesEngineService : IRulesEngineService
    {
        private readonly IAsyncCache<RuleTree> _ruleTreeRepository;

        public RulesEngineService(IAsyncCache<RuleTree> ruleTreeRepo)
        {
            // TODO: Initialize the data domain

            _ruleTreeRepository = ruleTreeRepo;
        }

        public async Task<RulesDomain> CreateRulesDomainAsync(RulesDomainSeed domainSeed)
        {
            // TODO: Do all the work to help initialize the RuleTree

            return new RulesDomain(domainSeed);
        }

        public async Task<RuleTree> CreateRuleTreeAsync(RulesDomain domain, RuleTreeOrigin origin)
        {
            // TODO: Do all the work to help initialize the RuleTree

            return new RuleTree(origin);
        }

        public async Task<RuleTreeReport> ExecuteAsync(RuleTree targetRuleTree, RuleTreeRecord targetRecord)
        {
            // TODO: Do all the work to execute the RuleTree - and return a report?

            return new RuleTreeReport(new RuleTreeOrigin());
        }

        public async Task<IReadOnlyList<RuleTree>> ListRuleTreeCacheAsync()
        {
            return await _ruleTreeRepository.ListAllAsync().ConfigureAwait(false);
        }
    }
}
