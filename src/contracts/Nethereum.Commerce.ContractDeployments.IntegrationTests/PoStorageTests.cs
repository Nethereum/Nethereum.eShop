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
            Po po = new Po()
            {
                PoNumber = 4,
                BuyerAddress = "0x",
                BuyerWalletAddress = "0x",
                BuyerNonce = 2,
                SellerSysId = "Nethereum.eShop",
                PoCreateDate = DateTime.UtcNow
            };
            po.PoItems = new List<PoItem>()
            {
                new PoItem
                {
                    PoItemNumber = 10,
                    SoNumber = "so1",
                    SoItemNumber = "100",
                    ProductId = "gtin1234",
                    Quantity = 3,
                    Unit = "EA",
                    QuantityErc20Symbol = "NA",
                    QuantityErc20Address = "",
                    Value = 7,
                    CurrencyErc20Symbol = "DAI",
                    CurrencyErc20Address = "",
                    Status = PoItemStatus.PoCreated,
                    GoodsIssueDate = DateTime.UtcNow,
                    EscrowReleaseDate = DateTime.UtcNow,
                    CancelStatus = PoItemCancelStatus.Initial
                }
            };
            po.PoItemCount = (uint)po.PoItems.Count;

            // Store it
            var txReceipt = await _contracts.PoStorageService.SetPoRequestAndWaitForReceiptAsync(po);

            txReceipt.Status.Value.Should().Be(1);
            //var expectedContractAddress = _contracts.EternalStorageService.ContractHandler.ContractAddress;

            // Retrieve it

            // It should be the same
            //actualContractAddress.Should().Be(expectedContractAddress);
        }
    }
}
