using Dapper;
using Microsoft.Data.Sqlite;
using Nethereum.eShop.ApplicationCore.Queries;
using Nethereum.eShop.ApplicationCore.Queries.Quotes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.Sqlite.Catalog.Queries
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

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@buyerId", buyerId);
                parameters.Add("@offset", paginationArgs.Offset);
                parameters.Add("@fetch", paginationArgs.Fetch);

                string sortOrder = paginationArgs.SortDescending ? "desc" : "asc";

                var dbResults = await connection.QueryMultipleAsync(
@$"
SELECT COUNT(1) FROM Quotes as q WHERE q.BuyerId  = @buyerId;
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
	CAST((select sum(qi.Quantity * qi.UnitPrice) from QuoteItems qi where qi.QuoteId = q.Id) AS REAL) as Total,
    CAST((select count(1) from QuoteItems qi where qi.QuoteId = q.Id) AS REAL) as ItemCount
FROM Quotes as q
WHERE q.BuyerId  = @buyerId
ORDER BY [{paginationArgs.SortBy}] {sortOrder}
LIMIT @fetch OFFSET @offset;
"
                        , parameters
                    );

                var totalCount = dbResults.Read<int>().First();
                var rows = dbResults.Read<QuoteExcerpt>();

                return new PaginatedResult<QuoteExcerpt>(totalCount, rows, paginationArgs);
            }
        }

    }
}
