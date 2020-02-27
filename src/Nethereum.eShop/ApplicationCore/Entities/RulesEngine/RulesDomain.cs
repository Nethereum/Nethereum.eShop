using System;
using System.Collections.Generic;

namespace Nethereum.eShop.ApplicationCore.Entities.RulesEngine
{
    public class RulesDomain: BaseEntity
    {
        public RulesDomain(RulesDomainSeed seed)
        {
            DomainSeed = seed;
            
            // We will instatiate the IMetadataRetrievable property here
        }

        public RulesDomainSeed DomainSeed { get; protected set; }
    }
}
