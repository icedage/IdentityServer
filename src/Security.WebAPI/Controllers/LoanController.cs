using System.Web.Http;

namespace Security.WebAPI.Controllers
{
    public class LoanController : ApiController
    {
        
        [HttpPost]
        [Authorize]
        public IHttpActionResult Apply()
        {
            return Ok();
        }
    }

    public class UnderWriterController : ApiController
    {

        [Authorize(Roles= "Underwriter")]
        public IHttpActionResult Approve()
        {
            return Ok();
        }
    }
}
