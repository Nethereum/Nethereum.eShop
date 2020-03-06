using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Queries.Catalog
{
    public class CatalogQueries : ICatalogQueries
    {
        private readonly string _connectionString;

        public CatalogQueries(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private static string[] SortByColumns = new[] { "Rank" };
        public async Task<Paginated<CatalogExcerpt>> GetCatalogItemsAsync(GetCatalogItemsSpecification catalogQuerySpecification)
        {
            catalogQuerySpecification.SortBy = catalogQuerySpecification.SortBy ?? "Rank";

            if (!SortByColumns.Contains(catalogQuerySpecification.SortBy)) throw new ArgumentException(nameof(catalogQuerySpecification.SortBy));

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@searchText", catalogQuerySpecification.SearchText);
                parameters.Add("@brandId", catalogQuerySpecification.BrandId);
                parameters.Add("@typeId", catalogQuerySpecification.TypeId);
                parameters.Add("@offset", catalogQuerySpecification.Offset);
                parameters.Add("@fetch", catalogQuerySpecification.Fetch);
                parameters.Add("@totalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);


                var rows = await connection.QueryAsync<CatalogExcerpt>(
@$"
		SELECT @totalCount = Count(1) FROM Catalog c
		INNER JOIN CatalogBrands b ON c.CatalogBrandId = b.Id
		INNER JOIN CatalogTypes t ON c.CatalogTypeId = t.Id
		WHERE
			(@brandId IS NULL OR (b.Id = @brandId)) AND
			(@typeId IS NULL OR (t.Id = @typeId)) AND
			(@searchText IS NULL OR ((c.[Name] LIKE '%' + @searchText + '%')) OR (b.Brand LIKE '%' + @searchText + '%'));

		SELECT c.Id, c.[Name], c.CatalogBrandId, b.[Brand], c.CatalogTypeId, t.[Type], c.PictureUri, c.Price, c.[Rank] 
        FROM Catalog c
		INNER JOIN CatalogBrands b ON c.CatalogBrandId = b.Id
		INNER JOIN CatalogTypes t ON c.CatalogTypeId = t.Id
		WHERE
			(@brandId IS NULL OR (b.Id = @brandId)) AND
			(@typeId IS NULL OR (t.Id = @typeId)) AND
			(@searchText IS NULL OR ((c.[Name] LIKE '%' + @searchText + '%')) OR (b.Brand LIKE '%' + @searchText + '%'))
        ORDER BY [{catalogQuerySpecification.SortBy}]
        OFFSET @offset ROWS
        FETCH NEXT @fetch ROWS ONLY;
"
                        , parameters
                    );

                return new Paginated<CatalogExcerpt>(parameters.Get<int>("@totalCount"), rows, catalogQuerySpecification);
            }
        }
    }
}
