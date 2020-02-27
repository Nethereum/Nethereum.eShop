using System;
using System.Text;

using Nethereum.eShop.ApplicationCore.Entities.RulesEngine;

namespace Nethereum.eShop.ApplicationCore.Exceptions
{
    public class RuleTreeInitException : Exception
    {
        public RuleTreeInitException(StringBuilder ruleTreeId) : base($"No ruletree found with id {ruleTreeId}")
        {
        }

        public RuleTreeInitException(string message) : base(message)
        {
        }

        public RuleTreeInitException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public RuleTreeInitException(string message, RuleTreeSeed failedTree = null) : base(message)
        {
            FailedRuleTree = failedTree;
        }

        public RuleTreeInitException(string message, Exception innerException, RuleTreeSeed failedTree = null) : base(message, innerException)
        {
            FailedRuleTree = failedTree;
        }

        #region Properties

        public RuleTreeSeed FailedRuleTree { get; protected set; }

        #endregion
    }
}