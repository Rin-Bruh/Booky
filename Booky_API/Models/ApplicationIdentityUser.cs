using Microsoft.AspNetCore.Identity;

namespace Booky_API.Models
{
	public class ApplicationIdentityUser : IdentityUser
	{
		public string Name { get; set; }
	}
}
