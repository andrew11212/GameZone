using Microsoft.AspNetCore.Identity;

namespace GameZone.Data
{
	public class Application_user:IdentityUser
	{
		public SelectListItem roleSelect { get; set; }


	}
}
