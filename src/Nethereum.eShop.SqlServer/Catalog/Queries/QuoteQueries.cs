using Dapper;
using Microsoft.Data.SqlClient;
using Nethereum.eShop.ApplicationCore.Queries;
using Nethereum.eShop.ApplicationCore.Queries.Quotes;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.SqlServer.Catalog.Queries
{
    public class QuoteQueries: IQuoteQueries
    {
        private readonly string _connectionString;

        public QuoteQueries(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private static string[] SortByColumns = new[] { "Id", "Status" };

        public async Task<PaginatedResult<QuoteExcerpt>> GetByBuyerIdAsync(string buyerId, PaginationArgs paginationArgs)
        {
            paginationArgs.SortBy = paginationArgs.SortBy ?? "Id";

            if (!SortByColumns.Contains(paginationArgs.SortBy)) throw new ArgumentException(nameof(paginationArgs.SortBy));

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@buyerId", buyerId);
                parameters.Add("@offset", paginationArgs.Offset);
                parameters.Add("@fetch", paginationArgs.Fetch);
                parameters.Add("@totalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                string sortOrder = paginationArgs.SortDescending ? "desc" : "asc";

                var rows = await connection.QueryAsync<QuoteExcerpt>(
@$"
SELECT @totalCount = COUNT(1) FROM Quotes as q WHERE q.BuyerId  = @buyerId;
SELECT 
    q.Id as QuoteId,
    q.BuyerAddress,
	q.BuyerId,
    q.TransactionHash,
    q.Date as QuoteDate,
    q.Status,
    q.PoNumber,
    q.PoType,
    q.CurrencySymbol,
    q.Expiry,
    q.[BillTo_RecipientName],
    q.[BillTo_ZipCode],
    q.[ShipTo_RecipientName],
    q.[ShipTo_ZipCode],
	(select sum(qi.Quantity * qi.UnitPrice) from QuoteItems qi where qi.QuoteId = q.Id)  as Total,
    (select count(1) from QuoteItems qi where qi.QuoteId = q.Id)  as ItemCount
FROM Quotes as q
WHERE q.BuyerId  = @buyerId
ORDER BY [{paginationArgs.SortBy}] {sortOrder}
OFFSET @offset ROWS
FETCH NEXT @fetch ROWS ONLY;
"
                        , parameters
                    );

                return new PaginatedResult<QuoteExcerpt>(parameters.Get<int>("@totalCount"), rows, paginationArgs);
            }
        }

    }
}
