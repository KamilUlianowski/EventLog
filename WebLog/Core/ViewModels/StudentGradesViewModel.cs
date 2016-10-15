using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebLog.Core.Common;
using WebLog.Core.Models;

namespace WebLog.Core.ViewModels
{
    public class StudentGradesViewModel
    {
        public Subject Subject { get; set; }
        public SchoolClass SchoolClass { get; set; }
        public Teacher Teacher { get; set; }
        public List<SchoolGrade> SchoolGrades { get; set; }

        [Range(1, int.MaxValue)]
        public Grade NewGrade { get; set; }

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