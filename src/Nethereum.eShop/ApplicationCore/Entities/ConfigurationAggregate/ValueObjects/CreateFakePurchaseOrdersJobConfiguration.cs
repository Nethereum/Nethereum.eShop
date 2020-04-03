using System;
using System.Collections.Generic;

namespace Nethereum.eShop.ApplicationCore.Entities.ConfigurationAggregate
{
    public class CreateFakePurchaseOrdersJobConfiguration // Value Object
    {
        public CreateFakePurchaseOrdersJobConfiguration(IEnumerable<Setting> settings)
        {
            Enabled = settings.GetBool("CreateFakePurchaseOrdersJob.Enabled", false);
        }

        public bool Enabled { get; set; }

        public void UpdateSettings(List<Setting> settings)
        {
            settings.SetOrCreateBool("CreateFakePurchaseOrdersJob.Enabled", Enabled);
        }
    }
}
