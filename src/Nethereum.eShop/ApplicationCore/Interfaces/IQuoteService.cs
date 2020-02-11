using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IQuoteService
    {
        Task CreateQuoteAsync(int basketId);
    }
}
