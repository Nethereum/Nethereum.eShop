using Nethereum.eShop.ApplicationCore.Interfaces;

namespace Nethereum.eShop.ApplicationCore.Entities.ConfigurationAggregate
{

    public class Setting : BaseEntity, IAggregateRoot
    {
        public Setting(string key, string val)
        {
            Key = key;
            Value = val;
        }

        private Setting()
        {
            // for EF
        }

        public string Key { get; private set; }
        public string Value { get; private set; }

        public void SetValue(string val)
        {
            Value = val;
        }
    }
}
