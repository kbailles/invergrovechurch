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
        public ActionResult SermonDetail(string sermonId)
        {
            return View("_SermonDetail", model: sermonId);
        }
    }
}