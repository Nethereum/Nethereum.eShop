using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities;

namespace Nethereum.eShop.EntityFramework.Catalog.EntityBuilders
{
    public static class PostalAddressBuilder
    {
        public static void ConfigureAddress<TParentEntity>(this OwnedNavigationBuilder<TParentEntity, PostalAddress> a) where TParentEntity : class
        {
            a.WithOwner();

            a.Property(a => a.RecipientName)
                .HasMaxLength(255)
                .IsRequired();

            a.Property(a => a.ZipCode)
                .HasMaxLength(18)
                .IsRequired();

            a.Property(a => a.Street)
                .HasMaxLength(180)
                .IsRequired();

            a.Property(a => a.State)
                .HasMaxLength(60);

            a.Property(a => a.Country)
                .HasMaxLength(90)
                .IsRequired();

            a.Property(a => a.City)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
