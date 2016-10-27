using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLog.Core.Models;

namespace WebLog.Core.ViewModels.TestsViewModels
{
    public class NewTestViewModel
    {

        public string Name { get; set; }
        public IList<SelectListItem> Subjects { get; set; }
        public int SelectedSubject { get; set; }
        public int Time { get; set; }

        public NewTestViewModel(IList<Subject> subjects)
        {
            Subjects =
        subjects.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            .ToList();
        }

        public NewTestViewModel()
        {
            
        }

       
    }
}