using InverGrove.Domain.Interfaces;
using System.Web.Mvc;
using InverGrove.Domain.Extensions;

namespace InverGrove.Web.Areas.Member.Controllers
{
    public class MemberController : Controller
    {
        private readonly IUserService userService;

        public MemberController(IUserService userService)
        {
            this.userService = userService;
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
            var users = this.userService.GetAllUsers();
            return this.Json(users, JsonRequestBehavior.AllowGet).AsCamelCaseResolverResult(); 
        }
    }
}