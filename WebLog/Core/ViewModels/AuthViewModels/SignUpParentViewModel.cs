using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Resources;

namespace WebLog.Core.ViewModels.AuthViewModels
{
    public class SignUpParentViewModel
    {
        public int ParentId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "NameValidation")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "SurnameValidation")]
        public string Surname { get; set; }

        [Required(ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "Required")]
        [StringLength(11, MinimumLength = 11, ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "PeselValidationLength")]
        [RegularExpression(@"^[0-9]*$", ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "PeselValidationNumbers")]
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