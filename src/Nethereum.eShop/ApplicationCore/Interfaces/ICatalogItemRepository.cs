using Nethereum.eShop.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface ICatalogItemRepository : IAsyncRepository<CatalogItem>
    {
    }
}
