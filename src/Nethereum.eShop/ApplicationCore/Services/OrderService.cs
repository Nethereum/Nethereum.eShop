using Ardalis.GuardClauses;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Nethereum.eShop.ApplicationCore.Entities.OrderAggregate;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Services
{
    public class OrderService : IOrderService
    {
        private readonly CatalogContext _dbContext;

        public OrderService(CatalogContext catalogContext)
        {
            _dbContext = catalogContext;
        }

        public async Task CreateOrderAsync(string transactionHash, Po purchaseOrder)
        {
            // TODO: write purchase order values to order
            // TODO: Ensure po values are consistent with quote

            int quoteId = (int)purchaseOrder.QuoteId;

            var quote = await _dbContext.GetQuoteWithItemsOrDefault(quoteId);

            Guard.Against.NullQuote(quoteId, quote);
            var items = new List<OrderItem>();
            foreach (var item in quote.QuoteItems)
            {
                var orderItem = new OrderItem(item.ItemOrdered, item.UnitPrice, item.Quantity);
                items.Add(orderItem);
            }
            var order = new Order(quote.BuyerId, purchaseOrder.BuyerAddress, quote.BillTo, quote.ShipTo, items);
            order.QuoteId = quote.Id;
            order.PoDate = DateTimeOffset.FromUnixTimeSeconds((long)purchaseOrder.PoCreateDate);
            order.PoType = (int)purchaseOrder.PoType;
            order.ApproverAddress = purchaseOrder.ApproverAddress;

            // TODO: define if we should have BuyerId and BuyerAddress
            // the po will have the address, the quote will have the buyer
            order.BuyerId = quote.BuyerId;
            order.BuyerAddress = purchaseOrder.BuyerAddress;

            order.BuyerWalletAddress = purchaseOrder.BuyerWalletAddress;
            order.CurrencyAddress = purchaseOrder.CurrencyAddress;
            order.CurrencySymbol = purchaseOrder.CurrencySymbol;
            order.PoNumber = (long)purchaseOrder.PoNumber;
            order.Status = OrderStatus.Pending;
            order.TransactionHash = transactionHash;

            foreach(var poItem in purchaseOrder.PoItems)
            {
                var orderItem = quote.QuoteItems.ElementAtOrDefault((int)poItem.PoItemNumber - 1);
                if (orderItem == null) continue;

                orderItem.PoItemNumber = (int)poItem.PoItemNumber;
                orderItem.QuantityAddress = poItem.QuantityAddress;
                orderItem.QuantitySymbol = poItem.QuantitySymbol;
                orderItem.Unit = poItem.Unit;
                // potentially large number so stored in sql as string
                orderItem.CurrencyValue = poItem.CurrencyValue.ToString();
                orderItem.EscrowReleaseDate = DateTimeOffset.FromUnixTimeSeconds((long)poItem.PlannedEscrowReleaseDate);
            }

            quote.TransactionHash = transactionHash;
            quote.PoNumber = (long)purchaseOrder.PoNumber;
            quote.Status = QuoteStatus.Complete;

            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync();
        }
    }
}
