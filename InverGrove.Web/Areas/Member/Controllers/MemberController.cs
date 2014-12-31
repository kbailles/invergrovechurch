using InverGrove.Domain.Interfaces;
using System.Web.Mvc;
using InverGrove.Domain.Extensions;

namespace InverGrove.Web.Areas.Member.Controllers
{
    public class MemberController : Controller
    {
        private readonly IPersonService personService;

        public MemberController(IPersonService personService)
        {
            this.personService = personService;
        }

        [HttpGet]
        public ActionResult Directory()
        {
            return PartialView("_Directory");
        }

        [HttpGet]
        public ActionResult ManageMembers()
        {
            return PartialView("_ManageMembers");
        }

        [HttpGet]
        public ActionResult GetAllUsers()
        {
            //var people = null; // this.personService.Get;
            //return this.Json(people, JsonRequestBehavior.AllowGet).AsCamelCaseResolverResult(); 
            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
    }
}