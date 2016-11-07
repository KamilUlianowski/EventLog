using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLog.Core.Models;

namespace WebLog.Core.ViewModels
{
    public class TeacherAccountViewModel
    {
        public Teacher Teacher { get; set; }
        public string Request { get; set; }
        public string Text { get; set; }
        public bool ByEmail { get; set; }
        public bool BySite { get; set; }
        public bool OnlyForParents { get; set; }
        public DateTime Date { get; set; }
        public IList<SelectListItem> Classes = new List<SelectListItem>();
        public IEnumerable<int> SelectedClasses { get; set; }

        public TeacherAccountViewModel()
        {

        }

        public TeacherAccountViewModel(Teacher teacher, List<SchoolClass> schoolClasses)
        {
            Teacher = teacher;
            SelectedClasses = new List<int>();
            foreach (var schoolClass in schoolClasses)
                Classes.Add(new SelectListItem { Text = schoolClass.Name, Value = schoolClass.Id.ToString() });
        }

        public TeacherAccountViewModel(Teacher teacher)
        {
            Teacher = teacher;
            SelectedClasses = new List<int>();
            foreach (var schoolClass in teacher.SchoolClasses)
                Classes.Add(new SelectListItem { Text = schoolClass.Name, Value = schoolClass.Id.ToString() });
        }
    }
}