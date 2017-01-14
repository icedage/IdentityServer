using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Web.Helpers;
using Microsoft.Owin;
using Owin;
using IdentityServer3.AccessTokenValidation;

[assembly: OwinStartup(typeof(Security.WebAPI.Startup))]

namespace Security.WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "https://localhost:44396/identity",
                RequiredScopes = new[] { "sampleApi" },
            });

            AntiForgeryConfig.UniqueClaimTypeIdentifier = IdentityServer3.Core.Constants.ClaimTypes.Subject;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();
            
            ConfigureAuth(app);
        }
    }
}
