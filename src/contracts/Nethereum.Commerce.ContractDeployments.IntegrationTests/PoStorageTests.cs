using FluentAssertions;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;
using Xunit;
using Xunit.Abstractions;
using static Nethereum.Commerce.ContractDeployments.IntegrationTests.PoTestHelpers;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    /// <summary>
    /// Tests for the PoStorage.sol contract in isolation. These tests
    /// store and retrieve dummy POs direct to storage. In real use, this
    /// contract is only called by the Purchasing.sol application layer
    /// and PO fields will be filled differently.
    /// </summary>
    [Collection("Contract Deployment Collection")]
    public class PoStorageTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixture _contracts;

        public PoStorageTests(ContractDeploymentsFixture fixture, ITestOutputHelper output)
        {
            // ContractDeploymentsFixture performed a complete deployment.
            // See Output window -> Tests for deployment logs.
            _contracts = fixture;
            _output = output;
        }

        [Fact]
        public async void ShouldStoreAndRetrievePo()
        {
            // Create a PO to store
            uint poNumber = GetRandomInt();
            string quoteSignerAddress = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610";
            uint quoteId = GetRandomInt();
            Po poExpected = CreatePoForPoStorageContract(poNumber, quoteSignerAddress, quoteId);

            // Store PO
            var txReceipt = await _contracts.Deployment.PoStorageServiceLocal.SetPoRequestAndWaitForReceiptAsync(poExpected);
            txReceipt.Status.Value.Should().Be(1);

            // Retrieve PO 
            var poActualDto = await _contracts.Deployment.PoStorageServiceLocal.GetPoQueryAsync(poNumber);
            var poActual = poActualDto.Po;

            // They should be the same
            CheckEveryPoFieldMatches(poExpected, poActual);
        }

        [Fact]
        public async void ShouldStoreAndRetrievePoBySellerAndQuote()
        {
            // Create a PO to store
            uint poNumberExpected = GetRandomInt();
            string quoteSignerAddress = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610";
            uint quoteId = GetRandomInt();
            Po poExpected = CreatePoForPoStorageContract(poNumberExpected, quoteSignerAddress, quoteId);

            // Store PO
            var txReceipt = await _contracts.Deployment.PoStorageServiceLocal.SetPoRequestAndWaitForReceiptAsync(poExpected);
            txReceipt.Status.Value.Should().Be(1);

            // Retrieve PO number by address and nonce
            var poNumberActual = await _contracts.Deployment.PoStorageServiceLocal.GetPoNumberByEshopIdAndQuoteQueryAsync(poExpected.EShopId, poExpected.QuoteId);

            // They should be the same
            poNumberActual.Should().Be(poNumberExpected);
        }

        [Fact]
        public async void ShouldStoreUpdateAndRetrievePo()
        {
            // Create a PO to store
            uint poNumber = GetRandomInt();
            string quoteSignerAddress = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610";
            uint quoteId = 666; 
            Po poExpected = CreatePoForPoStorageContract(poNumber, quoteSignerAddress, quoteId);

            // Store PO
            var txReceipt = await _contracts.Deployment.PoStorageServiceLocal.SetPoRequestAndWaitForReceiptAsync(poExpected);
            txReceipt.Status.Value.Should().Be(1);

            // Retrieve PO v1 
            var poActualv1 = (await _contracts.Deployment.PoStorageServiceLocal.GetPoQueryAsync(poNumber)).Po;
            DisplaySeparator(_output, "PO v1");
            DisplayPoHeader(_output, poActualv1);
            for (int i = 0; i < poActualv1.PoItems.Count; i++)
            {
                DisplayPoItem(_output, poActualv1.PoItems[i]);
            }
            
            // Update PO
            poExpected.QuoteId = 314;
            poExpected.PoItems[0].Status = Contracts.ContractEnums.PoItemStatus.Accepted;
            txReceipt = await _contracts.Deployment.PoStorageServiceLocal.SetPoRequestAndWaitForReceiptAsync(poExpected);
            txReceipt.Status.Value.Should().Be(1);
            
            // Retrieve PO v2 
            var poActualv2 = (await _contracts.Deployment.PoStorageServiceLocal.GetPoQueryAsync(poNumber)).Po;
            DisplaySeparator(_output, "PO v2");
            DisplayPoHeader(_output, poActualv2);
            for (int i = 0; i < poActualv2.PoItems.Count; i++)
            {
                DisplayPoItem(_output, poActualv2.PoItems[i]);
            }

            // They should be the same
            CheckEveryPoFieldMatches(poExpected, poActualv2);
        }
    }
}
