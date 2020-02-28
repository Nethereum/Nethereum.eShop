using System;

namespace Nethereum.eShop.ApplicationCore.Entities.RulesEngine
{
    public class RuleTree: BaseEntity
    {
        public RuleTree(RuleTreeSeed pOrigin)
        {
            TreeOrigin = pOrigin;           
        }

        #region Properties

        public RuleTreeSeed TreeOrigin { get; set; }

        #endregion

        #region Advanced Properties

        public bool SerializeToBlockchain { get; set; }

        public uint? MinGasCost { get; set; }

        public uint? MaxGasCost { get; set; }

        private string GroveId { get; set; }
        
        private uint? GroveIndex { get; set; }

        #endregion

    }
}
