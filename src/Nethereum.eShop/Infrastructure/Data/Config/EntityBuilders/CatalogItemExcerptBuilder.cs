using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities;

namespace Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders
{
    public static class CatalogItemExcerptBuilder
    {
        public static void ConfigureCatalogItemExcerpt<TParentEntity>(this OwnedNavigationBuilder<TParentEntity, CatalogItemExcerpt> a) where TParentEntity : class{

            a.WithOwner();

            a.Property(cio => cio.ProductName)
                .HasMaxLength(50)
                .IsRequired();

            a.Property(cio => cio.PictureUri).IsUri();

            a.Property(cio => cio.Gtin).IsGtin();
        }

    }
}
