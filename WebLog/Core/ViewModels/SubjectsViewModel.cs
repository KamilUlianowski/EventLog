using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Models;

namespace WebLog.Core.ViewModels
{
    public class SubjectsViewModel
    {
        public List<Subject> Subjects { get; set; }

        public SubjectsViewModel()
        {
            Subjects = new List<Subject>();
        }

        public SubjectsViewModel(List<Subject> subjects )
        {
            Subjects = subjects;
        }
    }
}