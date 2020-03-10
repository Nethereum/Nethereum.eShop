using Nethereum.eShop.ApplicationCore.Entities;
using Nethereum.eShop.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.Infrastructure.Data
{
    /// <summary>
    /// 
    /// This class should be a conduit for accessing a caching layer.  Obviously, the Dictionary<> member will not
    /// perform in a dependency-injection solution.  However, it's used as a placeholder here, one which will be
    /// replaced at some point with calls to a real caching solution.
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GeneralCache<T> : IAsyncCache<T> where T : BaseEntity
    {
        private readonly Dictionary<int, T> _cache;

        public GeneralCache()
        {
            _cache = new Dictionary<int, T>();
        }

        public virtual async Task<bool> ContainsAsync(int id)
        {
            return _cache.ContainsKey(id);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return _cache[id];
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return _cache.Values.ToList();
        }

        public async Task<T> AddAsync(T entity)
        {
            _cache[entity.Id] = entity;
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            if (_cache.ContainsKey(entity.Id))
                _cache.Remove(entity.Id);
        }
    }
}