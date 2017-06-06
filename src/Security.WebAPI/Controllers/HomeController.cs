using System.Web.Http;
using System.Web.Mvc;

namespace Security.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
    }
}
