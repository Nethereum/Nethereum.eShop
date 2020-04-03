using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.ConfigurationAggregate;

namespace Nethereum.eShop.EntityFramework.Catalog.EntityBuilders
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public virtual void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.Property(b => b.Key).HasMaxLength(100);
            builder.Property(b => b.Value);

            builder.HasIndex(b => b.Key);
        }
    }
}
