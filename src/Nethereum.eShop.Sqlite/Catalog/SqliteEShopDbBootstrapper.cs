using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.ApplicationCore.Queries.Catalog;
using Nethereum.eShop.ApplicationCore.Queries.Orders;
using Nethereum.eShop.ApplicationCore.Queries.Quotes;
using Nethereum.eShop.EntityFramework.Catalog;
using Nethereum.eShop.Sqlite.Catalog.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.eShop.Sqlite.Catalog
{
    public class SqliteEShopDbBootstrapper : EShopDbBootstrapperBase, IEShopDbBootstrapper
    {
        private const string ConnectionName = "CatalogConnection_Sqlite";

        public void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogContext, SqliteCatalogContext>((serviceProvider, options) =>
                options.UseSqlite(configuration.GetConnectionString(ConnectionName)));
        }

        public void AddQueries(IServiceCollection services, IConfiguration configuration)
        {
            string queryConnectionString = configuration.GetConnectionString(ConnectionName);
            services.AddSingleton<IQuoteQueries>(new QuoteQueries(queryConnectionString));
            services.AddSingleton<IOrderQueries>(new OrderQueries(queryConnectionString));
            services.AddSingleton<ICatalogQueries>(new CatalogQueries(queryConnectionString));
        }
    }
}
