using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Resources;
using InverGrove.Domain.Services;
using InverGrove.Domain.Utils;
using InverGrove.Domain.ViewModels;

namespace InverGrove.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserVerificationService userVerificationService;
        private readonly IRegistrationService registrationService;
        private readonly IProfileService profileService;
        private readonly IRoleProvider roleProvider;
        private readonly ISessionStateService sessionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="userVerificationService">The user verification service.</param>
        /// <param name="registrationService">The registration service.</param>
        /// <param name="profileService">The profile service.</param>
        /// <param name="roleProvider">The role provider.</param>
        public AccountController(IUserVerificationService userVerificationService,
                                 IRegistrationService registrationService,
                                 IProfileService profileService,
            IRoleProvider roleProvider)
        {
            this.userVerificationService = userVerificationService;
            this.registrationService = registrationService;
            this.profileService = profileService;
            this.roleProvider = roleProvider;
            this.sessionService = new SessionStateService(); // not a candidate for IOC
        }

        [AllowAnonymous]
        public JsonResult GetAuthenticatedUser()
        {
            RolePrincipal rolePrincipal = (RolePrincipal)this.User;
            var roles = rolePrincipal.GetRoles();
            var authenticatedUser = AuthenticatedUserFactory.Instance.Create(this.User.Identity.Name, this.User.Identity.IsAuthenticated, roles);

            return this.Json(authenticatedUser, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

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

            return View(new LoginUser());
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

                    SetDisplayUserFirstLastName(model.UserName);
                    var userRoles = this.roleProvider.GetRolesForUser(model.UserName);

                    if (userRoles.Contains("Member"))
                    {
                        return Redirect(Url.Action("Directory", "Member", new { area = "Member" }));
                    }

                    if (userRoles.Contains("MemberAdmin") || userRoles.Contains("SiteAdmin"))
                    {
                        return Redirect(Url.Action("ManageMembers", "Member", new { area = "Member" }));
                    }
                }
            }

            ModelState.AddModelError("", Messages.IncorrectPasswordErrorMessage);
            return View(model);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Request made to the server, presumably somebody answering their email to create a new user account
        /// after having been entered by somebody into the church directory.
        /// /Account/Register?code=someguid
        /// </summary>
        /// <returns></returns>
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
                    return View(userCandidate); // presented with opportunity to register
                }

                return RedirectToAction("Index", "Home"); // send the hack attempt somewhere
            }

            return RedirectToAction("Index", "Home"); // send the hack attempt somewhere
        }


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(Register model)
        {
            Guard.ArgumentNotNull(model, "model");
            Guard.ParameterGuidNotEmpty(model.Identifier, "identifier");

            var userVerification = this.userVerificationService.GetUserInviteNotice(model.Identifier);

            if (userVerification == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                model.PersonId = userVerification.PersonId;
                var registerUserResult = this.registrationService.RegisterUser(model);

                if (registerUserResult.Success)
                {
                    return RedirectToAction("Login", "Account");
                }
            }

            // until we decide what to do with hack attempts.
            return RedirectToAction("Index", "Home");
        }
        
        private void SetDisplayUserFirstLastName(string userName)
        {
            var profile = this.profileService.GetProfileByUserName(userName);
            var displayname = string.Concat(profile.Person.FirstName, " ", profile.Person.LastName);
            this.sessionService.Add("DisplayName", displayname);
        }
    }
}