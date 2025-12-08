using WesternStore2._0.Data;
using WesternStore2._0.InputHelper;
using WesternStore2._0.Models;

namespace WesternStore2._0.Menu
{
    public class CustomerMenu
    {

        private readonly MongoCRUD Crud;
        private User Customer;
        private const string ProductsCollection = "Products";
        private readonly List<CartItem> cart = new();

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
                Console.WriteLine($"      W E L C O M E  {Customer.UserName}  ");
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
                        ShopProducts().Wait();
                        break;

                    case "2":
                        ViewCart().Wait();
                        break;

                    case "3":
                        /*CheckOut().Wait();*/
                        break;

                    case "0":
                        bool shouldLogout = LogOut();
                        if (shouldLogout)
                        {
                            return;
                        }
                        break;

                    default:
                        Console.Write("Invalid choice. Press any key to continue.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        //1. Shop
        private async Task ShopProducts()
        {
            Console.Clear();
            Console.WriteLine("=== SHOP PRODUCTS ===\n");

            var allProducts = await Crud.ReadAll<Product>(ProductsCollection);
            if (!allProducts.Any())
            {
                Console.WriteLine("No products aviable");
                Input.Pause();
                return;
            }

            ProductCategory category;

            while (true)
            {
                Console.WriteLine("[1] Clothes  [2] Horse Tack  [3] Supplies  [0] Back ");
                Console.Write("Enter choice: ");
                string catInput = Console.ReadLine().Trim();

                if (int.TryParse(catInput, out int catChoice))
                {
                    if (catChoice == 0)
                    {
                        return;
                    }
                    if (Enum.IsDefined(typeof(ProductCategory), catChoice))
                    {
                        category = (ProductCategory)catChoice;
                        break;
                    }
                }
                Console.WriteLine("Invalid choice. Try again!");
            }

            var products = allProducts.Where(p => p.Category == category).ToList();
            if (!products.Any())
            {
                Console.WriteLine("\nNo products found in thos catorgory.");
                Input.Pause();
                return;
            }

            Console.WriteLine($"\nProducts in category: {category}\n");

            for (int i = 0; i < products.Count; i++)
            {
                var p = products[i];
                Console.WriteLine($"[{i + 1}] {p.Category} | {p.Name} | {p.Brand} | {p.Price}kr| Stock: {p.Quantity}");
            }

            int index;
            while (true)
            {
                Console.Write("\nEnter product to add to cart: ");
                string productChoice = Console.ReadLine().Trim();

                if (int.TryParse(productChoice, out index) && index > 0 && index <= products.Count)
                {
                    break;
                }
                Console.WriteLine("Invalid choice. Try Again.");
            }

            if (index == 0)
            {
                return;
            }
            var product = products[index - 1];

            int quantity;
            while (true)
            {
                Console.Write($"Quantity of '{product.Name}': ");
                string quantityInput = Console.ReadLine().Trim();
                if (int.TryParse(quantityInput, out quantity) && quantity > 0 && quantity <= product.Quantity)
                {
                    break;
                }
                Console.WriteLine("Invalid quantity or not enugh in stock.");
            }

            var existing = cart.FirstOrDefault(c => c.ProductId == product.Id);
            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = quantity,
                });

            }
            Console.WriteLine($"Added {quantity} of {product.Name} to cart");
            Input.Pause();
        }



        //2. View cart 
        private async Task ViewCart()
        {
            Console.Clear();
            Console.WriteLine("=== CART ===\n");

            if (!cart.Any())
            {
                Console.WriteLine("Cart is empty.");
                Input.Pause();
                return;
            }

            decimal total = 0;
            foreach (CartItem i in cart)
            {
                decimal rowTotal = i.Price * i.Quantity;
                total += rowTotal;
                Console.WriteLine($"{i.Name} | {i.Quantity} x {i.Price}kr = {rowTotal}kr");
            }
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine($"RTTotal: {total}");
            Input.Pause();
        }



        //3. check out 








        //LogOut lite kul 
        private bool LogOut()
        {
            Console.Clear();
            Console.Write("Are you really sure you want to leave this lovely western store? (yes/no): ");
            string? answer = Console.ReadLine()?.Trim().ToLower();

            if (answer != "yes")
            {
                return false;
            }
            Console.Clear();
            Console.Write("Are you REALLY really sure you wanna leave already? (yes/no): ");
            answer = Console.ReadLine()?.Trim().ToLower();

            if (answer != "yes")
            {
                return false;

            }

            Console.Clear();
            Console.Write("Well then partner, you must press 'no' to leave... (yes/no): ");
            answer = Console.ReadLine()?.Trim().ToLower();

            if (answer == "no")
            {
                Console.Clear();
                Console.WriteLine("HAHAH! I got you, you're still here cowboy!");
                Input.Pause();
                return false;
            }
            Console.Clear();
            Console.Write("No, now for real... are you sure you wanna leave? (yes/no): ");
            answer = Console.ReadLine()?.Trim().ToLower();

            if (answer == "yes")
            {
                Console.Clear();
                Console.WriteLine("You tipped your hat and rode off into the sunset.");
                Console.WriteLine("See ya soon, partner!");
                Input.Pause();
                return true;
            }
            return false;
        }

    }
}
