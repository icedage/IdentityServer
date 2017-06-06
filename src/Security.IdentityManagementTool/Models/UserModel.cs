using Microsoft.AspNet.Identity.EntityFramework;

namespace Security.IdentityManagementTool.Models
{
	public class PostUserModel
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }
	}

	public class PostUserRoleModel : PostUserModel
	{
		public string Role { get; set; }
	}

	public class UserModel : IdentityUser
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Password { get; set; }

		public string Role { get; set; }
	}
}