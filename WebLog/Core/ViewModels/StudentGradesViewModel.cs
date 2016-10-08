using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Models;

namespace WebLog.Core.ViewModels
{
    public class StudentGradesViewModel
    {
        public Subject Subject { get; set; }
        public SchoolClass SchoolClass { get; set; }
        public Teacher Teacher { get; set; }
        public List<SchoolGrade> SchoolGrades { get; set; }

        public StudentGradesViewModel()
        {
            
        }

        public StudentGradesViewModel(Subject subject, SchoolClass schoolClass, Teacher teacher, List<SchoolGrade> schoolGrades  )
        {
            Subject = subject;
            SchoolClass = schoolClass;
            Teacher = teacher;
            SchoolGrades = schoolGrades;
        }
    }
}