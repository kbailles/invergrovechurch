using System.Web.Mvc;
using System.Web.Security;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Resources;
using InverGrove.Domain.Utils;
using InverGrove.Domain.ViewModels;

namespace InverGrove.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IMembershipProvider membershipProvider;

        public AccountController(IMembershipProvider membershipProvider)
        {
            this.membershipProvider = membershipProvider;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginUser model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return Redirect(Url.Action("Index", "Home"));
                }

                // If we got this far, something failed, redisplay form
                ModelState.AddModelError("", Messages.IncorrectPasswordErrorMessage);
                ViewBag.ReturnUrl = returnUrl;
                return View(model);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            var resetModel = ObjectFactory.Create<ResetPassword>();
            resetModel.Code = code;

            return string.IsNullOrEmpty(code) ? View("Error") : View(resetModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPassword model)
        {
            Guard.ParameterNotNull(model, "model");

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("Password", Messages.ConfirmPasswordErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = Membership.GetUser(model.UserName, true);

            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            var result = this.membershipProvider.ChangePassword(model.UserName, model.Code, model.Password);

            if (result)
            {
                result = this.membershipProvider.UpdateSecurityQuestionAnswer(model.UserName, model.PasswordQuestion, model.PasswordAnswer);
            }

            if (result)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}