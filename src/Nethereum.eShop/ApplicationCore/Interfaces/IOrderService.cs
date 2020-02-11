using System.Threading.Tasks;
using Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Po purchaseOrder);
    }
}
