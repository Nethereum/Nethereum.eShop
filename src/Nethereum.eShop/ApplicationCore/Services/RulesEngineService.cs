using Ardalis.GuardClauses;
using Nethereum.eShop.ApplicationCore.Entities;
using Nethereum.eShop.ApplicationCore.Interfaces;
using System;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Services
{
    public class RulesEngineService : IRulesEngineService
    {
        private readonly IAsyncCache<RuleTree> _ruletreeRepository;

        public RulesEngineService(IAsyncCache<RuleTree> ruletreeRepo)
        {
            // TODO: Initialize the data domain

            _ruletreeRepository = ruletreeRepo;
        }

        public async Task<RuleTree> CreateRuleTreeAsync(RuleTreeOrigin origin)
        {
            // TODO: Do all the work to help initialize the RuleTree

            return new RuleTree(origin);
        }

        public async Task Execute(RuleTree TargetRuleTree)
        {
            // TODO: Do all the work to execute the RuleTree - and return a report?
        }
    }
}
