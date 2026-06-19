using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Experimental
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string serverName = @".\SQLEXPRESS";
            Person person1 = new Person(1, "Ivan", "Ivanov", 20, "Sofia");
            Person person2 = new Person(2, "Georgi", "Georgiev", 30, "Plovdiv");
            Person person3 = new Person(3, "Dimitar", "Dimitrov", 50, "Vratsa");
            Console.WriteLine(person1.ToString());
            Console.WriteLine(person2.ToString());
            Console.WriteLine(person3.ToString());
            //AppDbContext.AddRecord(person1);
        }
    }
    public class Person
    {
        public Person(int id, string firstName, string lastName, int age, string town)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.Town = town;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Town { get; set; }
        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName} with ID {this.Id} and age {this.Age} is from {this.Town}";
        }
    }
    public class AppDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        //public void AddRecord(Person person)
        //{
        //    People.Add(person);
        //}
    }
}
