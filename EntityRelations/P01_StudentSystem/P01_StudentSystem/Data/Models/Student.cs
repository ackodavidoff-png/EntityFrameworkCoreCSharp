using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.Data.Models
{
    public class Student
    {
        //public Student(int studentId, string name, string? phoneNumber, DateTime registeredOn, DateTime? birthday, ICollection<Homework> homeworks)
        //{
        //    StudentId = studentId;
        //    Name = name;
        //    PhoneNumber = phoneNumber;
        //    RegisteredOn = registeredOn;
        //    Birthday = birthday;
        //    Homeworks = homeworks;
        //}
        public Student()
        {
            Homeworks = new HashSet<Homework>();
            StudentsCourses = new HashSet<StudentCourse>();
        }
        [Key]
        public int StudentId { get; set; }
        [Required]
        [MaxLength(100)]
        [Unicode(true)]
        public string Name { get; set; } = null!;
        [Unicode(false)]
        [StringLength(10)]
        public string? PhoneNumber { get; set; }
        [Required] //may not be needed
        public DateTime RegisteredOn { get; set; }
        public DateTime? Birthday { get; set; }
        public ICollection<Homework> Homeworks { get; set; }
        public ICollection<StudentCourse> StudentsCourses { get; set; }
    }
}
