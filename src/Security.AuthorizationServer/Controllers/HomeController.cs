using System.Security.Claims;
using System.Web.Mvc;

namespace Security.AuthorizationServer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //[Auth]
        public ActionResult About()
        {
            return View((User as ClaimsPrincipal).Claims);
        }

        //[Auth]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}