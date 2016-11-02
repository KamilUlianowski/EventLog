using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLog.Core.Models;

namespace WebLog.Core.ViewModels.SubjectViewModels
{
    public class SubjectSiteViewModel
    {
        public Subject Subject { get; set; }

        [Required]
        public string FilePath { get; set; }
        public string Content { get; set; }
        public List<Test> Tests { get; set; }
        public string Name { get; set; }
        public IList<SelectListItem> Subjects { get; set; }
        public int SelectedSubject { get; set; }
        public int Time { get; set; }

        public SubjectSiteViewModel(Subject subject, IList<Subject> subjects)
        {
            Subject = subject;
            Subjects =
   subjects.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
       .ToList();
        }

        public SubjectSiteViewModel(Subject subject, List<Test> tests, IList<Subject> subjects)
        {
            Subject = subject;
            Tests = tests;
            Subjects =
   subjects.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
       .ToList();
        }

        public SubjectSiteViewModel()
        {

        }


    }
}