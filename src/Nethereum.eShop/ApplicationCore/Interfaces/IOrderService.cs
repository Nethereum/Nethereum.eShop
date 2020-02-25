using System.Threading.Tasks;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(string transactionHash, Po purchaseOrder);
    }
}
