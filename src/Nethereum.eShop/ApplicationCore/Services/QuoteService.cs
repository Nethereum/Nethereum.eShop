using Ardalis.GuardClauses;
using Nethereum.eShop.ApplicationCore.Entities;
using Nethereum.eShop.ApplicationCore.Entities.BasketAggregate;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using Nethereum.eShop.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IAsyncRepository<Quote> _quoteRepository;
        private readonly IAsyncRepository<Basket> _basketRepository;
        private readonly IAsyncRepository<CatalogItem> _itemRepository;

        public QuoteService(
            IAsyncRepository<Basket> basketRepository,
            IAsyncRepository<CatalogItem> itemRepository,
            IAsyncRepository<Quote> orderRepository)
        {
            _quoteRepository = orderRepository;
            _basketRepository = basketRepository;
            _itemRepository = itemRepository;
        }

        public async Task CreateQuoteAsync(int basketId)
        {
            // TODO: 
            // Validate
            // CheckStock
            // Check Wonka Rules
            // Invoke Po Creation on blockchain / metamask
            // Create purchase order
            // reserve stock?

            var basket = await _basketRepository.GetByIdAsync(basketId);
            Guard.Against.NullBasket(basketId, basket);

            var quoteItems = await MapAsync(basket);

            var quote = new Quote(quoteItems)
            {
                BuyerId = basket.BuyerAddress,
                BuyerAddress = basket.BuyerAddress,
                BillTo = basket.BillTo,
                ShipTo = basket.ShipTo
            };

            await _quoteRepository.AddAsync(quote);
        }

        private async Task<IEnumerable<QuoteItem>> MapAsync(Basket basket)
        {
            var items = new List<QuoteItem>();
            foreach (var item in basket.Items)
            {
                var catalogItem = await _itemRepository.GetByIdAsync(item.CatalogItemId);

                var itemOrdered = new CatalogItemExcerpt(
                    catalogItem.Id, catalogItem.Gtin, catalogItem.GtinRegistryId, catalogItem.Name, catalogItem.PictureUri);

                var quoteItem = new QuoteItem(itemOrdered, item.UnitPrice, item.Quantity);
                items.Add(quoteItem);
            }
            return items;
        }
    }
}
