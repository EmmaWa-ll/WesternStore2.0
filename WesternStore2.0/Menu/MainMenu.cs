using WesternStore2._0.Data;
using WesternStore2._0.Service;

namespace WesternStore2._0.Menu
{
    public class MainMenu
    {

        private readonly MongoCRUD Crud;

        public MainMenu(MongoCRUD crud)
        {
            Crud = crud;
        }


        public async Task ShowMainMenu()
        {

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("======================================");
                Console.WriteLine("      W E S T E R N   S T O R E   ");
                Console.WriteLine("======================================\n");
                Console.ResetColor();
                Console.WriteLine("[1] Login");
                Console.WriteLine("[2] Register New Customer ");
                Console.WriteLine("[0] Exit shop");
                Console.WriteLine("-------------------------------------");
                Console.Write("\nEnter choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await HandleLogin();
                        break;
                    case "2":
                        await AuthService.RegisterCustomer(Crud);
                        break;
                    case "0":
                        Console.WriteLine("Hope we'll see you back soon! Have a great day! ");
                        return;
                    default:
                        Console.Write("Invalid choice. Press any key to continue.");
                        Console.ReadKey();
                        break;
                }
            }

        }

        private async Task HandleLogin()
        {
            var loggedInUser = await AuthService.Login(Crud);

            if (loggedInUser == null)
            {
                return;
            }

            if (loggedInUser.IsAdmin)
            {
                var adminMenu = new AdminMenu(Crud, loggedInUser);
                adminMenu.ShowAdminMenu();
            }
            else
            {
                var customerMenu = new CustomerMenu(Crud, loggedInUser);
                customerMenu.ShowCustomerMenu();
            }
        }



    }
}
