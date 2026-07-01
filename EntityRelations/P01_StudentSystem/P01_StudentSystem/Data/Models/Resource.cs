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
    public class Resource
    {
        [Key]
        public int ResourceId { get; set; }
        [Required]
        [MaxLength(50)]
        [Unicode(true)]
        public string Name { get; set; } = null!;
        [Unicode(false)]
        public string Url { get; set; } = string.Empty;
        public ResourceType ResourceType { get; set; }
        [ForeignKey(nameof(Course))]
        [Required]
        public int CourseId { get; set; }
        [Required]
        public Course Course { get; set; } = null!;
        //public ICollection<Course> Courses { get; set; }
    }
}
