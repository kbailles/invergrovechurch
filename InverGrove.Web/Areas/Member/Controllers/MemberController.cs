using System.Web.Mvc;
using InverGrove.Domain.Interfaces;
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
        public ActionResult ManageAttendance()
        {
            return PartialView("_ManageAttendance");
        }

        [HttpGet]
        public ActionResult AddAttendance()
        {
            return PartialView("_AddAttendance");
        }

        [HttpGet]
        public ActionResult EditAttendance()
        {
            return PartialView("_EditAttendance");
        }

        [HttpGet]
        public ActionResult DeleteAttendance()
        {
            return PartialView("_DeleteAttendance");
        }

        [HttpGet]
        public ActionResult ManageMembers()
        {
            return PartialView("_ManageMembers");
        }

        [HttpGet]
        public ActionResult GetAllUsers()
        {
            var people = this.personService.GetAll();
            return this.Json(people, JsonRequestBehavior.AllowGet).AsCamelCaseResolverResult(); 
        }
    }
}