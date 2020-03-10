using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Nethereum.eShop.Infrastructure.Identity;

namespace Nethereum.eShop.SqlServer.Infrastructure.Data.Config
{
    public class SqlServerAppIdentityContextDesignTimeFactory : IDesignTimeDbContextFactory<AppIdentityDbContext>
    {
        public AppIdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppIdentityDbContext>();
            optionsBuilder.UseSqlServer(
                "Server=localhost\\sqldev;Integrated Security=true;Initial Catalog=eShop;",
                b => b.MigrationsAssembly("Nethereum.eShop.SqlServer.Migrations"));

            return new AppIdentityDbContext(
                optionsBuilder.Options);
        }
    }
}
