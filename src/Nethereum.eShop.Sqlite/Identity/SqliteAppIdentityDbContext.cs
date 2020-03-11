using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Nethereum.eShop.EntityFramework.Identity;

namespace Nethereum.eShop.Sqlite.Identity
{
    public class SqliteAppIdentityDbContext : AppIdentityDbContext
    {
        public SqliteAppIdentityDbContext(DbContextOptions<SqliteAppIdentityDbContext> options) : base(options)
        {
        }
    }

    public class SqliteAppIdentityContextDesignTimeFactory : IDesignTimeDbContextFactory<SqliteAppIdentityDbContext>
    {
        public SqliteAppIdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqliteAppIdentityDbContext>();
            optionsBuilder.UseSqlite(
                "Data Source=C:/temp/designtime_eshop_app_identity.db;");

            return new SqliteAppIdentityDbContext(
                optionsBuilder.Options);
        }
    }


}
