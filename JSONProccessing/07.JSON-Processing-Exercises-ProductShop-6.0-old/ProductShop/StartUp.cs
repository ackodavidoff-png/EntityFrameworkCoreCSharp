using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;
using System.Text.Json;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext context = new ProductShopContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            //Environment.Exit(0);
            string JSONInput = File.ReadAllText(@"..\..\..\Datasets\categories.json");
            //string result = ImportUsers(context, JSONInput);
            //string result = ImportProducts(context, JSONInput);
            string result = ImportCategories(context, JSONInput);
            Console.WriteLine(result);
        }
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            User[] users = JsonConvert.DeserializeObject<User[]>(inputJson);
            context.Users.AddRange(users);
            context.SaveChanges();
            return $"Successfully imported {users.Length}";
        }
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            Product[] products = JsonConvert.DeserializeObject<Product[]>(inputJson);
            foreach (Product product in products)
            {
                if(product.BuyerId == null)
                {
                    //product.BuyerId = 0;
                    continue;
                }
                context.Add(product);
            }
            context.SaveChanges();
            return $"Successfully imported {products.Length}";
            //ImportProductsDTO[]? productsDTOs = JsonConvert.DeserializeObject<ImportProductsDTO[]>(inputJson);
        }
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            Category[] categories = JsonConvert.DeserializeObject<Category[]>(inputJson);
            foreach(Category category in categories)
            {
                if(category.Name == null)
                {
                    continue;
                }
                context.Add(category);
            }
            context.SaveChanges();
            return $"Successfully imported {context.Categories.Count()}";
        }
    }
}