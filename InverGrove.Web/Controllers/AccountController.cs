using System;
using System.Web.Mvc;
using System.Web.Security;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Resources;
using InverGrove.Domain.Utils;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.ViewModels;


namespace InverGrove.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IMembershipProvider membershipProvider;
        private readonly IUserVerificationService userVerificationService;


        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="membershipProvider">The membership provider.</param>
        /// <param name="userVerificationService">The user verification service.</param>
        public AccountController(IMembershipProvider membershipProvider, IUserVerificationService userVerificationService)
        {
            this.membershipProvider = membershipProvider;
            this.userVerificationService = userVerificationService;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            string accessToken = Request.QueryString["code"];

            if (accessToken.IsGuid())
            {
                Guid token = new Guid(accessToken);
                var userCandidate = this.userVerificationService.GetUserInviteNotice(token);

                if (userCandidate != null)
                {
                    return View(userCandidate);
                }
                else
                {
                    return RedirectToAction("Index", "Home"); // send the hack attempt somewhere
                }
             
            }
            else
            {
                return RedirectToAction("Index", "Home"); // send the hack attempt somewhere
            }
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetPreRegister(Register model)
        {
            string accessToken = Request.QueryString["code"];

            // valid - 0D3D730E-FCDB-4C70-A720-42E0D8B67496 (accessed)
            // valid - 19F503D7-2E53-4C5C-8419-E8ECF9F43190 (not accessed)

            // invalid -    B24E7772-4874-4EA8-80A3-72B703481135 (SP3)
            // invalid -    C34E1183-4874-4EC8-80A3-73C704482235 (made up)

            if (ModelState.IsValid && accessToken.IsGuid())
            {
                // keep the check ...
            }


            // until we decide what to do with hack attempts. 
            return RedirectToAction("Index", "Home");

        }



    }
}