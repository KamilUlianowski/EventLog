using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLog.Core.ViewModels.AuthViewModels
{
    public class SignUpParentViewModel
    {
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Pesel { get; set; }

        public SignUpParentViewModel()
        {
            
        }

        public SignUpParentViewModel(int parentId)
        {
            ParentId = parentId;
        }
    }
}