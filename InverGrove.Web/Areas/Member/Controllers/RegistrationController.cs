using System.Web.Mvc;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.ViewModels;

namespace InverGrove.Web.Areas.Member.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            this.registrationService = registrationService;
        }

        // GET: Admin/Registration
        [HttpGet]
        public ActionResult Index()
        {
            var userIsSiteAdmin = User.IsInRole("SiteAdmin");

            var model = (Register)this.registrationService.GetRegisterViewModel(userIsSiteAdmin);

            return this.View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(Register register)
        {
            if (register == null)
            {
                throw new ParameterNullException("register");
            }

            if (ModelState.IsValid)
            {
                var creationResult = this.registrationService.RegisterUser(register);

                if (!creationResult.Success)
                {
                    ModelState.AddModelError("", creationResult.MembershipCreateStatus.ErrorCodeToString());
                }
            }

            return this.View(register);
        }
    }
}