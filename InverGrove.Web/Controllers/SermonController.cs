using System.Web.Mvc;

namespace InverGrove.Web.Controllers
{
    public class SermonController : Controller
    {
        [HttpGet]
        public ActionResult ViewAll()
        {
            return View("_Sermons");
        }

        [HttpGet]
        public ActionResult SermonDetail()
        {
            return View("_SermonDetail");
        }
    }
}