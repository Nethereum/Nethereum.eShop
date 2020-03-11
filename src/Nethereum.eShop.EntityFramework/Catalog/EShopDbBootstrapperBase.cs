using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.EntityFramework.Catalog.Cache;
using Nethereum.eShop.EntityFramework.Catalog.Repositories;
using Nethereum.eShop.EntityFramework.Catalog.Seed;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.eShop.EntityFramework.Catalog
{
    public abstract class EShopDbBootstrapperBase
    {
        public virtual void AddRepositories(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICatalogItemRepository, CatalogItemRepository>();
            services.AddScoped<IQuoteRepository, QuoteRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IStockItemRepository, StockItemRepository>();
            services.AddScoped<IRuleTreeCache, RuleTreeCache>();
        }

        public virtual void AddSeeders(IServiceCollection services, IConfiguration configuration)
        {
            var CatalogSeedJsonFile = configuration["CatalogSeedJsonFile"];

            if (string.IsNullOrEmpty(CatalogSeedJsonFile))
            {
                services.AddScoped<ICatalogContextSeeder, HardCodedCatalogContextSeeder>();
            }
            else
            {
                services.AddScoped<ICatalogContextSeeder, JsonCatalogContextSeeder>();
            }
        }

        public virtual Task EnsureCreatedAsync(IServiceProvider serviceProvider, IConfiguration configuration, CancellationToken cancellationToken = default)
        {
            if (ApplyMigrationsOnStartup(configuration))
            {
                var context = serviceProvider.GetRequiredService<CatalogContext>();
                return context.Database.MigrateAsync(cancellationToken);
            }
            return Task.CompletedTask;
        }

        protected virtual bool ApplyMigrationsOnStartup(IConfiguration configuration)
        {
            return configuration.GetValue<bool>("CatalogApplyMigrationsOnStartup", false);
        }
    }
}
