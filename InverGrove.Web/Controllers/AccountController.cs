using System.Web.Mvc;
using System.Web.Security;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Resources;
using InverGrove.Domain.Utils;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.ViewModels;
using System.Linq;
using System;
using System.Web;

namespace InverGrove.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IMembershipProvider membershipProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="membershipProvider">The membership provider.</param>
        public AccountController(IMembershipProvider membershipProvider)
        {
            this.membershipProvider = membershipProvider;
        }

        [AllowAnonymous]
        public JsonResult GetAuthenticatedUser()
        {
            RolePrincipal rolePrincipal = (RolePrincipal)this.User;
            var roles = rolePrincipal.GetRoles();
            var authenticatedUser = AuthenticatedUserFactory.Instance.Create(this.User.Identity.Name, this.User.Identity.IsAuthenticated, roles);

            return this.Json(authenticatedUser, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (this.User.Identity.IsAuthenticated)
            {
                return Redirect(Url.Action("Index", "Home", new { area = "Member" }));
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginUser model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return Redirect(Url.Action("Index", "Home", new { area = "Member" }));
                }
            }

            ModelState.AddModelError("", Messages.IncorrectPasswordErrorMessage);
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            string accessToken = Request.QueryString["code"];

            if (accessToken.IsGuid())
            {
                // page will request data on authToken via angular
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home"); // send the hack attempt somewhere
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }




        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetPreRegister(Register model)
        {
            string accessToken = Request.QueryString["code"];

            // valid -      B24E7772-4874-4EA8-80A3-72B703481135
            // invalid -    C34E1183-4874-4EC8-80A3-73C704482235

            if (ModelState.IsValid && accessToken.IsGuid())
            {

                var foo = "All is well, now we will call a service and validate the guid against an actual identity.";
            }


            // until we decide what to do with hack attempts. 
            return RedirectToAction("Index", "Home");

        }



    }
}