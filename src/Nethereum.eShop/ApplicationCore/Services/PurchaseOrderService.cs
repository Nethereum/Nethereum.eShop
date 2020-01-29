using Ardalis.GuardClauses;
using Nethereum.eShop.ApplicationCore.Entities;
using Nethereum.eShop.ApplicationCore.Entities.BasketAggregate;
using Nethereum.eShop.ApplicationCore.Entities.OrderAggregate;
using Nethereum.eShop.ApplicationCore.Interfaces;
using System;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IAsyncRepository<Basket> _basketRepository;
        private readonly IAsyncRepository<CatalogItem> _itemRepository;

        public PurchaseOrderService(
            IAsyncRepository<Basket> basketRepository,
            IAsyncRepository<CatalogItem> itemRepository)
        {
            _basketRepository = basketRepository;
            _itemRepository = itemRepository;
        }

        public async Task CreateOrderAsync(int basketId, PostalAddress billingAddress, PostalAddress shippingAddress)
        {
            // TODO: 
            // write address to buyer, to be looked up on po creation
            // Create purchase order on chain
            // using basket to populate
            // we need the buyer nonce

            var basket = await _basketRepository.GetByIdAsync(basketId);
            Guard.Against.NullBasket(basketId, basket);

            throw new NotImplementedException();
            //var items = new List<OrderItem>();
            //foreach (var item in basket.Items)
            //{


            //    var catalogItem = await _itemRepository.GetByIdAsync(item.CatalogItemId);
            //    var itemOrdered = new CatalogItemOrdered(catalogItem.Id, catalogItem.Name, catalogItem.PictureUri);
            //    var orderItem = new OrderItem(itemOrdered, item.UnitPrice, item.Quantity);
            //    items.Add(orderItem);
            //}
            //var order = new Order(basket.BuyerId, billingAddress, shippingAddress, items);

            //await _orderRepository.AddAsync(order);
        }
    }
}
