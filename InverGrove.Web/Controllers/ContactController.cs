using System.Web.Mvc;

namespace InverGrove.Web.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet]
        public ActionResult ContactUs()
        {
            return PartialView("_Contact");
        }
    }
}