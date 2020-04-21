using FluentAssertions;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts.Deployment;
using Nethereum.Commerce.Contracts.Deployment.CompleteSample;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Nethereum.Commerce.ContractDeployments.IntegrationTests.PoTestHelpers;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests.CompleteSample
{
    [Trait("Complete Deployment", "")]
    public class CompleteSampleDeploymentTests
    {
        private readonly ITestOutputHelper _output;
        private readonly TestOutputHelperLogger _xunitlogger;

        public CompleteSampleDeploymentTests(ITestOutputHelper output)
        {
            _output = output;
            _xunitlogger = new TestOutputHelperLogger(_output);
        }

        [Fact]
        public async void ShouldDeployCompleteSample()
        {
            // Web3
            var blockchainUrl = "http://testchain.nethereum.com:8545";
            var privateKey = "0x7580e7fb49df1c861f0050fae31c2224c6aba908e116b8da44ee8cd927b990b0";
            var web3 = new Web3.Web3(new Account(privateKey), blockchainUrl);

            // Perform complete sample deployment: a shop, two buyers, two sellers and some mock dai
            var csdc = new CompleteSampleDeploymentConfig()
            {
                Eshop = new EshopDeploymentCompleteSampleConfig()
                {
                    EshopId = "Satoshi" + GetRandomString(),
                    EshopDescription = "Description",
                    QuoteSigners = new List<string>()
                    {
                        "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9",
                        "0x94618601FE6cb8912b274E5a00453949A57f8C1e"
                    }
                },

                Seller = new SellerDeploymentCompleteSampleConfig()
                {
                    SellerId = "Charlie" + GetRandomString(),
                    SellerDescription = "Description"
                },

                Seller02 = new SellerDeploymentCompleteSampleConfig()
                {
                    SellerId = "David" + GetRandomString(),
                    SellerDescription = "Description"
                }
            };
            var completeSampleDeployment = CompleteSampleDeployment.CreateFromNewDeployment(
                 web3,
                 csdc,
                 _xunitlogger);
            Func<Task> act = async () => await completeSampleDeployment.InitializeAsync();
            await act.Should().NotThrowAsync();
        }
    }
}
