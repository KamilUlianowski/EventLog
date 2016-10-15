using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Resources;

namespace WebLog.Core.ViewModels
{
    public class NewSubjectViewModel
    {
        [Required]
        [StringLength(10, MinimumLength = 2, ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "NameClassValidation")]
        public string Name { get; set; }
    }
}