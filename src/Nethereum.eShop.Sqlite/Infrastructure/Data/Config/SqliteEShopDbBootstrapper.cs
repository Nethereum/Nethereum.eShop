using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.ApplicationCore.Queries.Catalog;
using Nethereum.eShop.ApplicationCore.Queries.Orders;
using Nethereum.eShop.ApplicationCore.Queries.Quotes;
using Nethereum.eShop.Infrastructure.Data;
using Nethereum.eShop.Infrastructure.Data.Config;
using Nethereum.eShop.Sqlite.ApplicationCore.Queries.Catalog;
using Nethereum.eShop.Sqlite.ApplicationCore.Queries.Orders;
using Nethereum.eShop.Sqlite.ApplicationCore.Queries.Quotes;
using System;
using System.Threading.Tasks;

namespace Nethereum.eShop.Sqlite.Infrastructure.Data.Config
{
    public class SqliteEShopDbBootstrapper : EShopDbBootstrapperBase, IEShopDbBootstrapper
    {
        private const string ConnectionName = "CatalogConnection";

        public void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogContext>((serviceProvider, options) =>
                options.UseSqlite(configuration.GetConnectionString(ConnectionName)));

            // Point the CatalogContext at this assembly for the Model Builder Configurations
            services.AddSingleton<IModelBuilderAssemblyHandler<CatalogContext>>(
                new ModelBuilderAssemblyHandler<CatalogContext>(typeof(CatalogContext).Assembly));
        }

        public void AddQueries(IServiceCollection services, IConfiguration configuration)
        {
            string queryConnectionString = configuration.GetConnectionString(ConnectionName);
            services.AddSingleton<IQuoteQueries>(new QuoteQueries(queryConnectionString));
            services.AddSingleton<IOrderQueries>(new OrderQueries(queryConnectionString));
            services.AddSingleton<ICatalogQueries>(new CatalogQueries(queryConnectionString));
        }

        public Task EnsureCreatedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<CatalogContext>();
            return context.Database.EnsureCreatedAsync();
        }
    }
}
