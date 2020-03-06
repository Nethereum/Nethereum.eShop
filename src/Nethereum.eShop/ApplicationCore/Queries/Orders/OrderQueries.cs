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

        public async Task<Paginated<OrderExcerpt>> GetByBuyerIdAsync(string buyerId, string sortBy = null, int offset = 0, int fetch = 50)
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
ORDER BY [{sortBy}]
OFFSET @offset ROWS
FETCH NEXT @fetch ROWS ONLY;
"
                        , parameters
                    );

                return new Paginated<OrderExcerpt>(offset, fetch, parameters.Get<int>("@totalCount"), rows, sortBy);
            }
        }

    }
}
