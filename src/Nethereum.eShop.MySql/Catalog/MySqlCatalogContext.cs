using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Nethereum.eShop.EntityFramework.Catalog;

namespace Nethereum.eShop.MySql.Catalog
{
    public class MySqlCatalogContext : CatalogContext
    {
        public MySqlCatalogContext(DbContextOptions<MySqlCatalogContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //config is in EntityFramework project
            builder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }
    }

    public class MySqlCatalogContextContextDesignTimeFactory : IDesignTimeDbContextFactory<MySqlCatalogContext>
    {
        public MySqlCatalogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MySqlCatalogContext>();
            optionsBuilder.UseMySql("server=localhost;database=library;user=user;password=password");
            return new MySqlCatalogContext(optionsBuilder.Options, null);
        }
    }
}
