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
        public ActionResult ViewSermons()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                if (this.User.IsInRole("Member"))
                {
                    return Redirect(Url.Action("Directory", "Member", new { area = "Member" }));
                }

                if (this.User.IsInRole("MemberAdmin") || this.User.IsInRole("SiteAdmin"))
                {
                    return Redirect(Url.Action("ManageMembers", "Member", new { area = "Member" }));
                }
            }

            var sermons = this.sermonService.GetSermons().ToSafeList().OrderByDescending(sermon => sermon.SermonDate);

            return View("_ViewSermons", sermons);
        }

        [HttpGet]
        public ActionResult SermonDetail(int sermonId)
        {
            var sermon = this.sermonService.GetSermon(sermonId);

            return View("_SermonDetail", (object)JsonConvert.SerializeObject(sermon, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }
    }
}