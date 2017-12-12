using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class SystemController : Controller
    {
       [Route("pagenotfound")]
        public ActionResult PageNotFound()
        {
            return View();
        }

        [Route("servererror")]
        public ActionResult ServerError()
        {
            return View();
        }
    }
}