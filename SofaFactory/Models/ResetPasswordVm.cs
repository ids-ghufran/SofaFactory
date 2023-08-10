using System;
using System.ComponentModel.DataAnnotations;

namespace SofaFactory.Models
{
	public class ResetPasswordVm
	{
		[Required]
		public string UserId { get; set; }
        [Required]
        public string Token { get; set; }
        [Required(ErrorMessage = "Password field is required")]
		[DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Enter minimum 6 character password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password field is required")]
		[Compare("Password", ErrorMessage = "Password and Confirm Password Doesn't match")]
        public string ConfirmPassword { get; set; }
		
	}
}

