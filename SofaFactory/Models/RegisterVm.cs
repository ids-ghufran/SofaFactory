using System;
using System.ComponentModel.DataAnnotations;

namespace SofaFactory.Models
{
	public class RegisterVm
	{
		public string Email { get; set; }
		public string MobileNo { get; set; }
		public string Password { get; set; }
		[Compare("Password", ErrorMessage = "Password and Confirm Password doesn't match")]
		public string ConfirmPassword { get; set; }
	}
}

