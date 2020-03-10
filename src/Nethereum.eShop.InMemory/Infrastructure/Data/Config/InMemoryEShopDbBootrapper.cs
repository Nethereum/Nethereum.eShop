using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.ApplicationCore.Queries.Catalog;
using Nethereum.eShop.ApplicationCore.Queries.Orders;
using Nethereum.eShop.ApplicationCore.Queries.Quotes;
using Nethereum.eShop.Infrastructure.Data;
using Nethereum.eShop.Infrastructure.Data.Config;
using Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders;
using Nethereum.eShop.InMemory.ApplicationCore.Queries.Catalog;
using Nethereum.eShop.InMemory.ApplicationCore.Queries.Orders;
using Nethereum.eShop.InMemory.ApplicationCore.Queries.Quotes;
using System;
using System.Threading.Tasks;

namespace Nethereum.eShop.InMemory.Infrastructure.Data.Config
{
    public class InMemoryEShopDbBootrapper : EShopDbBootstrapperBase, IEShopDbBootstrapper
    {
        public void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogContext>(c =>
                c.UseInMemoryDatabase("Catalog"));

            // for in-memory, we'll use the basic model builders
            services.AddSingleton<IModelBuilderAssemblyHandler<CatalogContext>>(
                new ModelBuilderAssemblyHandler<CatalogContext>(typeof(BasketConfiguration).Assembly));
        }

        public void AddQueries(IServiceCollection services, IConfiguration configuration) 
        {
            services.AddScoped<ICatalogQueries, CatalogQueries>();
            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddScoped<IQuoteQueries, QuoteQueries>();
        }

        public Task EnsureCreatedAsync(IServiceProvider serviceProvider) => Task.CompletedTask;
    }
}
