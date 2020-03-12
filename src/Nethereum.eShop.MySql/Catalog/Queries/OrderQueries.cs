using Dapper;
using MySql.Data.MySqlClient;
using Nethereum.eShop.ApplicationCore.Queries;
using Nethereum.eShop.ApplicationCore.Queries.Orders;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.MySql.Catalog.Queries
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

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@buyerId", buyerId);

                string sortOrder = paginationArgs.SortDescending ? "desc" : "asc";

                var dbResults = await connection.QueryMultipleAsync(
@$"
SELECT COUNT(1) FROM Orders as o WHERE o.BuyerId = @buyerId;
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
    o.BillTo_RecipientName,
    o.BillTo_ZipCode,
    o.ShipTo_RecipientName,
    o.ShipTo_ZipCode,
	(select sum(oi.Quantity * oi.UnitPrice) from OrderItems oi where oi.OrderId = o.Id) as Total,
    (select count(1) from OrderItems oi where oi.OrderId = o.Id) as ItemCount
FROM Orders as o
WHERE o.BuyerId = @buyerId
ORDER BY o.{paginationArgs.SortBy} {sortOrder}
LIMIT {paginationArgs.Offset},{paginationArgs.Fetch};
"
                        , parameters
                    );

                var totalCount = dbResults.Read<int>().First();
                var rows = dbResults.Read<OrderExcerpt>();

                return new PaginatedResult<OrderExcerpt>(totalCount, rows, paginationArgs);
            }
        }

    }
}
