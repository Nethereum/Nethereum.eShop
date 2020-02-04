using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nethereum.eShop.Infrastructure.Data.Config
{
    public static class EntityBuilderExtensions
    {
        private const int Hash = 67;
        private const int Address = 43;
        private const int BigIntegerLength = 100;

        public static PropertyBuilder<decimal> IsPrice(this PropertyBuilder<decimal> prop)
        {
            return prop.HasColumnType("decimal(18,2)");
        }

        public static PropertyBuilder<string> IsHash(this PropertyBuilder<string> prop)
        {
            return prop.HasMaxLength(Hash);
        }

        public static PropertyBuilder<string> IsAddress(this PropertyBuilder<string> prop)
        {
            return prop.HasMaxLength(Address);
        }

        public static PropertyBuilder<string> IsBigInteger(this PropertyBuilder<string> prop)
        {
            return prop.HasMaxLength(BigIntegerLength);
        }
    }
}
