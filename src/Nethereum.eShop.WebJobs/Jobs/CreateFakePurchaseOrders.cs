using Microsoft.Extensions.Logging;
using Nethereum.Commerce.Contracts;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Nethereum.Commerce.Contracts.WalletBuyer;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.ApplicationCore.Specifications;
using Nethereum.eShop.WebJobs.Configuration;
using Nethereum.Web3.Accounts;
using Nethereum.Contracts.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using System.Threading.Tasks;
using static Nethereum.Commerce.Contracts.ContractEnums;
using Storage = Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;
using Nethereum.Contracts;
using System.Linq;

namespace Nethereum.eShop.WebJobs.Jobs
{
    public class CreateFakePurchaseOrders : ICreateFakePurchaseOrders
    {
        private readonly EshopConfiguration _config;
        private readonly IQuoteRepository _quoteRepository;

        public CreateFakePurchaseOrders(EshopConfiguration config, IQuoteRepository quoteRepository)
        {
            this._config = config;
            this._quoteRepository = quoteRepository;
        }

        public class GetPendingQuotesSpec : BaseSpecification<Quote>
        {
            public GetPendingQuotesSpec()
                : base(b => b.Status != QuoteStatus.Complete)
            {
                AddInclude(b => b.QuoteItems);
            }
        }

        public async Task ExecuteAsync(ILogger logger)
        {
            if (!_config.CreateFakePurchaseOrders) return;

            var pendingQuotes = await _quoteRepository.ListAsync(new GetPendingQuotesSpec());

            var account = new Account(_config.AccountPrivateKey);
            var web3 = new Web3.Web3(account);
            var walletBuyerService = new WalletBuyerService(web3, _config.BuyerWalletAddress);

            foreach (var quote in pendingQuotes)
            {
                var existing = await walletBuyerService.GetPoBySellerAndQuoteQueryAsync(_config.SellerId, quote.Id);
                if (existing?.Po?.PoNumber > 0) continue;

                //var quoteId = (uint)DateTime.Now.Subtract(new DateTime(2020, 1, 1)).TotalSeconds;
                var po = CreateDummyPoForPurchasingCreate(quote).ToBuyerPo();
                var receipt = await walletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(po);


                var createdEvent = receipt.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();

                logger.LogInformation($"Created PO from quote.  Block Number: {receipt.BlockNumber}, PO: {createdEvent?.Event.PoNumber}");
            }
        }

        public Storage.Po CreateDummyPoForPurchasingCreate(Quote quote)
        {
            var po = new Storage.Po()
            {
                // PoNumber assigned by contract
                BuyerAddress = "0x37ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                ReceiverAddress = "0x36ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                BuyerWalletAddress = "0x39ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                CurrencySymbol = "DAI",
                CurrencyAddress = "0x41ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                QuoteId = quote.Id,
                QuoteExpiryDate = DateTimeOffset.UtcNow.AddMonths(1).ToUnixTimeSeconds(),
                ApproverAddress = string.Empty,  // assigned by contract
                PoType = PoType.Cash,
                SellerId = _config.SellerId,
                // PoCreateDate assigned by contract
                // PoItemCount assigned by contract
                PoItems = new List<Storage.PoItem>()
            };

            //gtin1111

            foreach (var quoteItem in quote.QuoteItems)
            {
                po.PoItems.Add(new Storage.PoItem
                {
                    // PoNumber assigned by contract
                    // PoItemNumber assigned by contract
                    SoNumber = string.Empty,
                    SoItemNumber = string.Empty,
                    ProductId = quoteItem.ItemOrdered.Gtin,
                    Quantity = quoteItem.Quantity,
                    Unit = "EA",
                    QuantitySymbol = "NA",
                    QuantityAddress = "0x40ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                    CurrencyValue = quoteItem.CurrencyValue == null ? 0 : BigInteger.Parse(quoteItem.CurrencyValue),
                    // Status assigned by contract
                    // GoodsIssuedDate assigned by contract
                    // GoodsReceivedDate assigned by contract
                    // PlannedEscrowReleaseDate assigned by contract
                    // ActualEscrowReleaseDate assigned by contract
                    // IsEscrowReleased assigned by contract
                    // CancelStatus assigned by contract
                });
            }

            return po;
        }
    }
}
