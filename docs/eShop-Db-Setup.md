# Nethereum eShop Db Setup

## Configuration

Connection strings and settings are loaded from configuration in the following order.  Each source will potentially overwrite settings in previous sources.

* appsettings.json
* appsettings.{environment}.json
* environmental variables
* console args
* user secrets (development environment only)

For access keys and connection strings, user secrets is preferred for the development environment.  It ensures they are excluded from source control and are able to differ from one developer to another.

## Supported DB's

* SqlServer (tested against 15.0.2000.5 (2019), SQL Server Developer Edition running locally)
* Sqlite
* InMemory

## Initial Db Setup

### Options

* Automatic migration at Startup.
    * During startup of the main "Web" app migrations will be applied to create and update the database.
    * This can be enabled with the following settings in appsettings.json
        * "CatalogApplyMigrationsOnStartup": true,
        * "IdentityApplyMigrationsOnStartup": true,
    * *Warning* For SQL Server this will may require a user with necessary permissions to create a DB.
* Manually applying SQL Scripts
    * You need to create the DB First (e.g. ``` create database EShop ```) then apply the appropriate scripts below to create the schema.
        * src\Nethereum.eShop.SqlServer\Catalog\Migrations\Scripts
        * src\Nethereum.eShop.SqlServer\Identity\Migrations\Scripts
        * src\Nethereum.eShop.Sqlite\Catalog\Migrations\Scripts
        * src\Nethereum.eShop.Sqlite\Identity\Migrations\Scripts

### DB Connections

Database providers are dictated by the ``` CatalogDbProvider ``` and `` AppIdentityDbProvider` ``` settings.  

Supported values are:
* InMemory
* SqlServer
* Sqlite

``` json
  "ConnectionStrings": {
    "CatalogConnection_SqlServer": "Server=localhost;Integrated Security=true;Initial Catalog=eShop;",
    "IdentityConnection_SqlServer": "Server=localhost;Integrated Security=true;Initial Catalog=eShop;",
    "CatalogConnection_Sqlite": "Data Source=C:/temp/eshop_catalog.db",
    "IdentityConnection_Sqlite": "Data Source=C:/temp/eshop_app_identity.db",
    "BlockchainProcessingProgressDb": "Server=localhost\\sqldev;Integrated Security=true;Initial Catalog=eShopWebJobs;"
  },
  "CatalogDbProvider": "SqlServer",
  "AppIdentityDbProvider": "SqlServer",
  "CatalogApplyMigrationsOnStartup": true,
  "IdentityApplyMigrationsOnStartup": true
```

* CatalogConnection_{DbProvider} - the main DB containing products, quotes, orders etc
    * This connection is used by both the Web and WebJob projects
* IdentityConnection_{DbProvider} - authentication DB (web login)
    * Only used by the Web project at present
* BlockchainProcessingProgressDb - stores event log processing progress (last processed block number etc)
    * Only used by the WebJobs project

Connection strings names are suffixed with the provider name (e.g. CatalogConnection_SqlServer) to allow easy switching between providers.  The developer can multiple  connection strings in settings and swap over using only the "CatalogDbProvider" and "AppIdentityDbProvider" settings.

### User Secrets

The "Web" and "WebJobs" project share the same User Secrets Id.  This is because they are expected to share the same persistence stores.  To set or change user secrets, right click the project and select "manage user-secrets".

### Adding Migrations and DB Creation Scripts

When schema changes have been made, migrations should be created so that DB's can be created or updated to the most recent schema.

The solution is setup so that migrations for multiple DB providers can be managed from one place which ensures they are in sync.  Note: creating or adding a migration does not update the database, it simply creates code or scripts which can be run later.

Migrations are created using the "dotnet-ef" tool with a specific start up project ('Nethereum.eShop.Migrations').  The migration process requires a start up project and it is not practical to use the main "Web" or "WebApp" projects as these are only configured for one Db provider at any one time.  The goal here is to create migrations for each DB provider at once.

* Installing dotnet-ef tool (you may need to update to .Net Core 3.1.2 first)
```
dotnet tool install --global dotnet-ef --version 3.1.2
```

#### Adding a migration

```
AddMigration.bat <NameOfMigration>
```

Creates a new migration in each DB provider project.  

#### Creating Db Scripts

```
ScriptDbs.bat
```

Generates a complete DB creation script for each supported Db Provider for both the Catalog connection and Identity connection.  This script can be run manually against the chosen database. 