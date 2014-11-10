using System.Web.Mvc;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Web.Areas.Member.Controllers
{
    [Authorize(Roles = "MemberAdmin, SiteAdmin")]
    public class AdminSermonController : Controller
    {
        private readonly ISermonService sermonService;

        public AdminSermonController(ISermonService sermonService)
        {
            this.sermonService = sermonService;
        }

        // GET: Admin/Sermon
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Domain.Models.Sermon sermon)
        {
            if (sermon == null)
            {
                throw new ParameterNullException("sermon");
            }

            var sermonId = this.sermonService.AddSermon(sermon);

            return this.View();
        }
    }
}