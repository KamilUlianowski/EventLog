using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
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

        public SubjectSiteViewModel(Subject subject)
        {
            Subject = subject;
        }

        public SubjectSiteViewModel(Subject subject, List<Test> tests )
        {
            Subject = subject;
            Tests = tests;
        }

        public SubjectSiteViewModel()
        {
            
        }


    }
}