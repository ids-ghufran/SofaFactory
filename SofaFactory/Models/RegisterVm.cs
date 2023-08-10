using System;
using System.ComponentModel.DataAnnotations;
using SofaFactory.Attribute;

namespace SofaFactory.Models
{
	public class RegisterVm
	{
		[Required(ErrorMessage = "Please enter you email address")]
		[EmailAddress (ErrorMessage = "Please enter a valid email address")]
		public string Email { get; set; }
        [Required(ErrorMessage = "Please enter you Mobile No")]
        [MobileNo(ErrorMessage = "Please enter a valid Mobile No")]
		public string MobileNo { get; set; }
		[Required(ErrorMessage = "Please enter password")]
        [MinLength(6, ErrorMessage = "Enter minimum 6 character password")]
		public string Password { get; set; }
        [Required(ErrorMessage = "Please enter confirm password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password doesn't match")]
		public string ConfirmPassword { get; set; }
	}
}

