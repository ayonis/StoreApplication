using Microsoft.AspNetCore.Identity;
using Store;

namespace StoreApplication.Models
{
	public class ApplicationUser: IdentityUser<int>
	{
		public Employee Employee { get; set; }
		public Customer Customer { get; set; }
	}
}
