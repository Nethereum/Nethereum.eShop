using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.eShop.ApplicationCore.Interfaces;

namespace Nethereum.eShop.Infrastructure.Data.Config
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
        }
    }
}
