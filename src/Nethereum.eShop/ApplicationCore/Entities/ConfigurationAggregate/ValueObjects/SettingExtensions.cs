using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Nethereum.eShop.ApplicationCore.Entities.ConfigurationAggregate
{
    public static class SettingExtensions
    {
        private static Setting GetSingle(this IEnumerable<Setting> settings, string key) 
            => settings.FirstOrDefault(s => s.Key.Equals(key, System.StringComparison.OrdinalIgnoreCase));

        public static string GetString(this IEnumerable<Setting> settings, string key, string defaultValue = null)
        {
            var setting = settings.GetSingle(key);
            return setting?.Value ?? defaultValue;
        }

        public static string[] GetStringArray(this IEnumerable<Setting> settings, string key, string[] defaultValue = null)
        {
            var setting = settings.GetSingle(key);
            return setting?.Value?.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries) ?? defaultValue;
        }

        public static int GetInt(this IEnumerable<Setting> settings, string key, int defaultValue = 0)
        {
            var setting = settings.GetSingle(key);
            if (setting == null) return defaultValue;
            return int.TryParse(setting.Value, out int val) ? val : defaultValue; 
        }

        public static bool GetBool(this IEnumerable<Setting> settings, string key, bool defaultValue = false)
        {
            var setting = settings.GetSingle(key);
            if (setting == null) return defaultValue;
            return bool.TryParse(setting.Value, out bool val) ? val : defaultValue;
        }

        public static BigInteger? GetBigIntegerOrNull(this IEnumerable<Setting> settings, string key)
        {
            var setting = settings.GetSingle(key);
            if (setting == null) return null;
            return BigInteger.TryParse(setting.Value, out BigInteger val) ? val : (BigInteger?)null;
        }

        public static void SetOrCreateString(this List<Setting> settings, string key, string val)
        {
            var setting = settings.GetSingle(key);
            if (setting == null)
            {
                settings.Add(new Setting(key, val));
            }
            else
            {
                setting.SetValue(val);
            }
        }

        public static void SetOrCreateStringArray(this List<Setting> settings, string key, string[] val, char delimeter = ',')
        {
            var setting = settings.GetSingle(key);
            var valueAsString = val == null ? string.Empty : string.Join(delimeter, val);

            if (setting == null)
            {
                settings.Add(new Setting(key, valueAsString));
            }
            else
            {
                setting.SetValue(valueAsString);
            }
        }

        public static void SetOrCreateBool(this List<Setting> settings, string key, bool val)
        {
            var setting = settings.GetSingle(key);
            if (setting == null)
            {
                settings.Add(new Setting(key, val.ToString()));
            }
            else
            {
                setting.SetValue(val.ToString());
            }
        }

        public static void SetOrCreateInt(this List<Setting> settings, string key, int val)
        {
            var setting = settings.GetSingle(key);
            if (setting == null)
            {
                settings.Add(new Setting(key, val.ToString()));
            }
            else
            {
                setting.SetValue(val.ToString());
            }
        }

        public static void SetOrCreateNullableBigInteger(this List<Setting> settings, string key, BigInteger? val)
        {
            var valToWrite = val == null ? null : val.ToString();

            var setting = settings.GetSingle(key);
            if (setting == null)
            {
                settings.Add(new Setting(key, valToWrite));
            }
            else
            {
                setting.SetValue(valToWrite);
            }
        }
    }
}
