using WesternStore2._0.Data;
using WesternStore2._0.Models;

namespace WesternStore2._0.Menu
{
    public class CustomerMenu
    {

        private readonly MongoCRUD Crud;
        private User Customer;
        private const string ProductsCollection = "Products";

        //private readonly List<CartItem> cart = new();

        public CustomerMenu(MongoCRUD crud, User customer)
        {
            Crud = crud;
            Customer = customer;
        }

        public void ShowCustomerMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("======================================");
                Console.WriteLine($"      W E L C O M E    ");//{customer.UserName}
                Console.WriteLine("======================================\n");
                Console.WriteLine("[1] Shop Products");
                Console.WriteLine("[2] View Cart");
                Console.WriteLine("[3] Check Out (buy)");
                Console.WriteLine("[0] Log Out");
                Console.WriteLine("-------------------------------------------");
                Console.Write("\nEnter choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        //ShopProducts().Wait();
                        break;

                    case "2":
                        //ViewCart();
                        break;

                    case "3":
                        /*CheckOut().Wait()*/
                        ;
                        break;

                    case "0":
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
