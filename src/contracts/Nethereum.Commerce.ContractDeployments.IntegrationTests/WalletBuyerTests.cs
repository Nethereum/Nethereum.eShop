using FluentAssertions;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using static Nethereum.Commerce.Contracts.ContractEnums;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Collection("Contract Deployment Collection")]
    public class WalletBuyerTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixture _contracts;

        public WalletBuyerTests(ContractDeploymentsFixture fixture, ITestOutputHelper output)
        {
            // ContractDeploymentsFixture performed a complete deployment.
            // See Output window -> Tests for deployment logs.
            _contracts = fixture;
            _output = output;
        }

        [Fact]
        public async void ShouldGetAPo()
        {
            // Prepare a PO
            uint poNumber = 514159;
            string approverAddress = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610";
            uint quoteId = 3;
            Po poExpected = PoHelpers.CreateTestPo(poNumber, approverAddress, quoteId);


        //    // Store PO
        //    var txReceipt = await _contracts.PoStorageService.SetPoRequestAndWaitForReceiptAsync(poExpected);
        //    txReceipt.Status.Value.Should().Be(1);

        //    // Retrieve PO 
        //    var poActual = (await _contracts.PoStorageService.GetPoQueryAsync(poNumber)).Po;

        //    // They should be the same
        //    CheckEveryPoFieldMatches(poExpected, poActual);
        }

        //[Fact]
        //public async void ShouldStoreAndRetrievePoBySellerAndQuote()
        //{
        //    // Create a PO to store
        //    uint poNumberExpected = 314159;
        //    string approverAddress = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610";
        //    uint quoteId = 2;
        //    Po poExpected = CreateTestPo(poNumberExpected, approverAddress, quoteId);

        //    // Store PO
        //    var txReceipt = await _contracts.PoStorageService.SetPoRequestAndWaitForReceiptAsync(poExpected);
        //    txReceipt.Status.Value.Should().Be(1);

        //    // Retrieve PO number by address and nonce
        //    var poNumberActual = await _contracts.PoStorageService.GetPoNumberBySellerAndQuoteQueryAsync(poExpected.SellerId, poExpected.QuoteId);

        //    // They should be the same
        //    poNumberActual.Should().Be(poNumberExpected);
        //}
        
    }
}
