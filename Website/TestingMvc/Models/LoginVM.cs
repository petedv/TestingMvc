using System;
using System.ComponentModel.DataAnnotations;

namespace TestingMvc
{
	public class LoginVM
	{
		[Required(ErrorMessage="Username is required.")]
		[MinLength(4, ErrorMessage="Minimum length for Username is 4.")]
		public string Username { get; set; }
		[Required(ErrorMessage="Password is required.")]
		[MinLength(4, ErrorMessage="Minimum length for Password is 4.")]
		public string Password { get; set; } 

		public string ReturnUrl { get; set; }
	}
}

