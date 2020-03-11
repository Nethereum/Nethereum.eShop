using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.eShop.EntityFramework.Identity
{
    public abstract class EShopAppIdentityDbBootstrapperBase
    {
        public virtual Task EnsureCreatedAsync(IServiceProvider serviceProvider, IConfiguration configuration, CancellationToken cancellationToken = default)
        {
            if (ApplyMigrationsOnStartup(configuration))
            {
                var context = serviceProvider.GetRequiredService<AppIdentityDbContext>();
                return context.Database.MigrateAsync(cancellationToken);
            }
            return Task.CompletedTask;
        }

        protected virtual bool ApplyMigrationsOnStartup(IConfiguration configuration)
        {
            return configuration.GetValue<bool>("IdentityApplyMigrationsOnStartup", false);
        }
    }
}
