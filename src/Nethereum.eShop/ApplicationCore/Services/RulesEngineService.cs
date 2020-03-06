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
        private readonly IRuleTreeCache _ruleTreeRepository;

        private readonly IRulesEngineInitializer _rulesEngineInitializer;

        public RulesEngineService(IRuleTreeCache ruleTreeRepo, IRulesEngineInitializer rulesEngineInitializer)
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

            var NewRuleTree = new RuleTree(origin);

            await _ruleTreeRepository.AddAsync(NewRuleTree).ConfigureAwait(false);

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

            RuleTreeSeed quoteTreeSeed =
                new RuleTreeSeed(1, "QuoteTree", sBizRulesUrl, "default");

            var RuleTreeIsCached = await _ruleTreeRepository.ContainsAsync(quoteTreeSeed.Id).ConfigureAwait(false);

            if (RuleTreeIsCached)
            {
                return await _ruleTreeRepository.GetByIdAsync(quoteTreeSeed.Id).ConfigureAwait(false);
            }
            else
            {
                var DomainSeed = new RulesDomainSeed(new HashSet<Type>() { typeof(Quote), typeof(QuoteItem) });

                return await CreateRuleTreeAsync(new RulesDomain(DomainSeed), quoteTreeSeed).ConfigureAwait(false);
            }
        }

        public async Task<RuleTree> GetQuoteItemRuleTree()
        {
            var sBizRulesUrl = _rulesEngineInitializer.GetQuoteItemBizRulesFileUrl();

            RuleTreeSeed quoteItemTreeSeed =
                new RuleTreeSeed(2, "QuoteItemTree", sBizRulesUrl, "default");

            var RuleTreeIsCached = await _ruleTreeRepository.ContainsAsync(quoteItemTreeSeed.Id).ConfigureAwait(false);

            if (RuleTreeIsCached)
            {
                return await _ruleTreeRepository.GetByIdAsync(quoteItemTreeSeed.Id).ConfigureAwait(false);
            }
            else
            {
                var DomainSeed = new RulesDomainSeed(new HashSet<Type>() { typeof(Quote), typeof(QuoteItem) });

                return await CreateRuleTreeAsync(new RulesDomain(DomainSeed), quoteItemTreeSeed).ConfigureAwait(false);
            }
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
