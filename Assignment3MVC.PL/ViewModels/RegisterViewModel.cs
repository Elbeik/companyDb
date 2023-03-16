using System.ComponentModel.DataAnnotations;

namespace Assignment3MVC.PL.ViewModels
{
	public class RegisterViewModel
	{

		//public string Name { get; set; }
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
		[MinLength(5, ErrorMessage = "Minimum Password Length is 5 ")]
		[DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password Is Required")]
		[Compare("Password", ErrorMessage = "Confirm Password Not match password")]
		[DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Must be Agree Requirment")]
        public bool IsAgree { get; set; }
	}
}
