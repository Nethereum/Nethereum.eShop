using Nethereum.eShop.ApplicationCore.Entities.BasketAggregate;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IBasketRepository : IRepository
    {
        Basket Add(Basket basket);
        Basket Update(Basket basket);
        void Delete(Basket basket);
        void Delete(int basketId);
        Task<Basket> GetByIdWithItemsAsync(int id);

        Task<Basket> GetByBuyerIdWithItemsAsync(string userName);
    }
}
