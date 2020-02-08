using System;

namespace Nethereum.eShop.ApplicationCore.Entities
{
    public class RuleTree: BaseEntity
    {
        public RuleTree(RuleTreeOrigin pOrigin)
        {
            TreeOrigin = pOrigin;           
        }

        #region Properties

        public RuleTreeOrigin TreeOrigin { get; set; }

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
