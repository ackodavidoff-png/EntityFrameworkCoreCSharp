using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {
        }
        public StudentSystemContext(DbContextOptions<StudentSystemContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;Encrypted=False").LogTo(x => Console.WriteLine(x));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentsCourses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentsCourses)
                .HasForeignKey(sc => sc.CourseId);
        }
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Homework> Homeworks { get; set; } = null!;
        public virtual DbSet<Resource> Resources { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<StudentCourse> StudentsCourses { get; set; } = null!;
    }
}
