using System.ComponentModel.DataAnnotations;
using Resources;

namespace WebLog.Core.ViewModels.AuthViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "Required")]
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "EmailValidation")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "Required")]
        [StringLength(15, MinimumLength = 6, ErrorMessageResourceType = typeof(Global), ErrorMessageResourceName = "PasswordValidation")]
        public string Password { get; set; }
    }
}