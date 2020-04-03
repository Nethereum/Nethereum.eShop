using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Nethereum.eShop.EntityFramework.Catalog;

namespace Nethereum.eShop.Sqlite.Catalog
{
    public class SqliteCatalogContext : CatalogContext
    {
        public SqliteCatalogContext(DbContextOptions<SqliteCatalogContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //config is in EntityFramework project
            builder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }
    }

    public class SqliteCatalogContextDesignTimeFactory : IDesignTimeDbContextFactory<SqliteCatalogContext>
    {
        public SqliteCatalogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqliteCatalogContext>();
            optionsBuilder.UseSqlite(
                "Data Source=C:/temp/designtime_eshop_catalog.db;");

            return new SqliteCatalogContext(
                optionsBuilder.Options,
                null);
        }
    }
}

