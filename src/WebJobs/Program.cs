using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nethereum.BlockchainProcessing.ProgressRepositories;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.ApplicationCore.Services;
using Nethereum.eShop.Infrastructure.Data;
using Nethereum.eShop.WebJobs.Configuration;
using Nethereum.eShop.WebJobs.Jobs;

namespace Nethereum.eShop.WebJobs
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = null;
            EshopConfiguration eShopConfig = null;
            var hostBuilder = Host.CreateDefaultBuilder(args);

            hostBuilder.ConfigureServices(c =>
            {
                c.AddDbContext<CatalogContext>((serviceProvider, options) =>
                    options.UseSqlServer(config.GetConnectionString("CatalogConnection")));

                // config
                c.AddSingleton(eShopConfig);

                // supporting services
                c.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
                c.AddScoped<IQuoteRepository, QuoteRepository>();
                c.AddScoped<IOrderRepository, OrderRepository>();
                c.AddScoped<IOrderService, OrderService>();

                // TODO: There's a bug in the BlockProgressRepo
                // It's using string ordering instead of numeric ordering to get the last block processed
                // so 997 is considered higher than 2061
                // this has been fixed but is awaiting a PR merge and nuget release (probably 3.7.2)

                // blockchain event log progress db
                //var progressDbConnectionString = config.GetConnectionString("BlockchainProcessingProgressDb");
                //IBlockchainDbContextFactory blockchainDbContextFactory =
                //    new SqlServerCoreBlockchainDbContextFactory(
                //        progressDbConnectionString, DbSchemaNames.dbo);

                //using (var progressContext = blockchainDbContextFactory.CreateContext())
                //{
                //    progressContext.Database.EnsureCreated();
                //}

                //IBlockProgressRepository progressRepo = new BlockProgressRepository(blockchainDbContextFactory);
                c.AddSingleton<IBlockProgressRepository, JsonFileBlockProgressRepository>();

                // jobs
                c.AddScoped<IProcessPuchaseOrderEventLogs, ProcessPurchaseOrderEventLogs>();
                c.AddScoped<ICreateFakePurchaseOrders, CreateFakePurchaseOrders>();
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
                eShopConfig = config.GetSection("EshopConfiguration").Get<EshopConfiguration>();
            });

            hostBuilder.ConfigureLogging((context, b) =>
            {
                b.AddConsole();
            });

            using (var host = hostBuilder.Build())
            {
                using (var scope = host.Services.CreateScope())
                {
                    host.Run();
                }
            }
        }
    }
}
