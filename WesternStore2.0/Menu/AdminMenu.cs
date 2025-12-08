using WesternStore2._0.Data;
using WesternStore2._0.InputHelper;
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
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("======================================");
                Console.WriteLine("              ADMIN MENU   ");
                Console.WriteLine("======================================\n");
                Console.ResetColor();
                Console.WriteLine("[1] Show all products");
                Console.WriteLine("[2] Add product ");
                Console.WriteLine("[3] Edit product ");
                Console.WriteLine("[4] Delete product");
                Console.WriteLine("[0] Log out");
                Console.WriteLine("-------------------------------------");
                Console.Write("\nEnter choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowProducts().Wait();
                        break;
                    case "2":
                        AddProduct().Wait();
                        break;
                    case "3":
                        EditProduct().Wait();
                        break;
                    case "4":
                        DeleteProduct().Wait();
                        break;
                    case "0":
                        Console.WriteLine("Logging out... ");
                        return;
                    default:
                        Console.Write("Invalid choice. Press any key to continue.");
                        Console.ReadKey();
                        break;
                }

            }
        }

        //1. Show Products
        private async Task ShowProducts()
        {
            Console.Clear();
            Console.WriteLine("=== ALL PRODUCTS ===\n");

            var products = await Crud.ReadAll<Product>(ProductsCollection);
            if (!products.Any())
            {
                Console.WriteLine("No products found. ");
            }
            else
            {
                foreach (var p in products)
                {
                    Console.WriteLine(p.Id);
                    Console.WriteLine($"{p.Name} - {p.Category}");
                    Console.WriteLine($"Price: {p.Price}kr | Stock: {p.Quantity}");
                    Console.WriteLine("-------------------------------------");
                }
            }
            Input.Pause();
        }


        //2. Add/create product 
        private async Task AddProduct()
        {


            Console.Clear();
            Console.WriteLine("=== ADD PRODUCT ===\n");

            //lägg in min enumm (först category) -> name -> Brand -> price ->Quantity
            ProductCategory category;
            while (true)
            {
                Console.WriteLine("\nChoose product category: ");
                Console.WriteLine("[1] Clothes     [2] Horse Tack     [3] Supplies ");
                Console.Write("\nEnter number for category (1-3):  ");
                string? catogoryInput = Console.ReadLine();

                if (int.TryParse(catogoryInput, out int choice) && Enum.IsDefined(typeof(ProductCategory), choice))
                {
                    category = (ProductCategory)choice;
                    break;
                }
                Console.WriteLine("Invalid choice. Try again. \n");
            }

            string name;
            while (true)
            {
                Console.Write("Product name: ");
                name = Console.ReadLine().Trim().ToLower();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    break;
                }
                Console.WriteLine("Name cannot be empty. Try again. \n");
            }

            string brand;
            while (true)
            {
                Console.Write("Brand: ");
                brand = Console.ReadLine().Trim().ToLower();

                if (!string.IsNullOrWhiteSpace(brand))
                {
                    break;
                }
                Console.WriteLine("Brand cannot be empty. Try again. \n");
            }


            decimal price;
            while (true)
            {
                Console.Write("Price: ");
                if (decimal.TryParse(Console.ReadLine(), out price) && price >= 0)
                {
                    break;
                }
                Console.WriteLine("Invalid price. Enter numer 0 or higher.\n");

            }

            int quantity;
            while (true)
            {
                Console.Write("Quantity: ");
                if (int.TryParse(Console.ReadLine(), out quantity) && quantity >= 0)
                {
                    break;
                }
                Console.WriteLine("Invalid quantity. Enter a whole number, 0 or higgher. \n");
            }

            var product = new Product
            {
                Name = name,
                Category = category,
                Price = price,
                Quantity = quantity
            };

            await Crud.Create("Products", product);
            Console.WriteLine("\nProduct was added successfully!");
            Input.Pause();

        }



        //3. Edit product 
        private async Task EditProduct()
        {
            Console.Clear();
            Console.WriteLine("=== EDIT PRODUCT ===\n");

            var products = await Crud.ReadAll<Product>("Products");
            if (products == null || !products.Any())
            {
                Console.WriteLine("No products");
                Input.Pause();
                return;
            }

            for (int i = 0; i < products.Count; i++)
            {
                var p = products[i];
                Console.WriteLine($"[{i + 1}] {p.Category} | {p.Name} | {p.Brand} | {p.Price}kr | Stock: {p.Quantity} ");
            }

            int index;
            while (true)
            {
                Console.Write("\nEnter the number of the product to edit (0 to go back): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out index) && index >= 0 && index <= products.Count)
                {
                    break;
                }
                Console.WriteLine("Invalid choice. Try again.");
            }

            if (index == 0)
            {
                Console.WriteLine("Edit canceleed. Going back....");
                Input.Pause();
                return;
            }


            var product = products[index - 1];
            Console.WriteLine($"\nEditing: {product.Category} {product.Name}");
            Console.WriteLine($"Current price: {product.Price}");
            Console.WriteLine($"Current stock: {product.Quantity}");

            //pris?
            Console.Write($"\nNew price (leave empty to keep {product.Price}): ");
            string priceInput = Console.ReadLine().Trim();

            if (!string.IsNullOrWhiteSpace(priceInput))
            {
                if (decimal.TryParse(priceInput, out decimal newPrice) && newPrice >= 0)
                {
                    product.Price = newPrice;
                }
                else
                {
                    Console.WriteLine("Keeping old price.");
                }

            }

            //Quantity wtock? 
            Console.Write($"\nNew quantity (leave empty to keep {product.Quantity}): ");
            string quantityInput = Console.ReadLine().Trim();

            if (!string.IsNullOrWhiteSpace(quantityInput))
            {
                if (int.TryParse(quantityInput, out int newQuantity) && newQuantity >= 0)
                {
                    product.Quantity = newQuantity;
                }
                else
                {
                    Console.WriteLine("Keeping old quantity.");
                }
            }

            var updated = await Crud.Update("Products", product.Id, product);
            if (updated == null)
            {
                Console.WriteLine("\nUpdate failed. Products wasn't found.");
            }
            else
            {
                Console.WriteLine("\nProduct was updated sucesfully");
            }
            Input.Pause();
        }


        //4 Deletee
        private async Task DeleteProduct()
        {
            Console.Clear();
            Console.WriteLine("=== DELETE PRODUCT ===\n");

            var products = await Crud.ReadAll<Product>("Products");
            if (products == null || !products.Any())
            {
                Console.WriteLine("There are no procuts to delet.,");
                Input.Pause();
                return;
            }

            for (int i = 0; i < products.Count; i++)
            {
                var p = products[i];
                Console.WriteLine($"[{i + 1}]  {p.Category} | {p.Name} | {p.Brand} ");
            }

            int index;
            while (true)
            {
                Console.Write("Enter the number of the product you wanna delete (0 to cancel): ");
                string input = Console.ReadLine().Trim();

                if (int.TryParse(input, out index) && index >= 0 && index <= products.Count)
                {
                    break;
                }
                Console.WriteLine("Invalid number. Try again.");
            }

            if (index == 0)
            {
                Console.WriteLine("The delete was cancelled.");
                Input.Pause();
                return;
            }

            var productToDelete = products[index - 1];
            Console.Write($"Ar you sure you wanna delete the {productToDelete.Name} ?  (yes/no):");
            string answer = Console.ReadLine().Trim().ToLower();

            if (answer != "yes")
            {
                Console.WriteLine("Delete cancelled");
                Input.Pause();
                return;
            }

            string result = await Crud.Delete<Product>("Products", productToDelete.Id);
            Console.WriteLine(result);
            Input.Pause();
        }



    }
}
