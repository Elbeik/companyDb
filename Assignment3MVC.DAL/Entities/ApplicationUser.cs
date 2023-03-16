using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3MVC.DAL.Entities
{
	public class ApplicationUser:IdentityUser
	{
		public bool IsAgree { get; set; }
	}
}
