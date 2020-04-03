﻿using Ardalis.GuardClauses;
using Nethereum.eShop.ApplicationCore.Entities;
using Nethereum.eShop.ApplicationCore.Entities.BasketAggregate;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using Nethereum.eShop.ApplicationCore.Entities.RulesEngine;
using Nethereum.eShop.ApplicationCore.Exceptions;
using Nethereum.eShop.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ICatalogItemRepository _itemRepository;
        private readonly IQuoteRepository _quoteRepository;
        private readonly IRulesEngineService _rulesEngineService;

        public QuoteService(
            IBasketRepository basketRepository,
            ICatalogItemRepository itemRepository,
            IQuoteRepository quoteRepository,
            IRulesEngineService rulesEngineService)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
            _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
            _rulesEngineService = rulesEngineService ?? throw new ArgumentNullException(nameof(rulesEngineService));
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

            var basket = await _basketRepository.GetByIdWithItemsAsync(basketId).ConfigureAwait(false);
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

            try
            {
                var ReportsWithWarnings = await ExecuteRules(quote).ConfigureAwait(false);
                if (ReportsWithWarnings.Count > 0)
                {
                    // NOTE: Should there be any indication of minor issues to the user?
                }
            }
            catch (RuleTreeException)
            {
                // NOTE: Should we redirect the user to an error page?
            }

            _quoteRepository.Add(quote);

            await _quoteRepository.UnitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task<List<RuleTreeReport>> ExecuteRules(Quote targetQuote)
        {
            var ReportsWithWarnings = new List<RuleTreeReport>();

            // NOTE: This block demonstrates the basic idea of how to use the rules engine, 
            // but it's definitely subject to change
            if (_rulesEngineService != null)
            {
                var QuoteRuleTree     = await _rulesEngineService.GetQuoteRuleTree().ConfigureAwait(false);
                var QuoteItemRuleTree = await _rulesEngineService.GetQuoteItemRuleTree().ConfigureAwait(false);

                var QuoteRecord = await _rulesEngineService.Transform(targetQuote).ConfigureAwait(false);
                var QuoteReport = await _rulesEngineService.ExecuteAsync(QuoteRuleTree, QuoteRecord).ConfigureAwait(false);

                if (QuoteReport.NumberOfFailures > 0)
                {
                    throw new RuleTreeException(QuoteRuleTree.TreeOrigin, QuoteReport);
                }
                else if ((QuoteReport.RuleSetsWithWarnings != null) && (QuoteReport.RuleSetsWithWarnings.Count > 0))
                {
                    ReportsWithWarnings.Add(QuoteReport);
                }

                if (QuoteItemRuleTree != null)
                {
                    foreach (var TmpQuoteItem in targetQuote.QuoteItems)
                    {
                        var QuoteItemRecord = await _rulesEngineService.Transform(TmpQuoteItem).ConfigureAwait(false);
                        var QuoteItemReport = await _rulesEngineService.ExecuteAsync(QuoteItemRuleTree, QuoteItemRecord).ConfigureAwait(false);

                        if (QuoteItemReport.NumberOfFailures > 0)
                        {
                            throw new RuleTreeException(QuoteItemRuleTree.TreeOrigin, QuoteItemReport);
                        }
                        else if ((QuoteItemReport.RuleSetsWithWarnings != null) && (QuoteItemReport.RuleSetsWithWarnings.Count > 0))
                        {
                            ReportsWithWarnings.Add(QuoteItemReport);
                        }
                    }
                }
            }

            return ReportsWithWarnings;
        }

        private async Task<IEnumerable<QuoteItem>> MapAsync(Basket basket)
        {
            var items = new List<QuoteItem>();
            foreach (var item in basket.Items)
            {
                var catalogItem = await _itemRepository.GetByIdAsync(item.CatalogItemId).ConfigureAwait(false);

                var itemOrdered = new CatalogItemExcerpt(
                    catalogItem.Id, catalogItem.Gtin, catalogItem.GtinRegistryId, catalogItem.Name, catalogItem.PictureUri);

                var quoteItem = new QuoteItem(itemOrdered, item.UnitPrice, item.Quantity);
                items.Add(quoteItem);
            }
            return items;
        }
    }
}
