using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Models;

namespace WebLog.Core.ViewModels
{
    public class TeacherAccountViewModel
    {
        public Teacher Teacher { get; set; }
        public List<SchoolClass> SchoolClasses { get; set; }
        public List<Teacher> Teachers { get; set; }
        public string Request { get; set; }

        public TeacherAccountViewModel()
        {
            
        }

        public TeacherAccountViewModel(Teacher teacher, List<SchoolClass> schoolClasses)
        {
            Teacher = teacher;
            SchoolClasses = schoolClasses;
        }
    }
}