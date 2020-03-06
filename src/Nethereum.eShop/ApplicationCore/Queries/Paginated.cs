using System.Collections.Generic;

namespace Nethereum.eShop.ApplicationCore.Queries
{
    public class Paginated<TModel> where TModel : class
    {
        public int Offset { get; private set; }

        public int Fetch { get; private set; }

        public long TotalCount { get; private set; }

        public IEnumerable<TModel> Data { get; private set; }

        public Paginated(int offset, int fetch, long totalCount, IEnumerable<TModel> data)
        {
            Offset = offset;
            Fetch = fetch;
            TotalCount = totalCount;
            Data = data;
        }
    }
}
