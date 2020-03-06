using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Queries.Quotes
{
    public class QuoteQueries: IQuoteQueries
    {
        private readonly string _connectionString;

        public QuoteQueries(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private static string[] SortByColumns = new[] { "Id", "Status" };

        public async Task<Paginated<QuoteExcerpt>> GetByBuyerIdAsync(string buyerId, string sortBy = null, int offset = 0, int fetch = 50)
        {
            sortBy = sortBy ?? "Id";

            if (!SortByColumns.Contains(sortBy)) throw new ArgumentException(nameof(sortBy));

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@buyerId", buyerId);
                parameters.Add("@offset", offset);
                parameters.Add("@fetch", fetch);
                parameters.Add("@totalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var rows = await connection.QueryAsync<QuoteExcerpt>(
@$"
SELECT @totalCount = COUNT(1) FROM Quotes as q WHERE q.BuyerId  = @buyerId;
SELECT 
    q.Id as QuoteId,
    q.BuyerAddress,
	q.BuyerId,
    q.TransactionHash,
    q.Date,
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
ORDER BY [{sortBy}]
OFFSET @offset ROWS
FETCH NEXT @fetch ROWS ONLY;
"
                        , parameters
                    );

                return new Paginated<QuoteExcerpt>(offset, fetch, parameters.Get<int>("@totalCount"), rows);
            }
        }

    }
}
