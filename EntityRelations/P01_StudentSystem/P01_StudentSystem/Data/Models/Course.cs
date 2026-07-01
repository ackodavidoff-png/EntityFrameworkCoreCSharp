using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.Data.Models
{
    public class Course
    {
        public Course()
        {
            this.Resources = new HashSet<Resource>();
            this.Homewoks = new HashSet<Homework>();
            this.StudentsCourses = new HashSet<StudentCourse>();
        }
        [Key]
        public int CourseId { get; set; }
        [Required]
        [MaxLength(80)]
        [Unicode(true)]
        public string Name { get; set; } = null!;
        [Unicode(true)]
        public string? Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public decimal Price { get; set; }
        public ICollection<Resource> Resources { get; set; }
        public ICollection<Homework> Homewoks { get; set; }
        public ICollection<StudentCourse> StudentsCourses { get; set; }
    }
}