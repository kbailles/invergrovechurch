using System.Web.Mvc;

namespace InverGrove.Web.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		public ActionResult Index()
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

			return View("_Home");
		}
	}
}