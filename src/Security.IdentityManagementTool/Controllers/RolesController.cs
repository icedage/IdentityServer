using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Security.IdentityManagementToolInfrastructure;
using Security.IdentityManagementTool.Models;

namespace Security.IdentityManagementTool.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationRoleManager _RoleManager = null;

        protected ApplicationRoleManager AppRoleManager
        {
            get { return _RoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>(); }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RoleModel role)
        {
            AppRoleManager.Create(new IdentityRole() {Name = role.Name});
            return View();
        }

       
        public ActionResult Delete(string id)
        {
            var role = AppRoleManager.FindById(id);

            AppRoleManager.Delete(role);

            return Json("success");
        }

        [HttpGet]
        public JsonResult Get(string id)
        {
            var role =
             AppRoleManager.FindById(id);

            return Json(role, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Roles()
        {
            return View();
        }

        public JsonResult GetRoles()
        {
            var roles = new List<RoleModel>();
            
            foreach (var role in AppRoleManager.Roles)
            {
                roles.Add(new RoleModel() { Id = role.Id, Name = role.Name });
            }
            
            return Json(roles, JsonRequestBehavior.AllowGet);
        }
    }
}