using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLog.Core.Models;

namespace WebLog.Core.ViewModels.SummaryViewModels
{
    public class SummaryViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IList<SelectListItem> Students { get; set; }
        public IList<SelectListItem> Subjects { get; set; }
        public int? SelectedStudent { get; set; }
        public int? SelectedSubject { get; set; }

        public SummaryViewModel(List<Student> students, List<Subject> subjects )
        {
            Students = students.Select(x => new SelectListItem { Text = x.Name + " " + x.Surname, Value = x.Id.ToString() })
                    .ToList();
            Subjects = subjects.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
                    .ToList();
        }

        public SummaryViewModel()
        {
            
        }
    }
}