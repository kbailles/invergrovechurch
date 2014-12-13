using System.Web.Mvc;

namespace InverGrove.Web.Controllers
{
    public class SermonController : Controller
    {
        [HttpGet]
        public ActionResult ViewSermons()
        {
            return View("_ViewSermons");
        }

        [HttpGet]
        public ActionResult SermonDetail()
        {
            return PartialView("_SermonDetail");
        }
    }
}