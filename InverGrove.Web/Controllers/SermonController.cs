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
        public ActionResult ViewAll()
        {
            var sermons = this.sermonService.GetSermons();

            return View("_Sermons");
        }

        [HttpGet]
        public ActionResult SermonDetail(string sermonId)
        {
            return View("_SermonDetail", model: sermonId);
        }
    }
}