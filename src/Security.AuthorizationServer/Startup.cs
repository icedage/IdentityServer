using System;
using System.Security.Cryptography.X509Certificates;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using Microsoft.Owin;
using Owin;
using Security.AuthorizationServer;
using Security.AuthorizationServer.Factories;
using Serilog;

[assembly: OwinStartup(typeof(Startup))]
namespace Security.AuthorizationServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/identity", idsrvApp =>
            {
                var factory =
                    new IdentityServerServiceFactory().UseInMemoryClients(Clients.Get())
                                                      .UseInMemoryScopes(Scopes.Get());

                var userService = new UserService();


                factory.UserService = new Registration<IUserService>(reslove => userService);

                idsrvApp.UseIdentityServer(
                     new IdentityServerOptions
                     {
                         SiteName = "Standalone Identity Server",
                         SigningCertificate = LoadCertificate(),
                         Factory = factory,

                         RequireSsl = true
                     });
            });


            Serilog.Log.Logger =
                new LoggerConfiguration().MinimumLevel.Debug()
                    .WriteTo.RollingFile(pathFormat: @"c:\logs\IdSvrAdmin-{Date}.log")
                    .CreateLogger();
        }

        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
              $"{AppDomain.CurrentDomain.BaseDirectory}\\bin\\idsrv3test.pfx", "idsrv3test");
        }
    }
}
