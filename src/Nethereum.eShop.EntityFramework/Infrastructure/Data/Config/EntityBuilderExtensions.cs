using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nethereum.eShop.Infrastructure.Data.Config
{
    public static class EntityBuilderExtensions
    {
        private const int HashLength = 67;
        private const int AddressLength = 43;
        private const int BigIntegerLength = 100;
        private const int Bytes32Length = 32;
        private const int UriLength = 512;
        private const int GtinLength = 14;

        public static PropertyBuilder<decimal> IsPrice(this PropertyBuilder<decimal> prop)
        {
            return prop.HasColumnType("decimal(18,2)");
        }

        public static PropertyBuilder<string> IsHash(this PropertyBuilder<string> prop)
        {
            return prop.HasMaxLength(HashLength);
        }

        public static PropertyBuilder<string> IsAddress(this PropertyBuilder<string> prop)
        {
            return prop.HasMaxLength(AddressLength);
        }

        public static PropertyBuilder<string> IsBigInteger(this PropertyBuilder<string> prop)
        {
            return prop.HasMaxLength(BigIntegerLength);
        }

        public static PropertyBuilder<string> IsBytes32(this PropertyBuilder<string> prop)
        {
            return prop.HasMaxLength(Bytes32Length);
        }

        public static PropertyBuilder<string> IsUri(this PropertyBuilder<string> prop)
        {
            return prop.HasMaxLength(UriLength);
        }

        public static PropertyBuilder<string> IsGtin(this PropertyBuilder<string> prop)
        {
            return prop.HasMaxLength(GtinLength);
        }
    }
}
