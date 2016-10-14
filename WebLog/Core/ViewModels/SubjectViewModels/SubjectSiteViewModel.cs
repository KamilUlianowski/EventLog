using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Models;

namespace WebLog.Core.ViewModels.SubjectViewModels
{
    public class SubjectSiteViewModel
    {
        public Subject Subject { get; set; }
        public string FilePath { get; set; }
        public string Content { get; set; }

        public SubjectSiteViewModel(Subject subject)
        {
            Subject = subject;
        }

        public SubjectSiteViewModel()
        {
            
        }


    }
}