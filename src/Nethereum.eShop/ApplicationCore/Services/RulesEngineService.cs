using Ardalis.GuardClauses;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
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

        private readonly IRulesEngineInitializer _rulesEngineInitializer;

        public RulesEngineService(IAsyncCache<RuleTree> ruleTreeRepo, IRulesEngineInitializer rulesEngineInitializer)
        {
            // TODO: Initialize the data domain

            _ruleTreeRepository = ruleTreeRepo;

            _rulesEngineInitializer = rulesEngineInitializer;
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

        public async Task<RuleTree> GetQuoteRuleTree()
        {
            var sBizRulesUrl = _rulesEngineInitializer.GetQuoteBizRulesFileUrl();

            RuleTreeSeed defaultTreeSeed =
                new RuleTreeSeed("QuoteTree", sBizRulesUrl, "default");

            var DomainSeed = new RulesDomainSeed(new HashSet<Type>() { typeof(Quote), typeof(QuoteItem) });

            return await CreateRuleTreeAsync(new RulesDomain(DomainSeed), defaultTreeSeed).ConfigureAwait(false);
        }

        public async Task<RuleTree> GetQuoteItemRuleTree()
        {
            var sBizRulesUrl = _rulesEngineInitializer.GetQuoteItemBizRulesFileUrl();

            RuleTreeSeed defaultTreeSeed =
                new RuleTreeSeed("QuoteItemTree", sBizRulesUrl, "default");

            var DomainSeed = new RulesDomainSeed(new HashSet<Type>() { typeof(Quote), typeof(QuoteItem) });

            return await CreateRuleTreeAsync(new RulesDomain(DomainSeed), defaultTreeSeed).ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<RuleTree>> ListRuleTreeCacheAsync()
        {
            return await _ruleTreeRepository.ListAllAsync().ConfigureAwait(false);
        }

        public async Task<RuleTreeRecord> Transform(object originalObject)
        {
            // TODO: Convert the object to a RuleTreeRecord - using reflection?

            return new RuleTreeRecord();
        }
    }
}
