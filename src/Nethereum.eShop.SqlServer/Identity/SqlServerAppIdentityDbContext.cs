using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Nethereum.eShop.EntityFramework.Identity;

namespace Nethereum.eShop.SqlServer.Identity
{
    public class SqlServerAppIdentityDbContext : AppIdentityDbContext
    {
        public SqlServerAppIdentityDbContext(DbContextOptions<SqlServerAppIdentityDbContext> options) : base(options)
        {
        }
    }

    public class SqlServerAppIdentityContextDesignTimeFactory : IDesignTimeDbContextFactory<SqlServerAppIdentityDbContext>
    {
        public SqlServerAppIdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqlServerAppIdentityDbContext>();
            optionsBuilder.UseSqlServer(
                "Server=localhost\\sqldev;Integrated Security=true;Initial Catalog=eShop;");

            return new SqlServerAppIdentityDbContext(
                optionsBuilder.Options);
        }
    }
}
