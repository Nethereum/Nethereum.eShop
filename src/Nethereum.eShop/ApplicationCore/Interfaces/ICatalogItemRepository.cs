using Nethereum.eShop.ApplicationCore.Entities;
using Nethereum.eShop.Infrastructure.Data;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface ICatalogItemRepository : IAsyncRepository<CatalogItem>, IRepository
    {

    }
}
