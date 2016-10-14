using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Models;

namespace WebLog.Core.ViewModels
{
    public class SubjectDetailViewModel
    {
        public Subject Subject { get; set; }
        public List<SchoolClass> SchoolClasses { get; set; }
        public List<Teacher> Teachers { get; set; }

        public SubjectDetailViewModel()
        {

        }

        public SubjectDetailViewModel(Subject subject, List<SchoolClass> schoolClasses, List<Teacher> teachers)
        {
            Subject = subject;
            SchoolClasses = schoolClasses;
            Teachers = teachers;
        }
    }
}