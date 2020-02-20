using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.ApplicationCore.Services;
using Nethereum.eShop.Infrastructure.Data;
using System;

namespace Nethereum.eShop.WebJobs
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!ConfigurationSettings.VerifyConfiguration())
            {
                Console.ReadLine();
                return;
            }

            IConfigurationRoot config = null;
            var hostBuilder = Host.CreateDefaultBuilder(args);

            hostBuilder.ConfigureServices(c =>
            {
                c.AddDbContext<CatalogContext>((serviceProvider, options) =>
                    options.UseSqlServer(config.GetConnectionString("CatalogConnection")));

                c.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
                c.AddScoped<IQuoteRepository, QuoteRepository>();
                c.AddScoped<IOrderRepository, OrderRepository>();
                c.AddScoped<IOrderService, OrderService>();
            });

            hostBuilder.ConfigureWebJobs(b =>
            {
                b.AddAzureStorageCoreServices();
                b.AddAzureStorage();
                b.AddTimers();
            });

            hostBuilder.ConfigureAppConfiguration((context, c) =>
            {
                if (context.HostingEnvironment.IsDevelopment())
                    c.AddUserSecrets(typeof(Program).Assembly);

                config = c.Build();
            });

            hostBuilder.ConfigureLogging((context, b) =>
            {
                b.AddConsole();
            });

            using (var host = hostBuilder.Build())
            {
                using (var scope = host.Services.CreateScope())
                {
                    //var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                    host.Run();
                }
            }
        }
    }
}
