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
        public JsonResult GetSermons()
        {
            var sermons = this.sermonService.GetSermons();

            return Json(sermons, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ViewAll()
        {
            var sermons = this.sermonService.GetSermons();

            return View("_Sermons");
        }

        [HttpGet]
        public ActionResult SermonDetail(int sermonId)
        {
            var sermon = this.sermonService.GetSermon(sermonId);

            return View("_SermonDetail", sermon);
        }
    }
}