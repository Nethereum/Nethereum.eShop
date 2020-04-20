using Microsoft.Extensions.Configuration;
using Nethereum.Commerce.Contracts.Deployment;
using Nethereum.Commerce.Contracts.Deployment.CompleteSample;
using Nethereum.Web3.Accounts;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests.Config
{
    public class CompleteSampleDeploymentFixture : IAsyncLifetime
    {
        /// <summary>
        /// Web3 representing the "primary user", that is the user with ether
        /// used to deploy and own contracts and to run tests.
        /// </summary>
        public Web3.Web3 Web3 { get; internal set; }

        /// <summary>
        /// Web3 representing the "secondary user", that is a user with
        /// ether but without owner rights to any contract. Used for testing 
        /// security.
        /// </summary>
        public Web3.Web3 Web3SecondaryUser { get; internal set; }

        /// <summary>
        /// A new complete sample Eshop deployment along with a new global business partner
        /// storage, two buyers, two sellers and a mock DAI token.
        /// </summary>
        public CompleteSampleDeployment CompleteDeployment { get; internal set; }

        public CompleteSampleDeploymentConfig CompleteSampleDeploymentConfig { get; internal set; }

        private readonly IMessageSink _diagnosticMessageSink;

        public CompleteSampleDeploymentFixture(IMessageSink diagnosticMessageSink)
        {
            _diagnosticMessageSink = diagnosticMessageSink;
            var appConfig = ConfigurationUtils.Build(Array.Empty<string>(), "UserSecret");

            // Web3
            var web3Config = appConfig.GetSection("Web3Config").Get<Web3Config>();
            var privateKey = web3Config.TransactionCreatorPrivateKey;
            Web3 = new Web3.Web3(new Account(privateKey), web3Config.BlockchainUrl);

            // Secondary users
            var secondaryUserConfig = appConfig.GetSection("Web3SecondaryConfig").Get<Web3SecondaryConfig>();
            Web3SecondaryUser = new Web3.Web3(new Account(secondaryUserConfig.UserPrivateKey), web3Config.BlockchainUrl);

            // Complete deploment
            CompleteSampleDeploymentConfig = appConfig.GetSection("CompleteSampleDeploymentConfig").Get<CompleteSampleDeploymentConfig>();
        }

        public async Task InitializeAsync()
        {
            // Deploy complete sample
            var completeSampleDeployment = CompleteSampleDeployment.CreateFromNewDeployment(
                 Web3,
                 CompleteSampleDeploymentConfig,
                 new DiagnosticMessageSinkLogger(_diagnosticMessageSink));
            await completeSampleDeployment.InitializeAsync();
            
            // Transfer Ether from main web3 primary user to secondary users, so secondary users can post tx
            LogSeparator();
            Log("Transferring Ether to secondary users...");
            var txEtherTransfer = await Web3.Eth.GetEtherTransferService()
                .TransferEtherAndWaitForReceiptAsync(Web3SecondaryUser.TransactionManager.Account.Address, 1.00m);
            Log($"Transfer tx status: {txEtherTransfer.Status.Value}");
            LogSeparator();
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
