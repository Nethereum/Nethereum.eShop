using System.Collections.Generic;

namespace Nethereum.eShop.ApplicationCore.Queries
{
    public class PaginatedResult<TModel> where TModel : class
    {
        public int Offset { get; private set; }

        public int Fetch { get; private set; }

        public int TotalCount { get; private set; }

        public IEnumerable<TModel> Data { get; private set; }

        public string SortedBy { get; private set; }

        public PaginatedResult(int offset, int fetch, int totalCount, IEnumerable<TModel> data, string sortedBy)
        {
            Offset = offset;
            Fetch = fetch;
            TotalCount = totalCount;
            Data = data;
            SortedBy = sortedBy;
        }

        public PaginatedResult(int totalCount, IEnumerable<TModel> data, PaginationArgs paginatedQuery)
        {
            TotalCount = totalCount;
            Data = data;
            Offset = paginatedQuery.Offset;
            Fetch = paginatedQuery.Fetch;
            SortedBy = paginatedQuery.SortBy;
        }
    }
}
