using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLog.Core.Models;

namespace WebLog.Core.ViewModels
{
    public class ManageViewModel
    {
        public List<SchoolClass> Classes { get; set; }

        public ManageViewModel(List<SchoolClass> classes )
        {
            Classes = classes;
        }
    }
}