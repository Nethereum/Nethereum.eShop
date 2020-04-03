using Microsoft.EntityFrameworkCore;
using Nethereum.eShop.EntityFramework.Identity;

namespace Nethereum.eShop.InMemory.Infrastructure.Identity
{
    public class InMemoryAppIdentityDbContext : AppIdentityDbContext
    {
        public InMemoryAppIdentityDbContext(DbContextOptions<InMemoryAppIdentityDbContext> options) : base(options)
        {
        }
    }
}
