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
            uint poNumber = 314159;
            string approverAddress = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610";
            uint quoteId = 2;
            Po poExpected = CreateTestPo(poNumber, approverAddress, quoteId);

            // Store PO
            var txReceipt = await _contracts.PoStorageService.SetPoRequestAndWaitForReceiptAsync(poExpected);
            txReceipt.Status.Value.Should().Be(1);

            // Retrieve PO 
            var poActual = (await _contracts.PoStorageService.GetPoQueryAsync(poNumber)).Po;

            // They should be the same
            CheckEveryPoFieldMatches(poExpected, poActual);
        }

        [Fact]
        public async void ShouldStoreAndRetrievePoBySellerAndQuote()
        {
            // Create a PO to store
            uint poNumberExpected = 314159;
            string approverAddress = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610";
            uint quoteId = 2;
            Po poExpected = CreateTestPo(poNumberExpected, approverAddress, quoteId);

            // Store PO
            var txReceipt = await _contracts.PoStorageService.SetPoRequestAndWaitForReceiptAsync(poExpected);
            txReceipt.Status.Value.Should().Be(1);

            // Retrieve PO number by address and nonce
            var poNumberActual = await _contracts.PoStorageService.GetPoNumberBySellerAndQuoteQueryAsync(poExpected.SellerId, poExpected.QuoteId);

            // They should be the same
            poNumberActual.Should().Be(poNumberExpected);
        }

        private static void CheckEveryPoFieldMatches(Po poExpected, Po poActual)
        {
            poActual.PoNumber.Should().Be(poExpected.PoNumber);
            poActual.BuyerAddress.Should().Be(poExpected.BuyerAddress);
            poActual.ReceiverAddress.Should().Be(poExpected.ReceiverAddress);
            poActual.BuyerWalletAddress.Should().Be(poExpected.BuyerWalletAddress);
            poActual.QuoteId.Should().Be(poExpected.QuoteId);
            poActual.QuoteExpiryDate.Should().Be(poExpected.QuoteExpiryDate);
            poActual.ApproverAddress.Should().Be(poExpected.ApproverAddress);
            poActual.PoType.Should().Be(poExpected.PoType);
            poActual.SellerId.Should().Be(poExpected.SellerId);
            poActual.PoCreateDate.Should().Be(poExpected.PoCreateDate);
            poActual.PoItemCount.Should().Be(poExpected.PoItemCount);
            for (int i = 0; i < poActual.PoItemCount; i++)
            {
                poActual.PoItems[i].PoNumber.Should().Be(poExpected.PoItems[i].PoNumber);
                poActual.PoItems[i].PoItemNumber.Should().Be(poExpected.PoItems[i].PoItemNumber);
                poActual.PoItems[i].SoNumber.Should().Be(poExpected.PoItems[i].SoNumber);
                poActual.PoItems[i].SoItemNumber.Should().Be(poExpected.PoItems[i].SoItemNumber);
                poActual.PoItems[i].ProductId.Should().Be(poExpected.PoItems[i].ProductId);
                poActual.PoItems[i].Quantity.Should().Be(poExpected.PoItems[i].Quantity);
                poActual.PoItems[i].Unit.Should().Be(poExpected.PoItems[i].Unit);
                poActual.PoItems[i].QuantitySymbol.Should().Be(poExpected.PoItems[i].QuantitySymbol);
                poActual.PoItems[i].QuantityAddress.Should().Be(poExpected.PoItems[i].QuantityAddress);
                poActual.PoItems[i].CurrencyValue.Should().Be(poExpected.PoItems[i].CurrencyValue);
                poActual.PoItems[i].CurrencySymbol.Should().Be(poExpected.PoItems[i].CurrencySymbol);
                poActual.PoItems[i].CurrencyAddress.Should().Be(poExpected.PoItems[i].CurrencyAddress);
                poActual.PoItems[i].Status.Should().Be(poExpected.PoItems[i].Status);
                poActual.PoItems[i].GoodsIssuedDate.Should().Be(poExpected.PoItems[i].GoodsIssuedDate);
                poActual.PoItems[i].PlannedEscrowReleaseDate.Should().Be(poExpected.PoItems[i].PlannedEscrowReleaseDate);
                poActual.PoItems[i].CancelStatus.Should().Be(poExpected.PoItems[i].CancelStatus);
            }
        }

        private static Po CreateTestPo(uint poNumber, string approverAddress, uint quoteId)
        {
            return new Po()
            {
                PoNumber = poNumber,
                BuyerAddress = "0x37ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                ReceiverAddress = "0x36ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                BuyerWalletAddress = "0x39ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                QuoteId = quoteId,
                QuoteExpiryDate = 1,
                ApproverAddress = approverAddress,
                PoType = PoType.Cash,
                SellerId = "Nethereum.eShop",
                PoCreateDate = 100,
                PoItemCount = 2,
                PoItems = new List<PoItem>()
                {
                    new PoItem()
                    {
                        PoNumber = poNumber,
                        PoItemNumber = 10,
                        SoNumber = "so1",
                        SoItemNumber = "100",
                        ProductId = "gtin1111",
                        Quantity = 1,
                        Unit = "EA",
                        QuantitySymbol = "NA",
                        QuantityAddress = "0x40ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                        CurrencyValue = 11,
                        CurrencySymbol = "DAI",
                        CurrencyAddress = "0x41ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                        Status = PoItemStatus.Created,
                        GoodsIssuedDate = 100,
                        GoodsReceivedDate = 0,
                        PlannedEscrowReleaseDate = 100,
                        IsEscrowReleased = false,
                        CancelStatus = PoItemCancelStatus.Initial
                    },
                    new PoItem()
                    {
                        PoNumber = poNumber,
                        PoItemNumber = 20,
                        SoNumber = "so1",
                        SoItemNumber = "200",
                        ProductId = "gtin2222",
                        Quantity = 2,
                        Unit = "EA",
                        QuantitySymbol = "NA",
                        QuantityAddress = "0x42ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                        CurrencyValue = 22,
                        CurrencySymbol = "DAI",
                        CurrencyAddress = "0x43ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                        Status = PoItemStatus.Created,
                        GoodsIssuedDate = 200,
                        GoodsReceivedDate = 0,
                        PlannedEscrowReleaseDate = 200,
                        IsEscrowReleased = false,
                        CancelStatus = PoItemCancelStatus.Initial
                    }
                }
            };
        }
    }
}
