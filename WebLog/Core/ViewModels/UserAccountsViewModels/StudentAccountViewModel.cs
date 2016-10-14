using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using Microsoft.Practices.ObjectBuilder2;
using WebLog.Core.Common;
using WebLog.Core.Models;

namespace WebLog.Core.ViewModels
{
    public class StudentAccountViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<SchoolGrade> SchoolGrades { get; set; }
        public IDictionary<string, List<SchoolGrade>> DictGrades { get; set; }
        public IEnumerable<Advertisement> Advertisements { get; set; }

        public StudentAccountViewModel()
        {
            SchoolGrades = new List<SchoolGrade>();
        }

        public StudentAccountViewModel(Student student, IEnumerable<SchoolGrade> schoolGrades, IEnumerable<Advertisement> advertisements  )
        {
            Student = student;
            Advertisements = advertisements;
            SchoolGrades = schoolGrades.ToList();
            DictGrades = ConvertHelper.StudentSchoolGrades(SchoolGrades);
        }
    }
}