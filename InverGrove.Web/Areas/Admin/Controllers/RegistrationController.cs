using System.Web.Mvc;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.ViewModels;

namespace InverGrove.Web.Areas.Admin.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            this.registrationService = registrationService;
        }

        // GET: Admin/Registration
        public ActionResult Index()
        {
            var model = (Register)this.registrationService.GetRegisterViewModel();

            return View(model);
        }
    }
}