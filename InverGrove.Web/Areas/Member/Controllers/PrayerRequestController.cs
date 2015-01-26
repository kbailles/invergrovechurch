using System.Web.Mvc;

namespace InverGrove.Web.Areas.Member.Controllers
{
    public class PrayerRequestController : Controller
    {

        [HttpGet]
        public ActionResult ManagePrayerRequests()
        {
            return PartialView("_ManagePrayerRequests");
        }

        [HttpGet]
        public ActionResult Add()
        {
            return PartialView("_AddPrayerRequest");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return PartialView("_EditPrayerRequest");
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return PartialView("_DeletePrayerRequest");
        }
    }
}