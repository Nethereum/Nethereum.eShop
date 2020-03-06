rem Create First Migration aka InitialCreate
dotnet build
dotnet ef migrations script -o c:\temp\CreateCatalogDb.sql --project ..\Nethereum.eShop --context Nethereum.eShop.Infrastructure.Data.CatalogContext --no-build
dotnet ef migrations script -o c:\temp\CreateAppIdentityContextDb.sql --project ..\Nethereum.eShop --context Nethereum.eShop.Infrastructure.Identity.AppIdentityDbContext --no-build