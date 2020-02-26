# Nethereum eShop Db Setup

## Configuration

Connection strings are loaded from configuration in the following order.  Each source will potentially overwrite settings in previous sources.

* appsettings.json
* appsettings.{environment}.json
* environmental variables
* console args
* user secrets (development environment only)

For access keys and connection strings, user secrets is preferred for the development environment.  It ensures they are excluded from source control and are able to differ from one developer to another.

## Initial Db Setup

### Prerequisites
* Access to a SQL Server (tested against 15.0.2000.5 (2019), SQL Server Developer Edition running locally)
* A Connection string which allows a Database to be created on server
    * In a windows environment the current windows user would typically be setup as as sysadmin on the SQL Server and the connection strings would use Integrated Security to map the windows user to SQL Server.  This avoids setting a password in the connection string.  The initial setup involves the migration creating a database so the connection must be for a user with create db access.
* Install the most recent dotnet-ef tool (necessary to create and run migrations)
```
dotnet tool install --global dotnet-ef --version 3.1.1
```

### DB Connections
* CatalogConnection - the main DB containing products, quotes, orders etc
    * This connection is used by both the Web and WebJob projects
* IdentityConnection - authentication DB
    * Only used by the Web project at present
* BlockchainProcessingProgressDb - stores event log processing progress (last processed block number etc)
    * Only used by the WebJobs project

### Steps

* Set up user-secrets

    * Main Web Project: 
        * Right Click the "Web" project and select "Manage User-Secrets", add the settings below and amend as necessary.
        * By default the Web project runs with an in-memory database.  To use a real database, the "use-in-memory-db" setting must be set to false.
        ``` json
        {
        "use-in-memory-db": false,
        "ConnectionStrings": {
            "CatalogConnection": "Server=<YourServer>;Integrated Security=true;Initial Catalog=eShop;",
            "IdentityConnection": "Server=<YourServer>;Integrated Security=true;Initial Catalog=eShop;"
        }
        }
        ```
    * Web Jobs Project: 
        * Right Click the "Web" project and select "Manage User-Secrets", add the settings below and amend as necessary.
        ``` json
        {
        "AzureWebJobsStorage": "DefaultEndpointsProtocol=https;AccountName=<AccountName>;AccountKey=<AccountKey>;EndpointSuffix=core.windows.net",
        "ConnectionStrings": {
            "CatalogConnection": "Server=<YourServer>;Integrated Security=true;Initial Catalog=eShop;",
            "IdentityConnection": "Server=<YourServer>;Integrated Security=true;Initial Catalog=eShop;",
            "BlockchainProcessingProgressDb": "Server=<YourServer>;Integrated Security=true;Initial Catalog=eShopWebJobs;"
        }
        }
        ```
* Ensure the Web Project builds! (essential for running the initial migration)
* Ensure the SQL Server is running
* Ensure the database you require HAS NOT already been created
* Run a script to create the DB
    * In a command prompt - navigate to the root of the "Web" Project
    * Run ApplyDbMigrations.bat
        * This will create the DB and the necessary tables for the Catalog and Identity
        * The "BlockchainProcessingProgressDb" referenced by the WebJobs project is setup differently, it is setup at run-time and created if it does not exist
    * Run the Web project to ensure it works as expected


