using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebLog.Core.Models
{
    public class SchoolGrade
    {
        public int Id { get; set; }

        [Required]
        public int Grade { get; set; }

        public DateTime Date { get; set; }
        public Teacher Teacher { get; set; }
        public Student Student { get; set; }
        public Subject Subject { get; set; }
            
        public SchoolGrade()
        {
            Date = DateTime.Now;
        }

        public SchoolGrade(int grade, Teacher teacher, Student student, Subject subject)
        {
            Grade = grade;
            Teacher = teacher;
            Student = student;
            Subject = subject;
            Date = DateTime.Now;
        }
    }
}