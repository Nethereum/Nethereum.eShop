using Ardalis.GuardClauses;
using Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;
using Nethereum.eShop.ApplicationCore.Entities;
using Nethereum.eShop.ApplicationCore.Entities.OrderAggregate;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using Nethereum.eShop.ApplicationCore.Interfaces;
using System.Collections.Generic;
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

            int quoteId = (int)purchaseOrder.QuoteId;

            var quote = await _quoteRepository.GetByIdAsync(quoteId);
            Guard.Against.NullQuote(quoteId, quote);
            var items = new List<OrderItem>();
            foreach (var item in quote.QuoteItems)
            {
                var orderItem = new OrderItem(item.ItemOrdered, item.UnitPrice, item.Quantity);
                items.Add(orderItem);
            }
            var order = new Order(quote.BuyerAddress, quote.BillTo, quote.ShipTo, items);

            await _orderRepository.AddAsync(order);
        }
    }
}
