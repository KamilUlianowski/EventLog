using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLog.Core.Common;
using WebLog.Core.Models;

namespace WebLog.Core.ViewModels.UserAccountsViewModels
{
    public class ParentAccountViewModel
    {
        public Parent Parent { get; set; }
        public IEnumerable<SchoolGrade> SchoolGrades { get; set; }
        public Dictionary<string, List<SchoolGrade>> DictGrades { get; set; }
        public IList<SelectListItem> Teachers { get; set; }
        public int SelectedTeacher { get; set; }
        public List<Student> Students { get; set; }
        public string Message { get; set; }

        public ParentAccountViewModel()
        {

        }

        public ParentAccountViewModel(Parent parent, IEnumerable<SchoolGrade> schoolGrades, List<Teacher> teachers)
        {
            Parent = parent;
            SchoolGrades = schoolGrades;
            DictGrades = ConvertHelper.StudentSchoolGrades(SchoolGrades);
            Teachers =
                teachers.Select(x => new SelectListItem { Text = x.Name + " " + x.Surname, Value = x.Id.ToString() })
                    .ToList();
        }

        public ParentAccountViewModel(Parent parent, List<Teacher> teachers)
        {
            Parent = parent;
            DictGrades = ConvertHelper.StudentSchoolGrades(SchoolGrades);
            Teachers =
                teachers.Select(x => new SelectListItem { Text = x.Name + " " + x.Surname, Value = x.Id.ToString() })
                    .ToList();
        }
    }
}