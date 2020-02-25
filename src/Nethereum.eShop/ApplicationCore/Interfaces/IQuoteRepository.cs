using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IQuoteRepository : IAsyncRepository<Quote>
    {
        Task<Quote> GetByIdWithItemsAsync(int id);
    }
}
