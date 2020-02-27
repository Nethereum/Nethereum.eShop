using Nethereum.eShop.ApplicationCore.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Nethereum.eShop.ApplicationCore.Specifications;
using System.Linq;
using Ardalis.GuardClauses;
using Nethereum.eShop.ApplicationCore.Entities.BasketAggregate;

namespace Nethereum.eShop.ApplicationCore.Services
{
    public class BasketService : IBasketService
    {
        private readonly IAsyncRepository<Basket> _basketRepository;
        private readonly IAppLogger<BasketService> _logger;

        public BasketService(IAsyncRepository<Basket> basketRepository,
            IAppLogger<BasketService> logger)
        {
            _basketRepository = basketRepository;
            _logger = logger;
        }

        public async Task AddItemToBasket(int basketId, int catalogItemId, decimal price, int quantity = 1)
        {
            var basket = await _basketRepository.GetByIdAsync(basketId);

            basket.AddItem(catalogItemId, price, quantity);

            await _basketRepository.UpdateAsync(basket);
        }

        public async Task DeleteBasketAsync(int basketId)
        {
            var basket = await _basketRepository.GetByIdAsync(basketId);
            await _basketRepository.DeleteAsync(basket);
        }

        public async Task<int> GetBasketItemCountAsync(string userName)
        {
            Guard.Against.NullOrEmpty(userName, nameof(userName));
            var basketSpec = new BasketWithItemsSpecification(userName);
            var basket = (await _basketRepository.ListAsync(basketSpec)).FirstOrDefault();
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
            var basket = await _basketRepository.GetByIdAsync(basketId);
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
            await _basketRepository.UpdateAsync(basket);
        }

        public async Task TransferBasketAsync(string anonymousId, string userName)
        {
            Guard.Against.NullOrEmpty(anonymousId, nameof(anonymousId));
            Guard.Against.NullOrEmpty(userName, nameof(userName));
            var basketSpec = new BasketWithItemsSpecification(anonymousId);
            var basket = (await _basketRepository.ListAsync(basketSpec)).FirstOrDefault();
            if (basket == null) return;
            basket.BuyerId = userName;
            // TODO: populate from buyer entity
            basket.BuyerAddress = "";
            await _basketRepository.UpdateAsync(basket);
        }
    }
}
