using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Nethereum.eShop.EntityFramework.Catalog;

namespace Nethereum.eShop.SqlServer.Catalog
{
    public class SqlServerCatalogContext : CatalogContext
    {
        public SqlServerCatalogContext(DbContextOptions<SqlServerCatalogContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }

    public class SqlServerCatalogContextDesignTimeFactory : IDesignTimeDbContextFactory<SqlServerCatalogContext>
    {
        public SqlServerCatalogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqlServerCatalogContext>();
            optionsBuilder.UseSqlServer(
                "Server=localhost\\sqldev;Integrated Security=true;Initial Catalog=eShop;");

            return new SqlServerCatalogContext(
                optionsBuilder.Options,
                null);
        }
    }
}
