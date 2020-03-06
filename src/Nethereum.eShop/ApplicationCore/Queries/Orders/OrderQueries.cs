using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Queries.Orders
{
    public class OrderQueries: IOrderQueries
    {
        private readonly string _connectionString;

        public OrderQueries(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private static string[] SortByColumns = new[] { "Id", "Status" };

        public async Task<PaginatedResult<OrderExcerpt>> GetByBuyerIdAsync(string buyerId, PaginationArgs paginationArgs)
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

                var rows = await connection.QueryAsync<OrderExcerpt>(
@$"
SELECT @totalCount = COUNT(1) FROM [Orders] as o WHERE o.BuyerId  = @buyerId;
SELECT 
    o.Id as OrderId,
    o.QuoteId as QuoteId,
    o.BuyerAddress,
	o.BuyerId,
    o.TransactionHash,
    o.OrderDate,
    o.Status,
    o.PoNumber,
    o.PoType,
    o.CurrencySymbol,
    o.[BillTo_RecipientName],
    o.[BillTo_ZipCode],
    o.[ShipTo_RecipientName],
    o.[ShipTo_ZipCode],
	(select sum(oi.Quantity * oi.UnitPrice) from OrderItems oi where oi.OrderId = o.Id)  as Total,
    (select count(1) from OrderItems oi where oi.OrderId = o.Id)  as ItemCount
FROM [Orders] as o
WHERE o.BuyerId  = @buyerId
ORDER BY [{paginationArgs.SortBy}] {sortOrder}
OFFSET @offset ROWS
FETCH NEXT @fetch ROWS ONLY;
"
                        , parameters
                    );

                return new PaginatedResult<OrderExcerpt>(parameters.Get<int>("@totalCount"), rows, paginationArgs);
            }
        }

    }
}
