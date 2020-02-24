rem Applying Migrations
dotnet build
dotnet ef migrations add InitialCreate --project ..\Nethereum.eShop --context Nethereum.eShop.Infrastructure.Data.CatalogContext --output-dir ..\Nethereum.eShop\Infrastructure\Data\Migrations --no-build
dotnet ef migrations add InitialCreate --project ..\Nethereum.eShop --context Nethereum.eShop.Infrastructure.Identity.AppIdentityDbContext --output-dir ..\Nethereum.eShop\Infrastructure\Identity\Migrations --no-build
dotnet build
dotnet ef database update --project ..\Nethereum.eShop --context Nethereum.eShop.Infrastructure.Data.CatalogContext --no-build
dotnet ef database update --project ..\Nethereum.eShop --context Nethereum.eShop.Infrastructure.Identity.AppIdentityDbContext --no-build