using Nethereum.eShop.ApplicationCore.Entities;
using Nethereum.eShop.ApplicationCore.Entities.OrderAggregate;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IPurchaseOrderService
    {
        Task CreateOrderAsync(int basketId, PostalAddress billingAddress, PostalAddress shippingAddress);
    }
}
