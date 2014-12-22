using System.Web.Mvc;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using InverGrove.Domain.Utils;

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
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ManageSermons()
        {
            return PartialView("_ManageSermons");
        }

        [HttpGet]
        public ActionResult Add()
        {
            return PartialView("_AddSermon");
        }

        [HttpPost]
        public ActionResult Add(Sermon sermon)
        {
            if (sermon == null)
            {
                throw new ParameterNullException("sermon");
            }

            sermon.ModifiedByUserId = 1;
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

            sermon.ModifiedByUserId = 1;
            var success = this.sermonService.UpdateSermon(sermon);

            return this.Json(success, JsonRequestBehavior.AllowGet).AsCamelCaseResolverResult();
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