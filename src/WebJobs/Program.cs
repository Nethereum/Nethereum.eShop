using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nethereum.BlockchainProcessing.ProgressRepositories;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.ApplicationCore.Services;
using Nethereum.eShop.InMemory.Infrastructure.Data.Config;
using Nethereum.eShop.Sqlite.Infrastructure.Data.Config;
using Nethereum.eShop.SqlServer.Infrastructure.Data.Config;
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

            hostBuilder.ConfigureServices(services =>
            {
                // TODO: Configure MediatR properly - this is just a place holder
                services.AddMediatR(typeof(Program));

                // config
                services.AddSingleton(eShopConfig);

                // db
                var dbBootstrapper = CreateDbBootstrapper(config);
                dbBootstrapper.AddDbContext(services, config);

                //repositories
                dbBootstrapper.AddRepositories(services, config);

                // supporting services
                services.AddScoped<IOrderService, OrderService>();

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
                services.AddSingleton<IBlockProgressRepository, JsonFileBlockProgressRepository>();

                // jobs
                services.AddScoped<IProcessPuchaseOrderEventLogs, ProcessPurchaseOrderEventLogs>();
                services.AddScoped<ICreateFakePurchaseOrders, CreateFakePurchaseOrders>();
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

        private static IEShopDbBootstrapper CreateDbBootstrapper(IConfiguration configuration)
        {
            var name = configuration["CatalogDbProvider"];
            return name switch
            {
                "SqlServer" => new SqlServerEShopDbBootstrapper(),
                "Sqlite" => new SqliteEShopDbBootstrapper(),
                _ => new InMemoryEShopDbBootrapper()
            };
        }
    }
}
