rem Applying Migrations (update db)
dotnet build
dotnet ef database update --project ..\Nethereum.eShop --context Nethereum.eShop.Infrastructure.Data.CatalogContext --no-build
dotnet ef database update --project ..\Nethereum.eShop --context Nethereum.eShop.Infrastructure.Identity.AppIdentityDbContext --no-build