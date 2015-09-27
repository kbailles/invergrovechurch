using System.Linq;
using System.Web.Mvc;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using InverGrove.Domain.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web;

namespace InverGrove.Web.Areas.Member.Controllers
{
    [Authorize(Roles = "MemberAdmin, SiteAdmin")]
    public class SermonController : Controller
    {
        private readonly ISermonService sermonService;

        public SermonController(ISermonService sermonService)
        {
            this.sermonService = sermonService;
        }

        [HttpGet]
        public ActionResult ManageSermons()
        {
            var sermons = this.sermonService.GetSermons().ToSafeList().OrderByDescending(sermon => sermon.SermonDate);

            return View("_ManageSermons", (object)JsonConvert.SerializeObject(sermons, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }

        [HttpGet]
        public ActionResult Add()
        {
            return PartialView("_AddSermon");
        }

        [HttpPost]
        public ActionResult Add(Sermon sermon, HttpPostedFileBase file)
        {
            if (sermon == null)
            {
                throw new ParameterNullException("sermon");
            }

            sermon.ModifiedByUserId = this.ControllerContext.HttpContext.Profile.UserId(); ;
            var sermonId = this.sermonService.AddSermon(sermon);

            return this.Json(sermonId, JsonRequestBehavior.AllowGet).AsCamelCaseResolverResult();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return PartialView("_EditSermon");
        }

        [HttpPost]
        public ActionResult Edit(Sermon sermon)
        {
            if (sermon == null)
            {
                throw new ParameterNullException("sermon");
            }

            sermon.ModifiedByUserId = this.ControllerContext.HttpContext.Profile.UserId();
            var success = this.sermonService.UpdateSermon(sermon);

            return this.Json(success, JsonRequestBehavior.AllowGet).AsCamelCaseResolverResult();
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return PartialView("_DeleteSermon");
        }

        [HttpPost]
        public ActionResult Delete(int sermonId)
        {
            Guard.ParameterNotOutOfRange(sermonId, "sermonId");

            this.sermonService.DeleteSermon(sermonId);

            return this.Json(true, JsonRequestBehavior.AllowGet).AsCamelCaseResolverResult();
        }
    }
}