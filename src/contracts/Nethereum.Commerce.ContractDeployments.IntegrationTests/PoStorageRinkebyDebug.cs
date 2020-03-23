using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts.AddressRegistry;
using Nethereum.Commerce.Contracts.AddressRegistry.ContractDefinition;
using Nethereum.Commerce.Contracts.EternalStorage;
using Nethereum.Commerce.Contracts.EternalStorage.ContractDefinition;
using Nethereum.Commerce.Contracts.PoStorage;
using Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;
using Nethereum.Web3.Accounts;
using System;
using Xunit;
using Xunit.Abstractions;
using static Nethereum.Commerce.ContractDeployments.IntegrationTests.PoTestHelpers;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    /// <summary>
    /// DEbugging PoStorage.sol
    /// </summary>
    public class PoStorageRinkebyDebug
    {
        private readonly ITestOutputHelper _output;

        public PoStorageRinkebyDebug(ITestOutputHelper output)
        {
            // ContractDeploymentsFixture performed a complete deployment.
            // See Output window -> Tests for deployment logs.
            _output = output;
        }

        [Fact]
        public async void ShouldStoreAndRetrievePoTestNet()
        {
            Web3.Web3 web3 = new Web3.Web3(
                new Account("0x7580e7fb49df1c861f0050fae31c2224c6aba908e116b8da44ee8cd927b990b0"),
                "http://testchain.nethereum.com:8545");

            // Deploy Address Registry
            var addressRegDeployment = new AddressRegistryDeployment();
            var addressRegistryService = await AddressRegistryService.DeployContractAndGetServiceAsync(web3, addressRegDeployment);
            var addressRegOwner = await addressRegistryService.OwnerQueryAsync();

            // Deploy Eternal Storage
            var eternalStorageDeployment = new EternalStorageDeployment();
            var eternalStorageService = await EternalStorageService.DeployContractAndGetServiceAsync(web3, eternalStorageDeployment);
            var eternalStorageOwner = await eternalStorageService.OwnerQueryAsync();

            // Deploy PO Storage
            var poStorageDeployment = new PoStorageDeployment() { ContractAddressOfRegistry = addressRegistryService.ContractHandler.ContractAddress };
            var poStorageService = await PoStorageService.DeployContractAndGetServiceAsync(web3, poStorageDeployment);
            var poStorageOwner = await poStorageService.OwnerQueryAsync();

            // Configure address reg to have EternalStorage address
            var txReceipt = await addressRegistryService.RegisterAddressStringRequestAndWaitForReceiptAsync(
                "EternalStorage",
                eternalStorageService.ContractHandler.ContractAddress);
            txReceipt.Status.Value.Should().Be(1);

            // Configure eternal storage to allow po storage to write to it
            txReceipt = await eternalStorageService.BindAddressRequestAndWaitForReceiptAsync(
                poStorageService.ContractHandler.ContractAddress);
            txReceipt.Status.Value.Should().Be(1);

            // Configure PO storage (this looks up the eternal storage it will use from the address reg)
            txReceipt = await poStorageService.ConfigureRequestAndWaitForReceiptAsync(
                "EternalStorage");
            txReceipt.Status.Value.Should().Be(1);

            // Prepare PO
            uint poNumber = GetRandomInt();
            string quoteSignerAddress = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610";
            uint quoteId = GetRandomInt();
            Po poExpected = CreatePoForPoStorageContract(poNumber, quoteSignerAddress, quoteId);

            // Store PO
            txReceipt = await poStorageService.SetPoRequestAndWaitForReceiptAsync(poExpected);
            txReceipt.Status.Value.Should().Be(1);

            // Retrieve PO 
            var poActualDto = await poStorageService.GetPoQueryAsync(poNumber);
            var poActual = poActualDto.Po;

            // They should be the same
            CheckEveryPoFieldMatches(poExpected, poActual);

        }

        [Fact]
        public async void ShouldStoreAndRetrievePoRinkeby()
        {
            // Get Rinkeby PK from user secrets
            ConfigurationUtils.SetEnvironment("development");
            var appConfig = ConfigurationUtils.Build(Array.Empty<string>(), "Eshop");
            var web3Config = appConfig.GetSection("Web3Config").Get<Web3Config>();
            _output.WriteLine(web3Config.TransactionCreatorPrivateKey);

            // Connect to rinkeby deployment that was done manually
            Web3.Web3 web3 = new Web3.Web3(new Account(web3Config.TransactionCreatorPrivateKey), "https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c");
            PoStorageService pss = new PoStorageService(web3, "0xB1004547f3ACCbd551E6554A4c46Dc390277DE65");

            uint poNumber = GetRandomInt();
            string quoteSignerAddress = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610";
            uint quoteId = GetRandomInt();
            Po poExpected = CreatePoForPoStorageContract(poNumber, quoteSignerAddress, quoteId);

            // Store PO
            var txReceipt = await pss.SetPoRequestAndWaitForReceiptAsync(poExpected);
            txReceipt.Status.Value.Should().Be(1);

            // Retrieve PO 
            var poActualDto = await pss.GetPoQueryAsync(poNumber);
            var poActual = poActualDto.Po;

            // They should be the same
            CheckEveryPoFieldMatches(poExpected, poActual);
            _output.WriteLine($"Po number: {poExpected.PoNumber}");
        }
    }
}
;