using Ardalis.GuardClauses;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Nethereum.eShop.ApplicationCore.Entities.OrderAggregate;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using Nethereum.eShop.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IQuoteRepository _quoteRepository;

        public OrderService(IOrderRepository orderRepository, IQuoteRepository quoteRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
        }

        public async Task CreateOrderAsync(string transactionHash, Po purchaseOrder)
        {
            // TODO: write purchase order values to order
            // TODO: Ensure po values are consistent with quote
            int quoteId = (int)purchaseOrder.QuoteId;
            var quote = await _quoteRepository.GetByIdWithItemsAsync(quoteId).ConfigureAwait(false);
            Guard.Against.NullQuote(quoteId, quote);

            List<OrderItem> orderItems = MapQuoteItemsToOrderItems(quote);
            Order order = MapQuoteToOrder(transactionHash, purchaseOrder, quote, orderItems);

            quote.SetConvertedToOrder(order.Id, (long)purchaseOrder.PoNumber, transactionHash);

            _orderRepository.Add(order);
            await _orderRepository.UnitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }

        private static List<OrderItem> MapQuoteItemsToOrderItems(Quote quote)
        {
            var items = new List<OrderItem>();
            foreach (var item in quote.QuoteItems)
            {
                var orderItem = new OrderItem(item.ItemOrdered, item.UnitPrice, item.Quantity);
                items.Add(orderItem);
            }

            return items;
        }

        private static Order MapQuoteToOrder(string transactionHash, Po purchaseOrder, Quote quote, List<OrderItem> items)
        {
            var order = new Order(quote.BuyerId, purchaseOrder.BuyerWalletAddress, quote.BillTo, quote.ShipTo, items);
            order.QuoteId = quote.Id;
            order.PoDate = DateTimeOffset.FromUnixTimeSeconds((long)purchaseOrder.PoCreateDate);
            order.PoType = (int)purchaseOrder.PoType;

            // TODO: define if we should have BuyerId and BuyerAddress
            // the po will have the address, the quote will have the buyer
            order.BuyerId = quote.BuyerId;

            order.BuyerWalletAddress = purchaseOrder.BuyerWalletAddress;
            order.CurrencyAddress = purchaseOrder.CurrencyAddress;
            order.CurrencySymbol = purchaseOrder.CurrencySymbol;
            order.PoNumber = (long)purchaseOrder.PoNumber;
            order.Status = OrderStatus.Pending;
            order.TransactionHash = transactionHash;

            foreach (var poItem in purchaseOrder.PoItems)
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

            return order;
        }
    }
}
