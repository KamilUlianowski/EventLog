using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebLog.Core.Common;
using WebLog.Core.Observer;

namespace WebLog.Core.Models
{
    public class SchoolGrade : ISchoolGrade
    {
        public int Id { get; set; }

        [Required]
        public Grade Grade { get; set; }

        public DateTime Date { get; set; }
        public Teacher Teacher { get; set; }
        public Student Student { get; set; }
        public Subject Subject { get; set; }
        public Test MyTest { get; set; }
        public SchoolGrade()
        {
            Date = DateTime.Now;
        }

        public SchoolGrade(Grade grade, Student student, Subject subject, Test test)
        {
            Grade = grade;
            Student = student;
            Subject = subject;
            MyTest = test;
            Subject = subject;
            Date = DateTime.Now;
        }

        public SchoolGrade(Grade grade, Teacher teacher, Student student, Subject subject)
        {
            Grade = grade;
            Teacher = teacher;
            Student = student;
            Subject = subject;
            Date = DateTime.Now;
        }

        public void NotifyObserver()
        {
            Student.SendNotice();
        }
    }
}