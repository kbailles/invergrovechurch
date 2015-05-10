using System.Web.Mvc;

namespace InverGrove.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("_Home");
        }
	}
}