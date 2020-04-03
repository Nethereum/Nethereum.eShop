using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Nethereum.eShop.EntityFramework.Identity;

namespace Nethereum.eShop.MySql.Identity
{
    public class MySqlAppIdentityDbContext : AppIdentityDbContext
    {
        public MySqlAppIdentityDbContext(DbContextOptions<MySqlAppIdentityDbContext> options) : base(options)
        {
        }
    }

    public class MySqlAppIdentityDbContextDesignTimeFactory : IDesignTimeDbContextFactory<MySqlAppIdentityDbContext>
    {
        public MySqlAppIdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MySqlAppIdentityDbContext>();
            optionsBuilder.UseMySql("server=localhost;database=library;user=user;password=password");

            return new MySqlAppIdentityDbContext(
                optionsBuilder.Options);
        }
    }
}
