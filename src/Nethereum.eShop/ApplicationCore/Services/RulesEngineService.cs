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

        #region Properties

        private RuleTree _defaultRuleTree;

        #endregion

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

        public async Task<RuleTree> CreateRuleTreeAsync(RulesDomain domain, RuleTreeSeed origin)
        {
            // TODO: Do all the work to help initialize the RuleTree

            return new RuleTree(origin);
        }

        public async Task<RuleTreeReport> ExecuteAsync(RuleTree targetRuleTree, RuleTreeRecord targetRecord)
        {
            // TODO: Do all the work to execute the RuleTree - and return a report?

            return new RuleTreeReport(new RuleTreeSeed());
        }

        public async Task<RuleTree> GetDefaultRuleTree()
        {
            return _defaultRuleTree;
        }

        public async Task<IReadOnlyList<RuleTree>> ListRuleTreeCacheAsync()
        {
            return await _ruleTreeRepository.ListAllAsync().ConfigureAwait(false);
        }

        public async Task SetDefaultRuleTree(RuleTree defaultRuleTree)
        {
            _defaultRuleTree = defaultRuleTree;
        }

        public async Task<RuleTreeRecord> Transform(object originalObject)
        {
            // TODO: Convert the object to a RuleTreeRecord - using reflection?

            return new RuleTreeRecord();
        }
    }
}
