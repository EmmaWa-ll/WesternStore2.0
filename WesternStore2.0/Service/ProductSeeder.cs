using WesternStore2._0.Data;
using WesternStore2._0.Models;

namespace WesternStore2._0.Service
{
    public class ProductSeeder
    {

        private const string ProductCollection = "Products";

        public static async Task SeedProducts(MongoCRUD crud)
        {
            var existing = await crud.ReadAll<Product>(ProductCollection);
            if (existing.Any())
                return;

            var products = new List<Product>
            {
                // 👕 CLOTHES
                new Product
                {
                    Name = "Cowboy Hat",
                    Brand = "Stetson",
                    Category = ProductCategory.Clothes,
                    Price = 2999,
                    Quantity = 10
                },
                new Product
                {
                    Name = "Leather Boots",
                    Brand = "Ariat",
                    Category = ProductCategory.Clothes,
                    Price = 1999,
                    Quantity = 5
                },
                new Product
                {
                    Name = "Work Jacket",
                    Brand = "Carhartt",
                    Category = ProductCategory.Clothes,
                    Price = 1700,
                    Quantity = 8
                },
                new Product
                {
                    Name = "Bootcut Jeans",
                    Brand = "Wrangler",
                    Category = ProductCategory.Clothes,
                    Price = 999,
                    Quantity = 12
                },

                // 🐎 HORSE TACK
                new Product
                {
                    Name = "Leather Saddle",
                    Brand = "Circle Y",
                    Category = ProductCategory.HorseTack,
                    Price = 3499,
                    Quantity = 2
                },
                new Product
                {
                    Name = "Western Bridle",
                    Brand = "Weaver Leather",
                    Category = ProductCategory.HorseTack,
                    Price = 899,
                    Quantity = 6
                },
                new Product
                {
                    Name = "Saddle Bag (Leather)",
                    Brand = "Outback",
                    Category = ProductCategory.HorseTack,
                    Price = 999,
                    Quantity = 4
                },
                new Product
                {
                    Name = "Reins (Split Leather)",
                    Brand = "Weaver Leather",
                    Category = ProductCategory.HorseTack,
                    Price = 499,
                    Quantity = 10
                },

                // 📦 SUPPLIES
                new Product
                {
                    Name = "Coffee Beans",
                    Brand = "Arbuckle's",
                    Category = ProductCategory.Supplies,
                    Price = 89,
                    Quantity = 30
                },
                new Product
                {
                    Name = "Whiskey Bottle",
                    Brand = "Old Crow",
                    Category = ProductCategory.Supplies,
                    Price = 199,
                    Quantity = 15
                },
                new Product
                {
                    Name = "Cigarettes",
                    Brand = "Marlboro",
                    Category = ProductCategory.Supplies,
                    Price = 59,
                    Quantity = 25
                },
                new Product
                {
                    Name = "Wool Blanket",
                    Brand = "Praire Co",
                    Category = ProductCategory.Supplies,
                    Price = 399,
                    Quantity = 7
                }
            };

            foreach (var product in products)
            {
                await crud.Create(ProductCollection, product);
            }
        }
    }
}
