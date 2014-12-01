using System.Web.Mvc;

namespace InverGrove.Web.Areas.Member.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Home()
        {
            return PartialView("_Home");
        }
    }
}