using System.ComponentModel.DataAnnotations;

namespace Assignment3MVC.PL.ViewModels
{
	public class ResetPasswordViewModel
	{
        [Required(ErrorMessage = "Password Is Required")]
        [MinLength(5, ErrorMessage = "Minimum Password Length is 5 ")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [Compare("NewPassword", ErrorMessage = "Confirm Password Not match password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
