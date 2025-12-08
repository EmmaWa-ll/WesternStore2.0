using WesternStore2._0.Data;
using WesternStore2._0.Menu;

namespace WesternStore2._0
{
    internal class Program
    {
        private static readonly MongoCRUD crud;

        static async Task Main(string[] args)
        {

            var connectionString = "mongodb://localhost:27017";
            var databaseName = "WesternStoreLocalDb";

            var crud = new MongoCRUD(connectionString, databaseName);


            var mainMenu = new MainMenu(crud);
            await mainMenu.ShowMainMenu();


        }
    }
}
