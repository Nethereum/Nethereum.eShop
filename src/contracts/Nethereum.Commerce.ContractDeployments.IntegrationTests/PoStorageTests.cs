using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;
using System.Collections.Generic;
using System;
using Nethereum.Commerce.Contracts;
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
            string buyerAddress = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610";
            uint buyerNonce = 2;
            Po poExpected = CreateTestPo(poNumber, buyerAddress, buyerNonce);

            // Store PO
            var txReceipt = await _contracts.PoStorageService.SetPoRequestAndWaitForReceiptAsync(poExpected);
            txReceipt.Status.Value.Should().Be(1);

            // Retrieve PO 
            var poActual = (await _contracts.PoStorageService.GetPoQueryAsync(poNumber)).Po;

            // They should be the same
            CheckEveryPoFieldMatches(poExpected, poActual);
        }

        [Fact]
        public async void ShouldStoreAndRetrievePoByAddressAndNonce()
        {
            // Create a PO to store
            uint poNumberExpected = 314159;
            string buyerAddress = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610";
            uint buyerNonce = 2;
            Po poExpected = CreateTestPo(poNumberExpected, buyerAddress, buyerNonce);

            // Store PO
            var txReceipt = await _contracts.PoStorageService.SetPoRequestAndWaitForReceiptAsync(poExpected);
            txReceipt.Status.Value.Should().Be(1);

            // Retrieve PO number by address and nonce
            var poNumberActual = await _contracts.PoStorageService.GetPoNumberByAddressAndNonceQueryAsync(buyerAddress, buyerNonce);

            // They should be the same
            poNumberActual.Should().Be(poNumberExpected);
        }

        private static void CheckEveryPoFieldMatches(Po poExpected, Po poActual)
        {
            poActual.PoNumber.Should().Be(poExpected.PoNumber);
            poActual.BuyerAddress.Should().Be(poExpected.BuyerAddress);
            poActual.BuyerWalletAddress.Should().Be(poExpected.BuyerWalletAddress);
            poActual.BuyerNonce.Should().Be(poExpected.BuyerNonce);
            poActual.SellerSysId.Should().Be(poExpected.SellerSysId);
            poActual.PoCreateDate.Should().Be(poExpected.PoCreateDate);
            poActual.PoItemCount.Should().Be(poExpected.PoItemCount);
            for (int i = 0; i < poActual.PoItemCount; i++)
            {
                poActual.PoItems[i].PoItemNumber.Should().Be(poExpected.PoItems[i].PoItemNumber);
                poActual.PoItems[i].SoNumber.Should().Be(poExpected.PoItems[i].SoNumber);
                poActual.PoItems[i].SoItemNumber.Should().Be(poExpected.PoItems[i].SoItemNumber);
                poActual.PoItems[i].ProductId.Should().Be(poExpected.PoItems[i].ProductId);
                poActual.PoItems[i].Quantity.Should().Be(poExpected.PoItems[i].Quantity);
                poActual.PoItems[i].Unit.Should().Be(poExpected.PoItems[i].Unit);
                poActual.PoItems[i].QuantityErc20Symbol.Should().Be(poExpected.PoItems[i].QuantityErc20Symbol);
                poActual.PoItems[i].QuantityErc20Address.Should().Be(poExpected.PoItems[i].QuantityErc20Address);
                poActual.PoItems[i].Value.Should().Be(poExpected.PoItems[i].Value);
                poActual.PoItems[i].CurrencyErc20Symbol.Should().Be(poExpected.PoItems[i].CurrencyErc20Symbol);
                poActual.PoItems[i].CurrencyErc20Address.Should().Be(poExpected.PoItems[i].CurrencyErc20Address);
                poActual.PoItems[i].Status.Should().Be(poExpected.PoItems[i].Status);
                poActual.PoItems[i].GoodsIssueDate.Should().Be(poExpected.PoItems[i].GoodsIssueDate);
                poActual.PoItems[i].EscrowReleaseDate.Should().Be(poExpected.PoItems[i].EscrowReleaseDate);
                poActual.PoItems[i].CancelStatus.Should().Be(poExpected.PoItems[i].CancelStatus);
            }
        }

        private static Po CreateTestPo(uint poNumber, string buyerAddress, uint buyerNonce)
        {
            return new Po()
            {
                PoNumber = poNumber,
                BuyerAddress = buyerAddress,
                BuyerWalletAddress = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                BuyerNonce = buyerNonce,
                SellerSysId = "Nethereum.eShop",
                PoCreateDate = 100,
                PoItemCount = 2,
                PoItems = new List<PoItem>()
                {
                    new PoItem()
                    {
                        PoItemNumber = 10,
                        SoNumber = "so1",
                        SoItemNumber = "100",
                        ProductId = "gtin1111",
                        Quantity = 1,
                        Unit = "EA",
                        QuantityErc20Symbol = "NA",
                        QuantityErc20Address = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                        Value = 11,
                        CurrencyErc20Symbol = "DAI",
                        CurrencyErc20Address = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                        Status = PoItemStatus.PoCreated,
                        GoodsIssueDate = 100,
                        EscrowReleaseDate = 100,
                        CancelStatus = PoItemCancelStatus.Initial
                    },
                    new PoItem()
                    {
                        PoItemNumber = 20,
                        SoNumber = "so1",
                        SoItemNumber = "200",
                        ProductId = "gtin2222",
                        Quantity = 2,
                        Unit = "EA",
                        QuantityErc20Symbol = "NA",
                        QuantityErc20Address = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                        Value = 22,
                        CurrencyErc20Symbol = "DAI",
                        CurrencyErc20Address = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                        Status = PoItemStatus.PoCreated,
                        GoodsIssueDate = 200,
                        EscrowReleaseDate = 200,
                        CancelStatus = PoItemCancelStatus.Initial
                    }
                }
            };
        }
    }
}
