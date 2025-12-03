using WesternStore2._0.Data;
using WesternStore2._0.Models;

namespace WesternStore2._0.Menu
{
    public class AdminMenu
    {

        private readonly MongoCRUD Crud;
        private readonly User Admin;
        private const string ProductsCollection = "Products";

        public AdminMenu(MongoCRUD crud, User admin)
        {
            Crud = crud;
            Admin = admin;
        }

        public void ShowAdminMenu()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("======================================");
                Console.WriteLine("              ADMIN MENU   ");
                Console.WriteLine("======================================\n");
                Console.ResetColor();
                Console.WriteLine("[1] Show all products");
                Console.WriteLine("[2] Add product ");
                Console.WriteLine("[3] Delete product");
                Console.WriteLine("[4] Log out");
                Console.WriteLine("-------------------------------------");
                Console.Write("\nEnter choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        //ShowProducts().Wait();
                        break;
                    case "2":
                        //AddProduct().Wait();
                        break;
                    case "3":
                        //DeleteProduct().Wait();
                        break;
                    case "4":
                        Console.WriteLine("Logging out... ");
                        return;
                    default:
                        Console.Write("Invalid choice. Press any key to continue.");
                        Console.ReadKey();
                        break;
                }

            }
        }





    }
}
