using Microsoft.AspNet.Identity.EntityFramework;

namespace Security.IdentityManagementTool.Models
{
    public class UserModel : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}