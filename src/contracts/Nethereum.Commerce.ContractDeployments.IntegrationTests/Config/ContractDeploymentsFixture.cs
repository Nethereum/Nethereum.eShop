using Microsoft.Extensions.Configuration;
using Nethereum.Commerce.Contracts.Deployment;
using Nethereum.Web3.Accounts;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests.Config
{
    public class ContractDeploymentsFixture : IAsyncLifetime
    {
        /// <summary>
        /// Web3 representing the "primary user", that is the user with ether
        /// used to deploy and own contracts and to run tests.
        /// </summary>
        public Web3.Web3 Web3 { get; internal set; }

        /// <summary>
        /// Account representing the "secondary user", that is a user with
        /// ether but without owner rights to any contract. Used for testing 
        /// security.
        /// </summary>
        public Account SecondaryUser { get; internal set; }

        /// <summary>
        /// eShop contract collection, newly deployed or connected to existing
        /// </summary>
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

            // Secondary users
            var secondaryUserConfig = appConfig.GetSection("SecondaryUsers").Get<SecondaryUserConfig>();
            SecondaryUser = new Account(secondaryUserConfig.UserPrivateKey);
        }

        public async Task InitializeAsync()
        {
            await Deployment.InitializeAsync();

            // Transfer money from main web3 primary user to secondary users
            LogSeparator();
            Log("Transferring Ether to secondary users...");
            var txEtherTransfer = await Web3.Eth.GetEtherTransferService()
                .TransferEtherAndWaitForReceiptAsync(SecondaryUser.Address, 1.00m);
            Log($"Transfer tx status: {txEtherTransfer.Status.Value}");
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        private void LogSeparator()
        {
            Log();
            Log("----------------------------------------------------------------");
            Log();
        }

        private void Log() => Log(string.Empty);

        private void Log(string message)
        {
            _diagnosticMessageSink.OnMessage(new DiagnosticMessage(message));
        }
    }
}
