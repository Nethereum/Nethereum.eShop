using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

            //var config = new JobHostConfiguration();
            //config.UseTimers();
            //var host = new JobHost(config);
            //// The following code ensures that the WebJob will be running continuously
            //host.RunAndBlock();

            IConfigurationRoot config = null;
            var hostBuilder = Host.CreateDefaultBuilder(args);

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

            var host = hostBuilder.Build();
            using (host)
            {

                host.Run();
            }
        }
    }
}
