using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using WebLog.Core.Common;
using WebLog.Core.Models;

namespace WebLog.Core.ViewModels
{
    public class StudentAccountViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<SchoolGrade> SchoolGrades { get; set; }
        public Dictionary<string, List<SchoolGrade>> DictGrades { get; set; }

        public StudentAccountViewModel()
        {
            SchoolGrades = new List<SchoolGrade>();
        }

        public StudentAccountViewModel(Student student, IEnumerable<SchoolGrade> schoolGrades )
        {
            Student = student;
            SchoolGrades = schoolGrades.ToList();
            DictGrades = ConvertHelper.StudentSchoolGrades(SchoolGrades);
        }
    }
}