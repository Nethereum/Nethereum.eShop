using Microsoft.EntityFrameworkCore;
using Nethereum.eShop.ApplicationCore.Entities;
using Nethereum.eShop.ApplicationCore.Queries;
using Nethereum.eShop.ApplicationCore.Queries.Catalog;
using Nethereum.eShop.Infrastructure.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Nethereum.eShop.InMemory.ApplicationCore.Queries.Catalog
{
    public class CatalogQueries : QueriesBase, ICatalogQueries
    {
        public CatalogQueries(CatalogContext dbContext) : base(dbContext)
        {
        }

        public async Task<PaginatedResult<CatalogExcerpt>> GetCatalogItemsAsync(GetCatalogItemsSpecification catalogQuerySpecification)
        {
            var query = Where(catalogQuerySpecification).AsNoTracking();

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip(catalogQuerySpecification.Offset)
                .Take(catalogQuerySpecification.Fetch)
                .Select(i =>
                    new CatalogExcerpt
                    {
                        Id = i.Id,
                        Brand = i.CatalogBrand.Brand,
                        Name = i.Name,
                        PictureUri = i.PictureUri,
                        Price = i.Price
                    })
                .ToListAsync();

            return new PaginatedResult<CatalogExcerpt>(totalCount, items, catalogQuerySpecification);
        }

        private IQueryable<CatalogItem> Where(GetCatalogItemsSpecification spec)
        {
            var query = _dbContext.CatalogItems
                .Include(c => c.CatalogBrand)
                .Where(c =>
                    (string.IsNullOrEmpty(spec.SearchText) || (c.Name.Contains(spec.SearchText, StringComparison.OrdinalIgnoreCase) || c.CatalogBrand.Brand.Contains(spec.SearchText, StringComparison.OrdinalIgnoreCase))) &&
                    (spec.BrandId == null || (c.CatalogBrandId == spec.BrandId)) &&
                    (spec.TypeId == null || (c.CatalogTypeId == spec.TypeId))
                );

            //TODO: implement other sort columns
            switch (spec.SortBy)
            {
                default:
                    return spec.SortDescending ? query.OrderByDescending(c => c.Rank) : query.OrderBy(c => c.Rank);
            }
        }
    }
}
