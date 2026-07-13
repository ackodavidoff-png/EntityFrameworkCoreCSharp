using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.DTOs.Import;
using ProductShop.Models;
using System.ComponentModel.DataAnnotations;

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
            string fileName = "categories-products.json";
            string filePath = GetJsonFilePath(fileName);
            string fileContent = File.ReadAllText(filePath);
            //string result = ImportUsers(context, fileContent);
            //string result = ImportProducts(context, fileContent);
            //string result = ImportCategories(context, fileContent);
            string result = ImportCategoryProducts(context, fileContent);
            Console.WriteLine(result);
        }
        //problem 1
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            IEnumerable<ImportUsersDTO>? importUsersDTOs = JsonConvert.DeserializeObject<ImportUsersDTO[]>(inputJson);
            if (importUsersDTOs == null)
            {
                importUsersDTOs = Array.Empty<ImportUsersDTO>();
            }
            ICollection<User> users = new List<User>();
            foreach (ImportUsersDTO user in importUsersDTOs)
            {
                if (!IsValid(user))
                {
                    continue;
                }
                User newUser = new User()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Age = user.Age
                };
                users.Add(newUser);
            }
            context.Users.AddRange(users);
            context.SaveChanges();
            return $"Successfully imported {users.Count}";
        }
        //problem 2
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            IEnumerable<ImportProductsDTO>? importProductsDTOs = JsonConvert.DeserializeObject<ImportProductsDTO[]>(inputJson);
            if (importProductsDTOs == null)
            {
                importProductsDTOs = Array.Empty<ImportProductsDTO>();
            }
            IEnumerable<int> validUsers = context.Users.AsNoTracking().Select(x => x.Id).ToArray();
            ICollection<Product> products = new List<Product>();
            foreach (ImportProductsDTO productDTO in importProductsDTOs)
            {
                if(!IsValid(productDTO))
                {
                    continue;
                }
                //bool isSellerValid = validUsers.Contains(productDTO.SellerId);
                //bool isBuyerValid = (!productDTO.BuyerId.HasValue) || (validUsers.Contains(productDTO.BuyerId.Value));
                //if(!isSellerValid || !isBuyerValid)
                //{
                //    continue;
                //}
                Product newProduct = new Product()
                {
                    Name = productDTO.Name,
                    Price = productDTO.Price,
                    SellerId = productDTO.SellerId,
                    BuyerId = productDTO.BuyerId
                };
                products.Add(newProduct);
            }
            context.Products.AddRange(products);
            context.SaveChanges();
            return $"Successfully imported {products.Count}";
        }
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            IEnumerable<ImportCategoriesDTO>? importCategoriesDTOs = JsonConvert.DeserializeObject<ImportCategoriesDTO[]>(inputJson);
            if (importCategoriesDTOs == null)
            {
                importCategoriesDTOs= Array.Empty<ImportCategoriesDTO>();
            }
            ICollection<Category> categories = new List<Category>();
            foreach (ImportCategoriesDTO importCategoriesDTO in importCategoriesDTOs)
            {
                if (importCategoriesDTO.Name == null)
                {
                    continue;
                }
                if(!IsValid(importCategoriesDTO))
                {
                    continue;
                }
                Category newCategory = new Category()
                {
                    Name = importCategoriesDTO.Name
                };
                categories.Add(newCategory);
            }
            context.Categories.AddRange(categories);
            context.SaveChanges();
            return $"Successfully imported {categories.Count}";
        }
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            IEnumerable<ImportCategoriesAndProductsDTO>? importCategoriesAndProductsDTOs = JsonConvert.DeserializeObject<ImportCategoriesAndProductsDTO[]>(inputJson);
            if (importCategoriesAndProductsDTOs == null)
            {
                importCategoriesAndProductsDTOs = Array.Empty<ImportCategoriesAndProductsDTO>();
            }
            ICollection<CategoryProduct> categoriesAndProducts = new List<CategoryProduct>();
            foreach(ImportCategoriesAndProductsDTO importCategoriesAndProductsDTO in importCategoriesAndProductsDTOs)
            {
                if (!IsValid(importCategoriesAndProductsDTO))
                {
                    continue;
                }
                CategoryProduct categoryProduct = new CategoryProduct()
                {
                    CategoryId = importCategoriesAndProductsDTO.CategoryId,
                    ProductId = importCategoriesAndProductsDTO.ProductId,
                };
                categoriesAndProducts.Add(categoryProduct);
            }
            context.CategoriesProducts.AddRange(categoriesAndProducts);
            context.SaveChanges();
            return $"Successfully imported {categoriesAndProducts.Count}";
        }
        //helpful methods
        private static bool IsValid(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            ICollection<ValidationResult> validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults);

            return isValid;
        }
        private static string GetJsonFilePath(string jsonFileName)
        {
            string jsonFolderRelPath = "../../../Datasets/";
            string jsonFilePath = Path.Combine(jsonFolderRelPath, jsonFileName);
            return Path.GetFullPath(jsonFilePath);
        }
    }
}