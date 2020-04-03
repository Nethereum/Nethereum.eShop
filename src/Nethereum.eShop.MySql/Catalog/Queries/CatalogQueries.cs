using Dapper;
using MySql.Data.MySqlClient;
using Nethereum.eShop.ApplicationCore.Queries;
using Nethereum.eShop.ApplicationCore.Queries.Catalog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.MySql.Catalog.Queries
{
    public class CatalogQueries : ICatalogQueries
    {
        private readonly string _connectionString;

        public CatalogQueries(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private static string[] SortByColumns = new[] { "Rank" };
        public async Task<PaginatedResult<CatalogExcerpt>> GetCatalogItemsAsync(GetCatalogItemsSpecification catalogQuerySpecification)
        {
            string sortOrder = catalogQuerySpecification.SortDescending ? "desc" : "asc";
            catalogQuerySpecification.SortBy = catalogQuerySpecification.SortBy ?? "Rank";

            if (!SortByColumns.Contains(catalogQuerySpecification.SortBy)) throw new ArgumentException(nameof(catalogQuerySpecification.SortBy));

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@searchText", catalogQuerySpecification.SearchText);
                parameters.Add("@brandId", catalogQuerySpecification.BrandId);
                parameters.Add("@typeId", catalogQuerySpecification.TypeId);

                var dbResults = await connection.QueryMultipleAsync(
@$"SELECT Count(1) FROM Catalog c
INNER JOIN CatalogBrands b ON c.CatalogBrandId = b.Id
INNER JOIN CatalogTypes t ON c.CatalogTypeId = t.Id
WHERE
	(@brandId IS NULL OR (b.Id = @brandId)) AND
	(@typeId IS NULL OR (t.Id = @typeId)) AND
	(@searchText IS NULL OR ((c.Name LIKE CONCAT('%', @searchText,'%'))) OR (b.Brand LIKE CONCAT('%', @searchText, '%')));
SELECT c.Id, c.Name, c.CatalogBrandId, b.Brand, c.CatalogTypeId, t.Type, c.PictureUri, c.Price, c.Rank
FROM Catalog c
INNER JOIN CatalogBrands b ON c.CatalogBrandId = b.Id
INNER JOIN CatalogTypes t ON c.CatalogTypeId = t.Id
WHERE
	(@brandId IS NULL OR (b.Id = @brandId)) AND
	(@typeId IS NULL OR (t.Id = @typeId)) AND
	(@searchText IS NULL OR ((c.Name LIKE CONCAT('%', @searchText,'%'))) OR (b.Brand LIKE CONCAT('%', @searchText, '%')))
ORDER BY c.{catalogQuerySpecification.SortBy} {sortOrder}
LIMIT {catalogQuerySpecification.Offset},{catalogQuerySpecification.Fetch};"
                , parameters
            );

                var totalCount = dbResults.Read<int>().Single();
                var rows = dbResults.Read<CatalogExcerpt>();

                return new PaginatedResult<CatalogExcerpt>(totalCount, rows, catalogQuerySpecification);
            }
        }
    }
}
