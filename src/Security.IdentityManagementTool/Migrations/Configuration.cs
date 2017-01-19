using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Security.IdentityManagementTool.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Security.IdentityManagementToolMigrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Security.IdentityManagementTool.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Security.IdentityManagementTool.Models.ApplicationDbContext context)
        {

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "SuperPowerUser",
                Email = "Cary.Grant@gmail.com",
                EmailConfirmed = true,
                FirstName = "Cary",
                LastName = "Grant",
                BirthDate = DateTime.Now
            };

            manager.Create(user, "P@ssword123");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            var adminUser = manager.FindByName("SuperPowerUser");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin" });
        }
    }
}
