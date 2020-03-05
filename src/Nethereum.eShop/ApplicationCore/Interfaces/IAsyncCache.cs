using Nethereum.eShop.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IAsyncCache<T> where T : BaseEntity
    {
        Task<bool> ContainsAsync(int id);

        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> ListAllAsync();

        Task<T> AddAsync(T entity);

        Task DeleteAsync(T entity);
    }
}

