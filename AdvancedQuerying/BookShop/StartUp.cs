namespace BookShop
{
    using BookShop.Models;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);
            string input = Console.ReadLine();
            //string result = GetBooksByAgeRestriction(db, input);
            //string result = GetGoldenBooks(db);
            //string result = GetBooksByPrice(db);
            //string result = GetBooksNotReleasedIn(db, int.Parse(input));
            //string result = GetBooksByCategory(db, input);
            //string result = GetBooksReleasedBefore(db, input);
            //string result = GetAuthorNamesEndingIn(db, input);
            //string result = GetBookTitlesContaining(db, input);
            //string result = GetBooksByAuthor(db, input);
            //string result = $"{CountBooks(db, int.Parse(input))}";
            //string result = CountCopiesByAuthor(db);
            //string result = GetTotalProfitByCategory(db);
            //string result = GetMostRecentBooks(db);
            //string result = $"{RemoveBooks(db)}";
            //Console.WriteLine(result);
            //IncreasePrices(db);
        }
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            AgeRestriction ageRestriction = Enum.Parse<AgeRestriction>(command, true);
            List<string> books = context.Books.Where(x => x.AgeRestriction == ageRestriction).OrderBy(x => x.Title).Select(x => x.Title).ToList();
            return string.Join(Environment.NewLine, books);
        }
        public static string GetGoldenBooks(BookShopContext context)
        {
            List<string> books = context.Books.Where(x => x.EditionType == EditionType.Gold && x.Copies < 5000).OrderBy(x => x.BookId).Select(x => x.Title).ToList();
            return string.Join(Environment.NewLine, books);
        }
        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var books = context.Books.Where(x => x.Price > 40).Select(x => new { x.Title, x.Price }).OrderByDescending(x => x.Price).ToList();
            foreach ( var book in books )
            {
                stringBuilder.AppendLine($"{book.Title} - ${book.Price:F2}");
            }
            return stringBuilder.ToString().TrimEnd();
        }
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            StringBuilder sb = new StringBuilder();
            var books = context.Books.Where(x => x.ReleaseDate.Value.Year != year).Select(x => new { x.Title, x.BookId }).OrderBy(x => x.BookId);
            foreach ( var book in books )
            {
                sb.AppendLine(book.Title);
            }
            return sb.ToString().TrimEnd();
        }
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string[] categories = input.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            IQueryable<string> books = context.Books.Where(x => x.BookCategories.Any(y => categories.Contains(y.Category.Name.ToLower()))).OrderBy(x => x.Title).Select(x => x.Title);
            foreach (string book in books)
            {
                stringBuilder.AppendLine(book);
            }
            return stringBuilder.ToString().TrimEnd();
        }
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            StringBuilder builder = new StringBuilder();
            var books = context.Books.Where(x => x.ReleaseDate < DateTime.ParseExact(date, "dd-MM-yyyy", null)).Select(x => new
            {
                x.Title,
                x.EditionType,
                x.Price,
                x.ReleaseDate
            }).OrderByDescending(x => x.ReleaseDate).ToList();
            foreach (var book in books)
            {
                builder.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            }
            return builder.ToString().TrimEnd();
        }
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var authors = context.Authors.Where(x => x.FirstName.EndsWith(input)).Select(x => new { x.FirstName, x.LastName }).OrderBy(x => x.FirstName).ThenBy(x => x.LastName);
            foreach(var author in authors)
            {
                stringBuilder.AppendLine($"{author.FirstName} {author.LastName}");
            }
            return stringBuilder.ToString().TrimEnd();
        }
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            List<string> books = context.Books.Where(x => x.Title.ToLower().Contains(input.ToLower())).Select(x => x.Title).OrderBy(x => x).ToList();
            return string.Join(Environment.NewLine, books);
        }
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var books = context.Books.Where(x => x.Author.LastName.ToLower().StartsWith(input.ToLower())).Select(x => new
            {
                x.Title,
                x.Author.FirstName,
                x.Author.LastName,
                x.BookId
            }).OrderBy(x => x.BookId).ToList();
            foreach(var book in books)
            {
                stringBuilder.AppendLine($"{book.Title} ({book.FirstName} {book.LastName})");
            }
            return stringBuilder.ToString().TrimEnd();
        }
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            IQueryable<Book> books = context.Books.Where(x => x.Title.Length > lengthCheck);
            return books.Count();
        }
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var result = context.Authors.Select(x => new
            {
                x.FirstName,
                x.LastName,
                CopiesCount = x.Books.Sum(x => x.Copies)
            }).OrderByDescending(x => x.CopiesCount).Select(x => new { x.FirstName, x.LastName, x.CopiesCount }).ToList();
            foreach(var author in result)
            {
                stringBuilder.AppendLine($"{author.FirstName} {author.LastName} - {author.CopiesCount}");
            }
            return stringBuilder.ToString().TrimEnd();
        }
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var categories = context.Categories.Select(x => new
            {
                x.Name,
                TotalSum = x.CategoryBooks.Sum(y => y.Book.Copies * y.Book.Price)
            }).OrderByDescending(x => x.TotalSum).ThenBy(x => x.Name).ToList();
            foreach(var category in categories)
            {
                stringBuilder.AppendLine($"{category.Name} ${category.TotalSum:F2}");
            }
            return stringBuilder.ToString().TrimEnd();
        }
        public static string GetMostRecentBooks(BookShopContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var result = context.Categories.OrderBy(x => x.Name).Select(x => new
            {
                CategoryName = x.Name,
                Top3Books = x.CategoryBooks.Select(y => y.Book).OrderByDescending(y => y.ReleaseDate).Take(3).Select(y => new
                {
                    y.Title,
                    y.ReleaseDate.Value.Year
                }).ToList()
            }).ToList();
            foreach(var category in result)
            {
                stringBuilder.AppendLine($"--{category.CategoryName}");
                foreach(var book in category.Top3Books)
                {
                    stringBuilder.AppendLine($"{book.Title} ({book.Year})");
                }
            }
            return stringBuilder.ToString().TrimEnd();
        }
        //Do not invoke this method!!!
        public static void IncreasePrices(BookShopContext context)
        {
            IQueryable<Book> books = context.Books.Where(x => x.ReleaseDate.HasValue && x.ReleaseDate.Value.Year < 2010);
            foreach(Book book in books)
            {
                book.Price += 5;
            }
            //if you want to invoke this method,make sure the line bellow is commented out!!!
            context.SaveChanges();
        }
        //Do not invoke this method!!!
        public static int RemoveBooks(BookShopContext context)
        {
            var booksToRemove = context.Books.Where(b => b.Copies < 4200).ToList();
            int count = booksToRemove.Count;
            context.Books.RemoveRange(booksToRemove);
            //If you want to invoke this method,make sure the line bellow is commented out!!!
            context.SaveChanges();
            return count;
        }
    }
}


