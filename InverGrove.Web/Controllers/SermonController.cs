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
    }
}