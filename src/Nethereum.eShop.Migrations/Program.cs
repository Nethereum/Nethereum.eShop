using System;

namespace Nethereum.eShop.Migrations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("IMPORTANT!");
            Console.WriteLine("This 'Nethereum.eShop.Migrations' console is only for generating Entity Framework Migrations");
            Console.WriteLine("It is ONLY intended to be run as the startup project for the dotnet-ef tool to add a migration or create a script");
            Console.WriteLine();
            Console.WriteLine("To add a named migration - go to the command line and run either AddCatalogMigration.bat or AddIdentityMigratoin.bat and supply the name of the migration");
            Console.WriteLine("This will create migrations for each DB provider (e.g. SqlServer, Sqlite, MySql etc)");
            Console.WriteLine(" e.g. AddCatalogMigration.bat InitialCreate");
            Console.ReadLine();
            Console.WriteLine("To create SQL Scripts for DB Creation or Update");
            Console.WriteLine(" e.g. ScriptCatalogDbs.bat or ScriptIdentityDb.bat");
            Console.ReadLine();
        }
    }
}
