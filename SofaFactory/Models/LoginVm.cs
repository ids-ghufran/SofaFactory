using System;
using System.ComponentModel.DataAnnotations;

namespace SofaFactory.Models
{
	public class LoginVm
	{
		[Required]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}

