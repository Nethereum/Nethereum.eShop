using Nethereum.eShop.ApplicationCore.Entities;
using Nethereum.eShop.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.Infrastructure.Cache
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

        public virtual Task<bool> ContainsAsync(int id)
        {
            return Task.FromResult(_cache.ContainsKey(id));
        }

        public virtual Task<T> GetByIdAsync(int id)
        {
            return Task.FromResult(_cache[id]);
        }

        public virtual Task<IReadOnlyList<T>> ListAllAsync()
        {
            IReadOnlyList<T> list = _cache.Values.ToList().AsReadOnly();
            return Task.FromResult(list);
        }

        public virtual Task<T> AddAsync(T entity)
        {
            _cache[entity.Id] = entity;
            return Task.FromResult(entity);
        }

        public Task DeleteAsync(T entity)
        {
            if (_cache.ContainsKey(entity.Id))
                _cache.Remove(entity.Id);
            return Task.CompletedTask;
        }
    }
}