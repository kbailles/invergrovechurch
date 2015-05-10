using System.Linq;
using System.Web.Mvc;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
        public JsonResult GetAll()
        {
            var sermons = this.sermonService.GetSermons().ToSafeList().OrderByDescending(sermon => sermon.SermonDate);

            return Json(sermons, JsonRequestBehavior.AllowGet).AsCamelCaseResolverResult(); ;
        }

        [HttpGet]
        public JsonResult GetById(int sermonId)
        {
            var sermon = this.sermonService.GetSermon(sermonId);

            return Json(sermon, JsonRequestBehavior.AllowGet).AsCamelCaseResolverResult();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public ActionResult ViewSermons()
        {
            var sermons = this.sermonService.GetSermons().ToSafeList().OrderByDescending(sermon => sermon.SermonDate);

            return PartialView("_ViewSermons", JsonConvert.SerializeObject(sermons, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver()}));
        }

        [HttpGet]
        public ActionResult SermonDetail()
        {
            return PartialView("_SermonDetail");
        }
    }
}