using System.Collections.Generic;
using System.Numerics;

namespace Nethereum.eShop.ApplicationCore.Entities.ConfigurationAggregate
{
    public class ProcessPurchaseOrderEventsJobConfiguration // Value Object
    {
        public static class Keys
        {
            public const string PREFIX = "ProcessPurchaseOrderEventsJob";
            private static string PrefixedKey(string key) => $"{PREFIX}.{key}";

            public static readonly string Enabled = PrefixedKey("Enabled");
            public static readonly string BlockProgressJsonFile = PrefixedKey("BlockProgressJsonFile");
            public static readonly string MinimumStartingBlock = PrefixedKey("MinimumStartingBlock");
            public static readonly string NumberOfBlocksPerBatch = PrefixedKey("NumberOfBlocksPerBatch");
            public static readonly string MinimumBlockConfirmations = PrefixedKey("MinimumBlockConfirmations");
            public static readonly string TimeoutMs = PrefixedKey("TimeoutMs");
        }

        public ProcessPurchaseOrderEventsJobConfiguration(IEnumerable<Setting> settings)
        {
            Load(settings);
        }

        private void Load(IEnumerable<Setting> settings)
        {
            Enabled = settings.GetBool(Keys.Enabled, false);
            BlockProgressJsonFile = settings.GetString(Keys.BlockProgressJsonFile);
            MinimumStartingBlock = settings.GetBigIntegerOrNull(Keys.MinimumStartingBlock);
            NumberOfBlocksPerBatch = settings.GetInt(Keys.NumberOfBlocksPerBatch);
            MinimumBlockConfirmations = settings.GetInt(Keys.MinimumBlockConfirmations);
            TimeoutMs = settings.GetInt(Keys.TimeoutMs);
        }

        public void UpdateSettings(List<Setting> settings)
        {
            settings.SetOrCreateBool(Keys.Enabled, Enabled);
            settings.SetOrCreateString(Keys.BlockProgressJsonFile, BlockProgressJsonFile);
            settings.SetOrCreateNullableBigInteger(Keys.MinimumStartingBlock, MinimumStartingBlock);
            settings.SetOrCreateInt(Keys.NumberOfBlocksPerBatch, NumberOfBlocksPerBatch);
            settings.SetOrCreateInt(Keys.MinimumBlockConfirmations, MinimumBlockConfirmations);
            settings.SetOrCreateInt(Keys.TimeoutMs, TimeoutMs);
        }

        public bool Enabled { get; set; }
        public string BlockProgressJsonFile { get; set; }
        public BigInteger? MinimumStartingBlock { get; set; }
        public int NumberOfBlocksPerBatch { get; set; }
        public int MinimumBlockConfirmations { get; set; }
        public int TimeoutMs { get; set; }


    } 
}
