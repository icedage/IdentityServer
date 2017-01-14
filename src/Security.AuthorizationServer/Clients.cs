using System.Collections.Generic;
using IdentityServer3.Core.Models;

namespace Security.AuthorizationServer
{
    public class Clients
    {
        private const string SecretApi = "0caf5019-6705-415c-bb59-2bbff2f07384";

        public static IEnumerable<Client> Get()
        {
            
            return new[]
            {
                new Client
                {
                    ClientName = "WebAPI Client",
                    ClientId = "api",
                    Flow = Flows.ResourceOwner,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret(SecretApi.Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        "WebAPI"
                    }
                },
                new Client
                {
                    Enabled = true,
                    ClientName = "Identity Management Tool",
                    ClientId = "IdentityManagementTool",
                    Flow = Flows.Implicit,
                    RequireConsent = true,
                    AllowRememberConsent = true,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:55112/"
                    },
                    IdentityTokenLifetime = 360,
                    AccessTokenLifetime = 3600,
                    AllowedScopes = new List<string>() { "openid", "profile" , "roles", "sampleApi" }
                },
            };
        }
    }
}