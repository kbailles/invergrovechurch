using System.Web.Mvc;
using InverGrove.Domain.Exceptions;
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
            var model = (Register)this.registrationService.GetRegisterViewModel();

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Index(Register register)
        {
            if (register == null)
            {
                throw new ParameterNullException("register");
            }

            if (ModelState.IsValid)
            {
                this.registrationService.RegisterUser(register);
            }

            return this.View(register);
        }
    }
}