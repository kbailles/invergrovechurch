using System.Web.Mvc;

namespace InverGrove.Web.Areas.Member.Controllers
{
    public class UserController : Controller
    {
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
    }
}