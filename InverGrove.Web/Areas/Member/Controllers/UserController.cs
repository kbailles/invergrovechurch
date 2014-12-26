using InverGrove.Domain.Interfaces;
using System.Web.Mvc;

namespace InverGrove.Web.Areas.Member.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Directory()
        {
            return PartialView("_Directory");
        }

        [HttpGet]
        public ActionResult ManageUsers()
        {
            return PartialView("_ManageUsers");
        }

        [HttpGet]
        public ActionResult GetAllUsers()
        {
            var users = this.userService.GetAllUsers();
            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
    }
}