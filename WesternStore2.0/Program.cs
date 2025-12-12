using Microsoft.Extensions.Configuration;
using WesternStore2._0.Data;
using WesternStore2._0.Menu;
using WesternStore2._0.Service;

namespace WesternStore2._0
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .Build();

            var connectionString = config["Mongo:ConnectionString"];
            var databaseName = config["Mongo:DatabaseName"] ?? "WesternStoreDb";

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new Exception("MongoDB connection string is missing.");
            }

            var crud = new MongoCRUD(connectionString, databaseName);

            await UserSeeder.SeedAdmin(crud);
            await ProductSeeder.SeedProducts(crud);

            var mainMenu = new MainMenu(crud);
            await mainMenu.ShowMainMenu();
        }
    }
}
