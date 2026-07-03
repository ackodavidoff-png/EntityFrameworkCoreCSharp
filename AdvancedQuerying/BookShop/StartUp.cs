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
            string result = GetBooksByCategory(db, input);
            Console.WriteLine(result);
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
    }
}


