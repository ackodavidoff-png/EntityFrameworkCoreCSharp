using P01_StudentSystem.Data;

namespace P01_StudentSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            using StudentSystemContext context = new StudentSystemContext();
            try
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
