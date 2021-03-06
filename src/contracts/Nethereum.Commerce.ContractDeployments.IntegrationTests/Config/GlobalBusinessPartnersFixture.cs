﻿using Microsoft.Extensions.Configuration;
using Nethereum.Commerce.Contracts.Deployment;
using Nethereum.Web3.Accounts;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests.Config
{
    public class GlobalBusinessPartnersFixture : IAsyncLifetime
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
        /// Global Business Partner Storage containing eShops and Sellers
        /// </summary>
        public IBusinessPartnersDeployment BusinessPartnersDeployment { get; internal set; }

        /// <summary>
        /// Global Business Partner Storage contract address
        /// </summary>
        public string BusinessPartnersContractAddress { get; internal set;}

        private readonly IMessageSink _diagnosticMessageSink;

        public GlobalBusinessPartnersFixture(IMessageSink diagnosticMessageSink)
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
        }

        public async Task InitializeAsync()
        {
            // Deploy global business partner storage and give it some master data to
            // be shared across all tests
            BusinessPartnersDeployment = Contracts.Deployment.BusinessPartnersDeployment.CreateFromNewDeployment(Web3, new DiagnosticMessageSinkLogger(_diagnosticMessageSink));
            await BusinessPartnersDeployment.InitializeAsync().ConfigureAwait(false);
            BusinessPartnersContractAddress = BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress;

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
