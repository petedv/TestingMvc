using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using database;

namespace TestingMvc
{
	public class UserVM
	{
		[Required(ErrorMessage="Required."), MinLength(4, ErrorMessage="Username must be at least 4 characters long.")]
		public string Username { get; set; }
		[Required(ErrorMessage="Required."), Display(Name="Password"), MinLength(4, ErrorMessage="Password must be at least 4 characters long.")]
		public string NewPassword { get; set; }
		[Required(ErrorMessage="Required."), Display(Name="Confirm Password"), MinLength(4, ErrorMessage="Confirm Password must be at least 4 characters long.")]
		public string NewPasswordAgain { get; set; }
		[Required(ErrorMessage="Required."), RegularExpression(@"[\w_\.]+@[\w_\.]+", ErrorMessage="Invalid Email")]
		public string Email { get; set; }
		[Required(ErrorMessage="At least one role is required.")]
		public string[] UserRoles { get; set; }

		public string[] RoleOptions 
		{
			get
			{
				return Roles.GetAllRoles();
			}
		}

		public bool PasswordsMatch()
		{
			return NewPassword.Equals(NewPasswordAgain, StringComparison.Ordinal);
		}
	}


}

