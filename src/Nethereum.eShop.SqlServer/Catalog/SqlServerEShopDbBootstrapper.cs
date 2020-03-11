using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.ApplicationCore.Queries.Catalog;
using Nethereum.eShop.ApplicationCore.Queries.Orders;
using Nethereum.eShop.ApplicationCore.Queries.Quotes;
using Nethereum.eShop.EntityFramework.Catalog;
using Nethereum.eShop.SqlServer.Catalog.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.eShop.SqlServer.Catalog
{
    public class SqlServerEShopDbBootstrapper : EShopDbBootstrapperBase, IEShopDbBootstrapper
    {
        private const string ConnectionName = "CatalogConnection_SqlServer";

        public void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogContext, SqlServerCatalogContext>((serviceProvider, options) =>
                options.UseSqlServer(configuration.GetConnectionString(ConnectionName)));
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
