using Microsoft.Extensions.Configuration;
using Nethereum.Commerce.Contracts.Deployment;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests.Config
{
    public class ContractDeploymentsFixture : IAsyncLifetime
    {
        // Deployed contracts
        public ContractDeployment Deployment { get; internal set; }

        private readonly IMessageSink _diagnosticMessageSink;

        public ContractDeploymentsFixture(IMessageSink diagnosticMessageSink)
        {
            _diagnosticMessageSink = diagnosticMessageSink;
            var appConfig = ConfigurationUtils.Build(Array.Empty<string>(), "UserSecret");

            // New deployment
            var contractDeploymentConfig = appConfig.GetSection("NewDeployment").Get<ContractDeploymentConfig>();
            // ...or attach to an existing deployment, swap to this:
            // var contractDeploymentConfig = appConfig.GetSection("ExistingDeployment").Get<ContractConnectExistingConfig>();
            Deployment = new ContractDeployment(contractDeploymentConfig, new DiagnosticMessageSinkLogger(_diagnosticMessageSink));
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
