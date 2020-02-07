using System;

namespace Nethereum.eShop.ApplicationCore.Entities
{
    public class RuleTreeOrigin: BaseEntity
    {
        public RuleTreeOrigin()
        {
            RuleTreeId        = null;
            RuleTreeOriginUrl = null;
            Owner             = null;
        }

        public RuleTreeOrigin(string psRuleTreeId, string psOriginUrl, string psOwner)
        {
            RuleTreeId        = psRuleTreeId;
            RuleTreeOriginUrl = psOriginUrl;
            Owner             = psOwner;
        }

        public string RuleTreeId { get; set; }

        public string OwnerName { get; set; }

        public string Owner { get; set; }

        public string RuleTreeOriginUrl { get; set; }

        public bool IsValid()
        {
            bool bIsValid = true;

            if (String.IsNullOrEmpty(RuleTreeId))
                bIsValid = false;

            if (String.IsNullOrEmpty(RuleTreeOriginUrl))
                bIsValid = false;

            if (String.IsNullOrEmpty(Owner))
                bIsValid = false;


            return bIsValid;
        }
    }
}
