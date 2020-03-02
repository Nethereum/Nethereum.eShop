using Microsoft.Extensions.Configuration;
using Nethereum.Commerce.Contracts.Deployment;
using Nethereum.Web3.Accounts;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests.Config
{
    public class ContractDeploymentsFixture : IAsyncLifetime
    {
        public Web3.Web3 Web3 { get; internal set; }

        // Deployed contracts
        public ContractDeployment Deployment { get; internal set; }

        private readonly IMessageSink _diagnosticMessageSink;

        public ContractDeploymentsFixture(IMessageSink diagnosticMessageSink)
        {
            _diagnosticMessageSink = diagnosticMessageSink;
            var appConfig = ConfigurationUtils.Build(Array.Empty<string>(), "UserSecret");

            // Web3
            var web3Config = appConfig.GetSection("Web3Config").Get<Web3Config>();

            var url = web3Config.BlockchainUrl;
            var privateKey = web3Config.TransactionCreatorPrivateKey;
            var account = new Account(privateKey);
            Web3 = new Web3.Web3(account, url);

            // New deployment
            var contractDeploymentConfig = appConfig.GetSection("NewDeployment").Get<ContractDeploymentConfig>();
            // ...or attach to an existing deployment, swap to this:
            // var contractDeploymentConfig = appConfig.GetSection("ExistingDeployment").Get<ContractConnectExistingConfig>();
            Deployment = new ContractDeployment(Web3, contractDeploymentConfig, new DiagnosticMessageSinkLogger(_diagnosticMessageSink));


            // Setup users - transfer money from main web3 primary user to here, the secondary user
            // 0x62a38a21d890ecaa4c7e336b12e64f84233bf7ba
            // 0xef373978c0a4d8b556bc55ed861e803bb297a97b159fee72f2cd1257ce27e6f3

        }

        public async Task InitializeAsync()
        {
            await Deployment.InitializeAsync();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
