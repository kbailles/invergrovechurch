using System.Web.Mvc;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Web.Controllers
{
    public class SermonController : Controller
    {
        private readonly ISermonService sermonService;

        public SermonController(ISermonService sermonService)
        {
            this.sermonService = sermonService;
        }

        [HttpGet]
        public ActionResult ViewSermons()
        {
            return View("_Sermons");
        }

        [HttpGet]
        public ActionResult SermonDetail()
        {
            return PartialView("_SermonDetail");
        }
    }
}