using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Security.IdentityManagementTool;

[assembly: OwinStartup(typeof(Startup))]
namespace Security.IdentityManagementTool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = "https://localhost:44396/identity",
                ClientId = "IdentityManagementTool",
                
                //In the Scope we ask what to include
                Scope = "openid profile roles",
                RedirectUri = "http://localhost:55112/",
                ResponseType = "id_token token",
                SignInAsAuthenticationType = "Cookies",
                UseTokenLifetime = false,

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = n =>
                    {
                        var id = n.AuthenticationTicket.Identity;
                        var sub = id.FindFirst(IdentityServer3.Core.Constants.ClaimTypes.Subject);
                        var roles = id.FindAll(IdentityServer3.Core.Constants.ClaimTypes.Role);

                        // create new identity and set name and role claim type
                        var nid = new ClaimsIdentity( id.AuthenticationType,
                                IdentityServer3.Core.Constants.ClaimTypes.Name , IdentityServer3.Core.Constants.ClaimTypes.Role);

                        nid.AddClaim(sub);
                        nid.AddClaims(roles);
                        

                        // keep the id_token for logout
                        nid.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));

                        nid.AddClaim(new Claim("access_token", n.ProtocolMessage.AccessToken));

                        n.AuthenticationTicket = new AuthenticationTicket( nid, n.AuthenticationTicket.Properties);
                       
                        return Task.FromResult(0);
                    }                    
                }
            });

            AntiForgeryConfig.UniqueClaimTypeIdentifier = IdentityServer3.Core.Constants.ClaimTypes.Subject;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            ConfigureAuth(app);
        }
    }
}
