using WesternStore2._0.Data;
using WesternStore2._0.Menu;

namespace WesternStore2._0
{
    internal class Program
    {
        private static readonly MongoCRUD crud;

        static async Task Main(string[] args)
        {
            //var connectionString = "";
            //var databasename = "WesternStoreDB";

            var mainMenu = new MainMenu(crud);
            mainMenu.ShowMainMenu();


        }
    }
}
