How to run Solution

I) Certificates

Security.AuthorizationServer

You will need to install test certificates. Download the certificates from https://github.com/IdentityServer/IdentityServer3.Samples/tree/master/source/Certificates and follow the instructions.

II) Database

Security.IdentityManagementTool uses Identity to Create users and roles. So, you will need an instance of the AspNetIdentity in your Sql Server. I am using EntityFramework, so all you need to do is the following:

Update-database

III) Run Projects

Next, you need to have all the projects running. To do this, right click on Solution to access Properties. Next, on the Property Pages, select Multiple Start up projects and set Action to Start for Security.AuthenticationServer, Security.IdentityManagementTool and Security.WebAPI.

Assuming you run solution successfuly, to verify that IdentityServer has been configured, bring up Indentity ManagementTool and click on Users on the menu.

The Endpoint that renders the Users view, has been decorated with the Authorize attribute. That means, if you haven’t logged in yet, the IdentityServer will ask you to authenticate by redirecting you to the IdentityServer’s login page as shown below.

You will see that you are redirected to another url. That is the IdentityServerss address. The application requests OAuth 2.0 authorization for the OpenID Connect scopes (openId, profile etc) by redirecting the user to an identity provider.
