using Ardalis.GuardClauses;
using Nethereum.eShop.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IAppLogger<BasketService> _logger;

        public BasketService(IBasketRepository basketRepository, IAppLogger<BasketService> logger)
        {
            _basketRepository = basketRepository ?? throw new System.ArgumentNullException(nameof(basketRepository));
            _logger = logger;
        }

        public async Task AddItemToBasket(int basketId, int catalogItemId, decimal price, int quantity = 1)
        {
            var basket = await _basketRepository.GetByIdWithItemsAsync(basketId).ConfigureAwait(false);
            basket.AddItem(catalogItemId, price, quantity);
            await _basketRepository.UnitOfWork.SaveEntitiesAsync().ConfigureAwait(false);
        }

        public async Task DeleteBasketAsync(int basketId)
        {
            _basketRepository.Delete(basketId);
            await _basketRepository.UnitOfWork.SaveEntitiesAsync().ConfigureAwait(false);
        }

        public async Task<int> GetBasketItemCountAsync(string userName)
        {
            Guard.Against.NullOrEmpty(userName, nameof(userName));
            var basket = await _basketRepository.GetByBuyerIdWithItemsAsync(userName).ConfigureAwait(false);
            if (basket == null)
            {
                _logger.LogInformation($"No basket found for {userName}");
                return 0;
            }
            int count = basket.Items.Sum(i => i.Quantity);
            _logger.LogInformation($"Basket for {userName} has {count} items.");
            return count;
        }

        public async Task SetQuantities(int basketId, Dictionary<string, int> quantities)
        {
            Guard.Against.Null(quantities, nameof(quantities));
            var basket = await _basketRepository.GetByIdWithItemsAsync(basketId).ConfigureAwait(false);
            Guard.Against.NullBasket(basketId, basket);
            foreach (var item in basket.Items)
            {
                if (quantities.TryGetValue(item.Id.ToString(), out var quantity))
                {
                    if(_logger != null) _logger.LogInformation($"Updating quantity of item ID:{item.Id} to {quantity}.");
                    item.Quantity = quantity;
                }
            }
            basket.RemoveEmptyItems();
            await _basketRepository.UnitOfWork.SaveEntitiesAsync().ConfigureAwait(false);
        }

        public async Task TransferBasketAsync(string anonymousId, string userName)
        {
            Guard.Against.NullOrEmpty(anonymousId, nameof(anonymousId));
            Guard.Against.NullOrEmpty(userName, nameof(userName));
            var basket = await _basketRepository.GetByBuyerIdWithItemsAsync(anonymousId).ConfigureAwait(false);
            if (basket == null) return;
            basket.BuyerId = userName;
            // TODO: populate from buyer entity
            basket.BuyerAddress = "";
            await _basketRepository.UnitOfWork.SaveEntitiesAsync().ConfigureAwait(false);
        }
    }
}
