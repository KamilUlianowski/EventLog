using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLog.Core.Models;

namespace WebLog.Core.ViewModels
{
    public class SchoolClassViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; }
        public Teacher Teacher { get; set; }
        public IList<SelectListItem> AllStudents { get; set; }
        public IList<SelectListItem> AllTeachers { get; set; }
        public int SelectedStudentId { get; set; }
        public int SelectedTeacherId { get; set; }

        public SchoolClassViewModel(SchoolClass schoolClass, List<Student> allStudents, List<Teacher> allTeachers)
        {
            Id = schoolClass.Id;
            Name = schoolClass.Name;
            Students = schoolClass.Students;
            Teacher = schoolClass.Teacher;
            AllStudents = allStudents.Select(x => new SelectListItem { Text = x.Name + " " + x.Surname, Value = x.Id.ToString() })
                    .ToList();
            AllTeachers =
                allTeachers.Select(x => new SelectListItem {Text = x.Name + " " + x.Surname, Value = x.Id.ToString()})
                    .ToList();
        }

        public SchoolClassViewModel()
        {

        }
    }
}