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
            Console.WriteLine("To add a name migration - go to command line and run AddMigration.bat {name}");
            Console.WriteLine("This will create migrations for each DB provider (e.g. SqlServer, Sqlite)");
            Console.WriteLine(" e.g. AddMigration.bat InitialCreate");
            Console.ReadLine();
            Console.WriteLine("To create a SQL Script for DB Creation (e.g. SqlServer, Sqlite)");
            Console.WriteLine(" e.g. ScriptDbs.bat");
            Console.ReadLine();
        }
    }
}
