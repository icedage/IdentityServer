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
using Newtonsoft.Json;

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
		public async Task<ActionResult> CreateAsync(PostUserModel user)
		{
			var applicationUser = new ApplicationUser()
			{
				UserName = user.UserName,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				BirthDate = System.DateTime.Now
			};

			IdentityResult result = await AppUserManager.CreateAsync(applicationUser, user.Password);

			if (!result.Succeeded)
			{
				throw new Exception("ERROR:" + result.Errors.FirstOrDefault());
			}
			
			return View();
		}

		[HttpPost]
        public async Task<JsonResult> CreateAsync1(PostUserModel user)
        {
			var applicationUser = new ApplicationUser()
			{
				UserName = user.UserName,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				BirthDate = System.DateTime.Now
            };

            IdentityResult result = await AppUserManager.CreateAsync(applicationUser, user.Password);
			
			var returnAction = new JsonResult();
			returnAction.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

			string responseJson = "";
			if (result.Succeeded)
			{
				responseJson = JsonConvert.SerializeObject(JsonConvert.SerializeObject(result), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
				HttpContext.Response.StatusDescription = "OK";
				//HttpContext.Response.StatusCode = 200;
			}
			else
			{
				responseJson = JsonConvert.SerializeObject(JsonConvert.SerializeObject(result.Errors), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
				HttpContext.Response.StatusDescription = "OK";
				HttpContext.Response.StatusCode = 404;
			}

			returnAction.ContentType = "application/json";
			returnAction.ContentEncoding = System.Text.Encoding.UTF8;
			returnAction.Data = responseJson;

			return returnAction;
		}

		[HttpPost]
        public ActionResult CreateUserRole(PostUserRoleModel user)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            IdentityResult result = AppUserManager.Create(applicationUser, user.Password);

            var usr = AppUserManager.FindByEmail(applicationUser.Email);

            var ir = AppUserManager.AddToRole(usr.Id, user.Role);
			if (ir.Succeeded)
			{				
				return View();
			}
			else
			{
				throw new Exception("AddToRoles failed " + ir.Errors.FirstOrDefault());
			}
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

            var ir=AppUserManager.Delete(user);

			if (ir.Succeeded)
			{
				return Json("success");
			}
			else
			{
				throw new Exception("AddToRoles failed " + ir.Errors.FirstOrDefault());
			}
		}

        public ActionResult AddRoles(string userId, string[] roles)
        {
            var ir = AppUserManager.AddToRoles(userId, roles);
			if (ir.Succeeded)
			{
				return Json("success");
			}
			else
			{
				throw new Exception("AddToRoles failed " + ir.Errors.FirstOrDefault());
			}

		}
	}
}