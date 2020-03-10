using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Nethereum.eShop.Infrastructure.Data;
using Nethereum.eShop.Infrastructure.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Nethereum.eShop.ApplicationCore.Interfaces;

namespace Nethereum.eShop.Web
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args)
                        .Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    await services.GetRequiredService<IEShopDbBootstrapper>().EnsureCreatedAsync(services);
                    await services.GetRequiredService<IEShopIdentityDbBootstrapper>().EnsureCreatedAsync(services);

                    var catalogContextSeeder = services.GetRequiredService<ICatalogContextSeeder>();
                    await catalogContextSeeder.SeedAsync(loggerFactory);

                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await AppIdentityDbContextSeed.SeedAsync(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
