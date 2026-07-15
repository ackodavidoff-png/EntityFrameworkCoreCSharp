using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ProductShop.Data;
using ProductShop.DTOs.Import;
using ProductShop.Models;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.PortableExecutable;
using System.Xml.Serialization;

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
            string XMLFileName = "categories.xml";
            string fullPath = GetXMLFilePath(XMLFileName);
            string fileContent = File.ReadAllText(fullPath);
            //string result = ImportUsers(context, fileContent);
            //string result = ImportProducts(context, fileContent);
            string result = ImportCategories(context, fileContent);
            Console.WriteLine(result);
        }
        //problem 1
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            IEnumerable<ImportUsersDTO>? importUsersDTOs = Deserialize<ImportUsersDTO[]>(inputXml, "Users");
            if (importUsersDTOs == null)
            {
                importUsersDTOs = Array.Empty<ImportUsersDTO>();
            }
            ICollection<User> users = new List<User>();
            foreach (ImportUsersDTO importUsersDTO in importUsersDTOs)
            {
                if (!IsValid(importUsersDTO))
                {
                    continue;
                }
                User user = new User()
                {
                    FirstName = importUsersDTO.FirstName,
                    LastName = importUsersDTO.LastName,
                    Age = importUsersDTO.Age
                };
                users.Add(user);
            }
            context.Users.AddRange(users);
            context.SaveChanges();
            return $"Successfully imported {users.Count}";
        }
        //problem 2
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            IEnumerable<ImportProductsDTO>? importProductsDTOs = Deserialize<ImportProductsDTO[]>(inputXml, "Products");
            if (importProductsDTOs == null)
            {
                importProductsDTOs = Array.Empty<ImportProductsDTO>();
            }
            IEnumerable<int> validUsers = context.Users.AsNoTracking().Select(x => x.Id).ToArray();
            ICollection<Product> products = new List<Product>();
            foreach (ImportProductsDTO productDTO in importProductsDTOs)
            {
                if (!IsValid(productDTO))
                {
                    continue;
                }
                bool isSellerIdValid = validUsers.Contains(productDTO.SellerId);
                bool isBuyerIdValid = validUsers.Contains(productDTO.BuyerId);
                if (!isSellerIdValid || !isBuyerIdValid)
                {
                    continue;
                }
                Product product = new Product()
                {
                    Name = productDTO.Name,
                    Price = productDTO.Price,
                    SellerId = productDTO.SellerId,
                    BuyerId = productDTO.BuyerId
                };
                products.Add(product);
            }
            context.Products.AddRange(products);
            context.SaveChanges();
            return $"Successfully imported {products.Count}";
        }
        //problem 3
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            IEnumerable<ImportCategoriesDTO>? importCategoriesDTOs = Deserialize<ImportCategoriesDTO[]>(inputXml, "Categories");
            if (importCategoriesDTOs == null)
            {
                importCategoriesDTOs = Array.Empty<ImportCategoriesDTO>();
            }
            ICollection<Category> categories = new List<Category>();
            foreach (ImportCategoriesDTO importCategoriesDTO in importCategoriesDTOs)
            {
                if (!IsValid(importCategoriesDTO))
                {
                    continue;
                }
                Category category = new Category() { Name = importCategoriesDTO.Name };
                categories.Add(category);
            }
            context.Categories.AddRange(categories);
            context.SaveChanges();
            return $"Successfully imported {categories.Count}";
        }
        //helpful methods
        private static T? Deserialize<T>(string inputXML, string rootName)
        {
            //Console.WriteLine(inputXML);
            //Environment.Exit(0);
            XmlRootAttribute XMLRoot = new XmlRootAttribute(rootName);
            XmlSerializer XMLSerializer = new XmlSerializer(typeof(T), XMLRoot);
            StringReader stringReader = new StringReader(inputXML);
            T? result = (T?)XMLSerializer.Deserialize(stringReader);
            return result;
        }
        //private static T? Deserialize<T>(string inputXML, string rootName)
        //{
        //    //using System.Xml.Serialization;
        //
        //    XmlSerializer serializer = new XmlSerializer(typeof(T));
        //
        //    T dto;
        //
        //    using (StreamReader reader = new StreamReader(inputXML))
        //    {
        //        dto = (T)serializer.Deserialize(reader);
        //    }
        //    return dto;
        //}
        //private static T? Deserialize<T>(string inputXML, string rootName)
        //{
        //    using var reader = new StreamReader(inputXML);
        //
        //    XmlSerializer serializer = new XmlSerializer(typeof(T));
        //
        //    T? usersDto = (T)serializer.Deserialize(reader);
        //    return usersDto;
        //}
    private static string GetXMLFilePath(string XMLFileName)
        {
            string XMLFolderPath = @"..\..\..\Datasets\";
            string XMLFilePath = Path.Combine(XMLFolderPath, XMLFileName);
            return Path.GetFullPath(XMLFilePath);
        }
        private static bool IsValid(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            ICollection<ValidationResult> validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults);

            return isValid;
        }

    }
}