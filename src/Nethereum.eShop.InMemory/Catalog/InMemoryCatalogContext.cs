using MediatR;
using Microsoft.EntityFrameworkCore;
using Nethereum.eShop.EntityFramework.Catalog;

namespace Nethereum.eShop.InMemory.Catalog
{
    public class InMemoryCatalogContext : CatalogContext
    {
        public InMemoryCatalogContext(DbContextOptions<InMemoryCatalogContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //config is in EntityFramework project
            builder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }
    }
}

