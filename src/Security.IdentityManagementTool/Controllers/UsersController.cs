using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RestSharp;
using Security.IdentityManagementTool.Models;
using Security.IdentityManagementTool.Filters;

namespace Security.IdentityManagementTool.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationUserManager _AppUserManager = null;

        protected ApplicationUserManager AppUserManager
        {
            get { return _AppUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NotAllowed()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(UserModel user)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = DateTime.Now
            };

            IdentityResult result = await AppUserManager.CreateAsync(applicationUser, user.Password);

            return View();
        }

        [HttpPost]
        public ActionResult CreateUserRole(UserModel user)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = DateTime.Now
            };

            IdentityResult result = AppUserManager.Create(applicationUser, user.Password);

            var usr = AppUserManager.FindByEmail(applicationUser.Email);

            AppUserManager.AddToRole(usr.Id, user.Role);

            return View();
        }

        [HttpGet]
        public JsonResult Get(string email)
        {
            var applicationUser =
                AppUserManager.FindByEmail(email);

            return Json(applicationUser, JsonRequestBehavior.AllowGet);
        }

        [Auth]
        public ActionResult Users()
        {
            return View();
        }

        [Authorize]
        public ActionResult GetUsers()
        {
            var users = AppUserManager.Users.Select(x => new {x.FirstName, x.LastName, x.Email, x.Id}).ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string id)
        {
            var user = AppUserManager.FindById(id);

            AppUserManager.Delete(user);

            return Json("success");
        }

        public void AddRoles(string userId, string[] roles)
        {
            AppUserManager.AddToRoles(userId, roles);
        }
    }
}