using System.Web.Mvc;

namespace InverGrove.Web.Areas.Member.Controllers
{
    public class NewsEventsController : Controller
    {

        [HttpGet]
        public ActionResult ManageNewsAndEvents()
        {
            return PartialView("_ManageNewsAndEvents");
        }

        [HttpGet]
        public ActionResult Add()
        {
            return PartialView("_AddNewsAndEvent");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return PartialView("_EditNewsAndEvent");
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return PartialView("_DeleteNewsAndEvent");
        }
    }
}