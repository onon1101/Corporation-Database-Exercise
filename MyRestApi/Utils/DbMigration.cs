// Utils/DbMigration.cs
using DbUp;
using System;
using System.Linq;

namespace MyRestApi.Utils
{
    public static class DbMigration
    {
        public static void EnsureDatabase(string connectionString)
        {
            var upgrader =
                DeployChanges.To
                    .PostgresqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(typeof(DbMigration).Assembly)
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                throw new Exception("Database migration failed");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Database migration successful!");
            Console.ResetColor();
        }
    }
}