using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AcademicRecordsApp.Models;

public partial class Student
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new HashSet<Grade>();
    public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();
}
