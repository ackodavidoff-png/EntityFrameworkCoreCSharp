using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicRecordsApp.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        public virtual ICollection<Exam> Exams { get; set; } = new HashSet<Exam>();
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();
    }
}
