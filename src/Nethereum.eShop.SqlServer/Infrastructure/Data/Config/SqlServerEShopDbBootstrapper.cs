using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.ApplicationCore.Queries.Catalog;
using Nethereum.eShop.ApplicationCore.Queries.Orders;
using Nethereum.eShop.ApplicationCore.Queries.Quotes;
using Nethereum.eShop.Infrastructure.Data;
using Nethereum.eShop.Infrastructure.Data.Config;
using Nethereum.eShop.Infrastructure.Identity;

namespace Nethereum.eShop.SqlServer.Infrastructure.Data.Config
{
    public class SqlServerEShopDbBootstrapper : EShopDbBootstrapperBase, IEShopDbBootstrapper
    {
        private const string ConnectionName = "CatalogConnection";

        public void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogContext>((serviceProvider, options) =>
                options.UseSqlServer(configuration.GetConnectionString(ConnectionName)));

            // Point the CatalogContext at this assembly for the Model Builder Configurations
            // these have some SQL specific tweaks
            services.AddSingleton<IModelBuilderAssemblyHandler<CatalogContext>>(
                new ModelBuilderAssemblyHandler<CatalogContext>(this.GetType().Assembly));
        }

        public void AddQueries(IServiceCollection services, IConfiguration configuration)
        {
            string queryConnectionString = configuration.GetConnectionString(ConnectionName);
            services.AddSingleton<IQuoteQueries>(new QuoteQueries(queryConnectionString));
            services.AddSingleton<IOrderQueries>(new OrderQueries(queryConnectionString));
            services.AddSingleton<ICatalogQueries>(new CatalogQueries(queryConnectionString));
        }
    }

    public class SqlServerEShopAppIdentityDbBootstrapper : IEShopIdentityDbBootstrapper
    {
        public void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
        }
    }
}
