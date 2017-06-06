using System.Web.Http;

namespace Security.WebAPI.Controllers
{
    public class UsersController : ApiController
    {
        [Authorize]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok();
        }
    }
}
