using FluentAssertions;
using Nethereum.Commerce.Contracts;
using Nethereum.StandardTokenEIP20;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Threading.Tasks;
using Xunit.Abstractions;
using static Nethereum.Commerce.Contracts.ContractEnums;
using Buyer = Nethereum.Commerce.Contracts.WalletBuyer.ContractDefinition;
using Seller = Nethereum.Commerce.Contracts.WalletSeller.ContractDefinition;
using Storage = Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    public static class PoTestHelpers
    {
        /// <summary>
        /// Revert message for the Solidity onlyRegisteredCaller() function modifier
        /// </summary>
        public const string AUTH_EXCEPTION_ONLY_REGISTERED = "*Only contract owner or a bound address may call this function*";

        /// <summary>
        /// Revert message for the Solidity onlyOwner() function modifier
        /// </summary>
        public const string AUTH_EXCEPTION_ONLY_OWNER = "*Ownable: caller is not the owner*";

        /// <summary>
        /// Revert message during PO creation, for when a PO + signature does not resolve to the expected signer address held
        /// in BusinessPartnerStorage.sol master data
        /// </summary>
        public const string SIGNER_EXCEPTION_WRONG_SIGNER = "*Signature for quote does not match expected signature*";

        
        private static Random _random;

        static PoTestHelpers()
        {
            _random = new Random();
        }

        public static string GetRandomString()
        {
            return ((uint)_random.Next(int.MinValue, int.MaxValue)).ToString(CultureInfo.InvariantCulture);
        }

        public static uint GetRandomInt()
        {
            return ((uint)_random.Next(int.MinValue, int.MaxValue));
        }

        public static void DisplayPoHeader(ITestOutputHelper output, Storage.Po po)
        {
            output.WriteLine("");
            if (po == null)
            {
                output.WriteLine($"No PO to display.");
            }
            else
            {
                output.WriteLine($"PO number          : {po.PoNumber}");
                output.WriteLine($"BuyerAddress       : {po.BuyerAddress}");
                output.WriteLine($"ReceiverAddress    : {po.ReceiverAddress}");
                output.WriteLine($"BuyerWalletAddress : {po.BuyerWalletAddress}");
                output.WriteLine($"CurrencySymbol     : {po.CurrencySymbol}");
                output.WriteLine($"CurrencyAddress    : {po.CurrencyAddress}");
                output.WriteLine($"QuoteId            : {po.QuoteId}");
                output.WriteLine($"QuoteExpiryDate    : {po.QuoteExpiryDate}");
                output.WriteLine($"ApproverAddress    : {po.ApproverAddress}");
                output.WriteLine($"PoType             : {po.PoType}");
                output.WriteLine($"SellerId           : {po.SellerId}");
                output.WriteLine($"PoCreateDate       : {po.PoCreateDate}");
                output.WriteLine($"PoItemCount        : {po.PoItemCount}");
                output.WriteLine($"PoRulesCount       : {po.RulesCount}");
            }
        }

        public static void DisplayPoItem(ITestOutputHelper output, Storage.PoItem poItem)
        {
            output.WriteLine("");
            if (poItem == null)
            {
                output.WriteLine($"No PO item to display.");
            }
            else
            {
                output.WriteLine($"PO number                : {poItem.PoNumber}");
                output.WriteLine($"PoItemNumber             : {poItem.PoItemNumber}");
                output.WriteLine($"SoNumber                 : {poItem.SoNumber}");
                output.WriteLine($"SoItemNumber             : {poItem.SoItemNumber}");
                output.WriteLine($"ProductId                : {poItem.ProductId}");
                output.WriteLine($"Quantity                 : {poItem.Quantity}");
                output.WriteLine($"Unit                     : {poItem.Unit}");
                output.WriteLine($"QuantitySymbol           : {poItem.QuantitySymbol}");
                output.WriteLine($"QuantityAddress          : {poItem.QuantityAddress}");
                output.WriteLine($"CurrencyValue            : {poItem.CurrencyValue}");
                output.WriteLine($"Status                   : {poItem.Status}");
                output.WriteLine($"GoodsIssuedDate          : {poItem.GoodsIssuedDate}");
                output.WriteLine($"GoodsReceivedDate        : {poItem.GoodsReceivedDate}");
                output.WriteLine($"PlannedEscrowReleaseDate : {poItem.PlannedEscrowReleaseDate}");
                output.WriteLine($"ActualEscrowReleaseDate  : {poItem.ActualEscrowReleaseDate}");
                output.WriteLine($"IsEscrowReleased         : {poItem.IsEscrowReleased}");
                output.WriteLine($"CancelStatus             : {poItem.CancelStatus}");

            }
        }

        public static void DisplaySeparator(ITestOutputHelper output, string s)
        {
            output.WriteLine("");
            output.WriteLine($"------------------------ {s} -----------------------");
        }

        /// <summary>
        /// Compare a requested PO with an as-built version of the same PO.
        /// Optionally also check the approver address and creation date.
        /// </summary>
        public static void CheckCreatedPoFieldsMatch(Storage.Po poAsRequested, Storage.Po poAsBuilt,
        BigInteger poNumberAsBuilt, string approverAddressAsBuilt = null, BigInteger? poCreateDateAsBuilt = null)
        {
            poAsBuilt.PoNumber.Should().Be(poNumberAsBuilt);
            poAsBuilt.BuyerAddress.Should().Be(poAsRequested.BuyerAddress);
            poAsBuilt.ReceiverAddress.Should().Be(poAsRequested.ReceiverAddress);
            poAsBuilt.BuyerWalletAddress.Should().Be(poAsRequested.BuyerWalletAddress);
            poAsBuilt.CurrencySymbol.Should().Be(poAsRequested.CurrencySymbol);
            poAsBuilt.CurrencyAddress.Should().Be(poAsRequested.CurrencyAddress);
            poAsBuilt.QuoteId.Should().Be(poAsRequested.QuoteId);
            poAsBuilt.QuoteExpiryDate.Should().Be(poAsRequested.QuoteExpiryDate);
            if (approverAddressAsBuilt is string approverAddressAsBuiltValue)
            {
                poAsBuilt.ApproverAddress.Should().Be(approverAddressAsBuiltValue);
            }
            poAsBuilt.PoType.Should().Be(poAsRequested.PoType);
            poAsBuilt.SellerId.Should().Be(poAsRequested.SellerId);
            if (poCreateDateAsBuilt is BigInteger poCreateDateAsBuiltValue)
            {
                poAsBuilt.PoCreateDate.Should().Be(poCreateDateAsBuiltValue);
            }
            poAsBuilt.PoItemCount.Should().Be((uint)poAsRequested.PoItems.Count);
            for (int i = 0; i < poAsBuilt.PoItemCount; i++)
            {
                poAsBuilt.PoItems[i].PoNumber.Should().Be(poNumberAsBuilt);
                poAsBuilt.PoItems[i].PoItemNumber.Should().Be((uint)(i + 1));
                poAsBuilt.PoItems[i].SoNumber.Should().Be(poAsRequested.PoItems[i].SoNumber);
                poAsBuilt.PoItems[i].SoItemNumber.Should().Be(poAsRequested.PoItems[i].SoItemNumber);
                poAsBuilt.PoItems[i].ProductId.Should().Be(poAsRequested.PoItems[i].ProductId);
                poAsBuilt.PoItems[i].Quantity.Should().Be(poAsRequested.PoItems[i].Quantity);
                poAsBuilt.PoItems[i].Unit.Should().Be(poAsRequested.PoItems[i].Unit);
                poAsBuilt.PoItems[i].QuantitySymbol.Should().Be(poAsRequested.PoItems[i].QuantitySymbol);
                poAsBuilt.PoItems[i].QuantityAddress.Should().Be(poAsRequested.PoItems[i].QuantityAddress);
                poAsBuilt.PoItems[i].CurrencyValue.Should().Be(poAsRequested.PoItems[i].CurrencyValue);
                poAsBuilt.PoItems[i].Status.Should().Be(PoItemStatus.Created);
                poAsBuilt.PoItems[i].GoodsIssuedDate.Should().Be(0);
                poAsBuilt.PoItems[i].GoodsReceivedDate.Should().Be(0);
                poAsBuilt.PoItems[i].PlannedEscrowReleaseDate.Should().Be(0);
                poAsBuilt.PoItems[i].ActualEscrowReleaseDate.Should().Be(0);
                poAsBuilt.PoItems[i].IsEscrowReleased.Should().Be(false);
                poAsBuilt.PoItems[i].CancelStatus.Should().Be(PoItemCancelStatus.Initial);
            }
        }

        /// <summary>
        /// Compare every field of two POs
        /// </summary>
        public static void CheckEveryPoFieldMatches(Storage.Po poExpected, Storage.Po poActual)
        {
            poActual.PoNumber.Should().Be(poExpected.PoNumber);
            poActual.BuyerAddress.Should().Be(poExpected.BuyerAddress);
            poActual.ReceiverAddress.Should().Be(poExpected.ReceiverAddress);
            poActual.BuyerWalletAddress.Should().Be(poExpected.BuyerWalletAddress);
            poActual.CurrencySymbol.Should().Be(poExpected.CurrencySymbol);
            poActual.CurrencyAddress.Should().Be(poExpected.CurrencyAddress);
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
                poActual.PoItems[i].Status.Should().Be(poExpected.PoItems[i].Status);
                poActual.PoItems[i].GoodsIssuedDate.Should().Be(poExpected.PoItems[i].GoodsIssuedDate);
                poActual.PoItems[i].GoodsReceivedDate.Should().Be(poExpected.PoItems[i].GoodsReceivedDate);
                poActual.PoItems[i].PlannedEscrowReleaseDate.Should().Be(poExpected.PoItems[i].PlannedEscrowReleaseDate);
                poActual.PoItems[i].ActualEscrowReleaseDate.Should().Be(poExpected.PoItems[i].ActualEscrowReleaseDate);
                poActual.PoItems[i].IsEscrowReleased.Should().Be(poExpected.PoItems[i].IsEscrowReleased);
                poActual.PoItems[i].CancelStatus.Should().Be(poExpected.PoItems[i].CancelStatus);
            }
            poActual.RulesCount.Should().Be(poExpected.RulesCount);
            for (int i = 0; i < poActual.RulesCount; i++)
            {
                poActual.Rules[i].Should().BeEquivalentTo(poExpected.Rules[i]);
            }
        }

        /// <summary>
        /// A realistic test PO intended for passing to contracts WalletBuyer.sol or Purchasing.sol poCreate() functions.
        /// </summary>        
        public static Storage.Po CreatePoForPurchasingContracts(
            string buyerAddress,
            string receiverAddress,
            string buyerWalletAddress,
            string currencySymbol,
            string currencyAddress,
            uint quoteId,
            bool isLargeValue = false)
        {
            BigInteger valueLine01 = BigInteger.Parse("110000000000000000000"); // eg this is 110 dai
            BigInteger valueLine02 = BigInteger.Parse("220000000000000000000"); // eg this is 220 dai
            if (isLargeValue)
            {
                valueLine01 *= 1000;
                valueLine02 *= 1000;
            }

            var po = new Storage.Po()
            {
                // PoNumber assigned by contract
                BuyerAddress = buyerAddress,
                ReceiverAddress = receiverAddress,
                BuyerWalletAddress = buyerWalletAddress,
                CurrencySymbol = currencySymbol,
                CurrencyAddress = currencyAddress,
                QuoteId = quoteId,
                QuoteExpiryDate = 1,
                ApproverAddress = string.Empty,  // assigned by contract
                PoType = PoType.Cash,
                SellerId = "Nethereum.eShop",
                // PoCreateDate assigned by contract
                // PoItemCount assigned by contract                
                PoItems = new List<Storage.PoItem>()
                {
                    new Storage.PoItem()
                    {
                        // PoNumber assigned by contract
                        // PoItemNumber assigned by contract
                        SoNumber = string.Empty,
                        SoItemNumber = string.Empty,
                        ProductId = "gtin1111",
                        Quantity = 1,
                        Unit = "EA",
                        QuantitySymbol = "NA",
                        QuantityAddress = "0x40ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                        CurrencyValue = valueLine01
                        // Status assigned by contract
                        // GoodsIssuedDate assigned by contract
                        // GoodsReceivedDate assigned by contract
                        // PlannedEscrowReleaseDate assigned by contract
                        // ActualEscrowReleaseDate assigned by contract
                        // IsEscrowReleased assigned by contract
                        // CancelStatus assigned by contract
                    },
                    new Storage.PoItem()
                    {
                        // PoNumber assigned by contract
                        // PoItemNumber assigned by contract
                        SoNumber = string.Empty,
                        SoItemNumber = string.Empty,
                        ProductId = "gtin2222",
                        Quantity = 2,
                        Unit = "EA",
                        QuantitySymbol = "NA",
                        QuantityAddress = "0x42ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                        CurrencyValue = valueLine02
                        // Status assigned by contract
                        // GoodsIssuedDate assigned by contract
                        // GoodsReceivedDate assigned by contract
                        // PlannedEscrowReleaseDate assigned by contract
                        // ActualEscrowReleaseDate assigned by contract
                        // IsEscrowReleased assigned by contract
                        // CancelStatus assigned by contract
                    }
                },
                // RulesCount assigned by contract
                Rules = new List<byte[]>()
                {
                    "rule01".ConvertToBytes32(),
                    "rule02".ConvertToBytes32(),
                    "rule03".ConvertToBytes32()
                }
            };
            return po;

        }

        /// <summary>
        /// An unrealistic PO intended for writing directly to the PO storage contract PoStorage.sol (ie no validations are done on
        /// this data, it is written direct to storage only).
        /// </summary>
        public static Storage.Po CreatePoForPoStorageContract(uint poNumber, string approverAddress, uint quoteId)
        {
            return new Storage.Po()
            {
                PoNumber = poNumber,
                BuyerAddress = "0x37ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                ReceiverAddress = "0x36ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                BuyerWalletAddress = "0x39ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                CurrencySymbol = "DAI",
                CurrencyAddress = "0x41ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                QuoteId = quoteId,
                QuoteExpiryDate = 1,
                ApproverAddress = approverAddress,
                PoType = PoType.Cash,
                SellerId = "Nethereum.eShop",
                PoCreateDate = 100,
                PoItemCount = 2,
                PoItems = new List<Storage.PoItem>()
                {
                    new Storage.PoItem()
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
                        Status = PoItemStatus.Created,
                        GoodsIssuedDate = 100,
                        GoodsReceivedDate = 0,
                        PlannedEscrowReleaseDate = 100,
                        ActualEscrowReleaseDate = 110,
                        IsEscrowReleased = false,
                        CancelStatus = PoItemCancelStatus.Initial
                    },
                    new Storage.PoItem()
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
                        Status = PoItemStatus.Created,
                        GoodsIssuedDate = 200,
                        GoodsReceivedDate = 0,
                        PlannedEscrowReleaseDate = 200,
                        ActualEscrowReleaseDate = 210,
                        IsEscrowReleased = false,
                        CancelStatus = PoItemCancelStatus.Initial
                    }
                },
                RulesCount = 3,
                Rules = new List<byte[]>()
                {
                    "rule01".ConvertToBytes32(),
                    "rule02".ConvertToBytes32(),
                    "rule03".ConvertToBytes32(),
                }
            };
        }

        public static async Task PrepSendFundsToBuyerWalletForPo(Web3.Web3 fromWeb3, Buyer.Po po)
        {
            // Transfer required funds (tokens) from given Web3 acccount to buyer wallet given on po
            StandardTokenService sts = new StandardTokenService(fromWeb3, po.CurrencyAddress);
            var txTransfer = await sts.TransferRequestAndWaitForReceiptAsync(po.BuyerWalletAddress, po.GetTotalCurrencyValue());
            txTransfer.Status.Value.Should().Be(1);
        }

        /// <summary>
        /// Format a token value according to its decimals and symbol
        /// eg "5000000000000000" becomes "50,000 SYM" 
        /// </summary>
        public static async Task<string> PrettifyAsync(this BigInteger value, StandardTokenService sts)
        {
            var symbol = await sts.SymbolQueryAsync();
            var decimals = await sts.DecimalsQueryAsync();
            var valueFactored = value / BigInteger.Pow(10, decimals);
            return $"{valueFactored.ToString("N0")} {symbol}";
        }
    }
}
