using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Nethereum.eShop.ApplicationCore.Interfaces;

namespace Nethereum.eShop.ApplicationCore.Entities.RulesEngine
{
    public class RuleTreeRecord : BaseEntity, IRecord
    {
        public RuleTreeRecord()
        {
        }

        #region Interface Methods

        public void AddData(string attrName, string attrValue) { }

        public string GetData(string attrName) { return String.Empty; }

        #endregion

    }
}
