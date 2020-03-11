using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.ApplicationCore.Queries.Catalog;
using Nethereum.eShop.ApplicationCore.Queries.Orders;
using Nethereum.eShop.ApplicationCore.Queries.Quotes;
using Nethereum.eShop.EntityFramework.Catalog;
using Nethereum.eShop.InMemory.Catalog.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.eShop.InMemory.Catalog
{
    public class InMemoryEShopDbBootrapper : EShopDbBootstrapperBase, IEShopDbBootstrapper
    {
        public void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogContext, InMemoryCatalogContext>(c =>
                c.UseInMemoryDatabase("Catalog"));
        }

        public void AddQueries(IServiceCollection services, IConfiguration configuration) 
        {
            services.AddScoped<ICatalogQueries, CatalogQueries>();
            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddScoped<IQuoteQueries, QuoteQueries>();
        }

        public override Task EnsureCreatedAsync(IServiceProvider serviceProvider, IConfiguration configuration, CancellationToken cancellationToken = default) => Task.CompletedTask;
    }
}
