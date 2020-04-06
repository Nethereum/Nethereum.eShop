using FluentAssertions;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts.PoStorage;
using Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Nethereum.Commerce.ContractDeployments.IntegrationTests.PoTestHelpers;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Collection("Contract Deployment Collection")]
    public class PoStorageAuthTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixture _contracts;

        public PoStorageAuthTests(ContractDeploymentsFixture fixture, ITestOutputHelper output)
        {
            // ContractDeploymentsFixture performed a complete deployment.
            // See Output window -> Tests for deployment logs.
            _contracts = fixture;
            _output = output;
        }

        [Fact]
        public async void ShouldNotBeAbleToStorePoWhenNotRegisteredCaller()
        {
            // Try to store a PO sent by a non-authorised user, it should fail            
            // Prepare PO
            uint poNumber = GetRandomInt();
            string approverAddress = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610";
            uint quoteId = GetRandomInt();
            Po poExpected = CreatePoForPoStorageContract(poNumber, approverAddress, quoteId);

            // Store PO using preexisting PO storage service contract, but with tx executed by the non-authorised ("secondary") user
            var pss = new PoStorageService(_contracts.Web3SecondaryUser, _contracts.Deployment.PoStorageServiceLocal.ContractHandler.ContractAddress);
            Func<Task> act = async () => await pss.SetPoRequestAndWaitForReceiptAsync(poExpected);
            await act.Should().ThrowAsync<SmartContractRevertException>().WithMessage(AUTH_EXCEPTION_ONLY_REGISTERED);
        }
    }
}
