using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Nethereum.eShop.ApplicationCore.Interfaces;

namespace Nethereum.eShop.ApplicationCore.Entities.RulesEngine
{
    public class RuleTreeReport : BaseEntity, IExecutionReport
    {
        public RuleTreeReport(RuleTreeOrigin pOrigin)
        {
            TreeOrigin = pOrigin;
        }

        #region Basic Properties

        public RuleTreeOrigin TreeOrigin { get; set; }

        public uint? NumberOfFailures { get; set; }

        public List<string> RuleSetsExecuted { get; set; }

        public List<string> RulesExecuted { get; set; }

        public List<string> RuleSetsWithWarnings { get; set; }

        public List<string> RuleSetsWithFailures { get; set; }

        public Hashtable RuleSetFailMessages { get; set; }

        public string ErrorMessage { get; set; }

        public Hashtable RecordSnapshot { get; set; }

        public DateTime InvocationTime { get; set; }

        public DateTime CompletionTime { get; set; }

        #endregion

        #region Advanced Properties

        public bool? SimulationMode { get; set; }

        public uint? ExecutionGasCost { get; set; }

        public long InvocationBlockNum { get; set; }

        #endregion

        #region Interface Methods

        public DateTime GetStartTime() { return InvocationTime; }

        public DateTime GetEndTime() { return CompletionTime; }

        #endregion

    }
}
