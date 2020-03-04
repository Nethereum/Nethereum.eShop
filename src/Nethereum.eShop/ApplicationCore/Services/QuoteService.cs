using Ardalis.GuardClauses;
using Nethereum.eShop.ApplicationCore.Entities;
using Nethereum.eShop.ApplicationCore.Entities.BasketAggregate;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using Nethereum.eShop.ApplicationCore.Exceptions;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly CatalogContext _dbContext;
        private readonly IRulesEngineService _rulesEngineService;

        public QuoteService(
            CatalogContext catalogContext,
            IRulesEngineService rulesEngineService = null)
        {
            _dbContext = catalogContext;
            _rulesEngineService = rulesEngineService;
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

            var basket = await _dbContext.GetBasketWithItemsOrDefault(basketId).ConfigureAwait(false);
            Guard.Against.NullBasket(basketId, basket);

            var quoteItems = await MapAsync(basket);

            var quote = new Quote(quoteItems)
            {
                Status = QuoteStatus.Pending,
                BuyerId = basket.BuyerId,
                BuyerAddress = basket.BuyerAddress,
                BillTo = basket.BillTo,
                ShipTo = basket.ShipTo,
                Date = DateTimeOffset.UtcNow,
                Expiry = DateTimeOffset.UtcNow.Date.AddMonths(1)
            };

            // NOTE: This block demonstrates the basic idea of how to use the rules engine, 
            // but it's definitely subject to change
            if ((_rulesEngineService != null) && (_rulesEngineService.GetDefaultRuleTree() != null))
            {
                var QuoteRuleTree = await _rulesEngineService.GetDefaultRuleTree().ConfigureAwait(false);
                var QuoteRecord   = await _rulesEngineService.Transform(quote).ConfigureAwait(false);
                var QuoteReport   = await _rulesEngineService.ExecuteAsync(QuoteRuleTree, QuoteRecord).ConfigureAwait(false);

                if (QuoteReport.NumberOfFailures > 0)
                {
                    throw new RuleTreeException(QuoteRuleTree.TreeOrigin, QuoteReport);
                }
            }

            _dbContext.Quotes.Add(quote);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task<IEnumerable<QuoteItem>> MapAsync(Basket basket)
        {
            var items = new List<QuoteItem>();
            foreach (var item in basket.Items)
            {
                var catalogItem = await _dbContext.CatalogItems.FindAsync(item.CatalogItemId).ConfigureAwait(false);

                var itemOrdered = new CatalogItemExcerpt(
                    catalogItem.Id, catalogItem.Gtin, catalogItem.GtinRegistryId, catalogItem.Name, catalogItem.PictureUri);

                var quoteItem = new QuoteItem(itemOrdered, item.UnitPrice, item.Quantity);
                items.Add(quoteItem);
            }
            return items;
        }
    }
}
