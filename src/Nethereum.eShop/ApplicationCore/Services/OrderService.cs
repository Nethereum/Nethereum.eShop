using Ardalis.GuardClauses;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Nethereum.eShop.ApplicationCore.Entities;
using Nethereum.eShop.ApplicationCore.Entities.OrderAggregate;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IAsyncRepository<Order> _orderRepository;
        private readonly IAsyncRepository<Quote> _quoteRepository;
        private readonly IAsyncRepository<CatalogItem> _itemRepository;

        public OrderService(IAsyncRepository<Quote> quoteRepository,
            IAsyncRepository<CatalogItem> itemRepository,
            IAsyncRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
            _quoteRepository = quoteRepository;
            _itemRepository = itemRepository;
        }

        public async Task CreateOrderAsync(Po purchaseOrder)
        {
            // TODO: write purchase order values to order
            // TODO: Ensure po values are consistent with quote

            int quoteId = (int)purchaseOrder.QuoteId;

            var spec = new QuoteWithItemsSpecification(quoteId);

            var quotes = await _quoteRepository.ListAsync(spec);
            var quote = quotes.FirstOrDefault();

            Guard.Against.NullQuote(quoteId, quote);
            var items = new List<OrderItem>();
            foreach (var item in quote.QuoteItems)
            {
                var orderItem = new OrderItem(item.ItemOrdered, item.UnitPrice, item.Quantity);
                items.Add(orderItem);
            }
            var order = new Order(quote.BuyerAddress, quote.BillTo, quote.ShipTo, items);
            order.QuoteId = quote.Id;
            order.PoDate = DateTimeOffset.FromUnixTimeSeconds((long)purchaseOrder.PoCreateDate);
            order.PoType = (int)purchaseOrder.PoType;
            order.ApproverAddress = purchaseOrder.ApproverAddress;
            order.BuyerAddress = purchaseOrder.BuyerAddress;
            order.BuyerWalletAddress = purchaseOrder.BuyerWalletAddress;
            order.CurrencyAddress = purchaseOrder.CurrencyAddress;
            order.CurrencySymbol = purchaseOrder.CurrencySymbol;
            order.PoNumber = (long)purchaseOrder.PoNumber;
            order.Status = OrderStatus.Pending;

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

            await _orderRepository.AddAsync(order);
            quote.PoNumber = (long)purchaseOrder.PoNumber;
            quote.Status = QuoteStatus.Complete;
            await _quoteRepository.UpdateAsync(quote);
        }
    }
}
