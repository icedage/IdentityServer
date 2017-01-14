using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.Default;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Security.AuthorizationServer.Factories
{
    public class UserService : UserServiceBase
    {

        public override Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            var con = new IdentityDbContext();
            var userStore = new UserStore<IdentityUser>(con);
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Users.SingleOrDefault(x => x.UserName == context.UserName);

            if (user != null)
            {
                context.AuthenticateResult = new IdentityServer3.Core.Models.AuthenticateResult(user.Id, user.UserName);
            }

            return Task.FromResult(0);
        }


        public override Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var con = new IdentityDbContext();
            var userStore = new UserStore<IdentityUser>(con);
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.FindByName(context.Subject.Identity.Name);

            var roles = userManager.GetRoles(user.Id);
            var claims = new List<Claim>();

            foreach (var role in roles)
            {
                claims.Add(new Claim(IdentityServer3.Core.Constants.ClaimTypes.Role, role));
            }
           
            context.IssuedClaims = claims;

            return Task.FromResult(0);
        }
    }
}