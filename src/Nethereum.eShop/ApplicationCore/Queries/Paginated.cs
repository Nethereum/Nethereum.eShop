using System.Collections.Generic;

namespace Nethereum.eShop.ApplicationCore.Queries
{
    public class Paginated<TModel> where TModel : class
    {
        public int Offset { get; private set; }

        public int Fetch { get; private set; }

        public int TotalCount { get; private set; }

        public IEnumerable<TModel> Data { get; private set; }

        public string SortedBy { get; private set; }

        public Paginated(int offset, int fetch, int totalCount, IEnumerable<TModel> data, string sortedBy)
        {
            Offset = offset;
            Fetch = fetch;
            TotalCount = totalCount;
            Data = data;
            SortedBy = sortedBy;
        }

        public Paginated(int totalCount, IEnumerable<TModel> data, PaginatedQuerySpecification paginatedQuery)
        {
            TotalCount = totalCount;
            Data = data;
            Offset = paginatedQuery.Offset;
            Fetch = paginatedQuery.Fetch;
            SortedBy = paginatedQuery.SortBy;
        }
    }

    public class PaginatedQuerySpecification
    {
        public int Offset { get; set; }

        public int Fetch { get; set; }

        public string SortBy { get; set; }
    }
}
