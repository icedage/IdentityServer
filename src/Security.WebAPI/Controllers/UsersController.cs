using System.Web.Http;

namespace Security.WebAPI.Controllers
{
    public class UsersController : ApiController
    {

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok();
        }
    }
}
