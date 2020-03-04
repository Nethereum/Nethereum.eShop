using Ardalis.GuardClauses;
using Nethereum.eShop.ApplicationCore.Entities;
using Nethereum.eShop.ApplicationCore.Entities.BasketAggregate;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.Infrastructure.Data;
using System;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly CatalogContext _dbContext;

        public PurchaseOrderService(CatalogContext catalogContext)
        {
            _dbContext = catalogContext;
        }

        public async Task CreateOrderAsync(int basketId, PostalAddress billingAddress, PostalAddress shippingAddress)
        {
            // TODO: 
            // write address to buyer, to be looked up on po creation
            // Create purchase order on chain
            // using basket to populate
            // we need the buyer nonce

            var basket = await _dbContext.GetBasketWithItemsOrDefault(basketId).ConfigureAwait(false);
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
