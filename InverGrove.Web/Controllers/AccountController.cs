using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using InverGrove.Domain.Extensions;
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
        private readonly IUserVerificationService userVerificationService;
        private readonly IRegistrationService registrationService;
        private readonly IProfileService profileService;
        private readonly IRoleProvider roleProvider;
        private readonly ISessionStateService sessionService;
        private readonly IRegisterFactory registerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="userVerificationService">The user verification service.</param>
        /// <param name="registrationService">The registration service.</param>
        /// <param name="profileService">The profile service.</param>
        /// <param name="roleProvider">The role provider.</param>
        /// <param name="sessionStateService">The session state service.</param>
        /// <param name="registerFactory">The register factory.</param>
        public AccountController(IUserVerificationService userVerificationService,
            IRegistrationService registrationService,
            IProfileService profileService,
            IRoleProvider roleProvider,
            ISessionStateService sessionStateService,
            IRegisterFactory registerFactory)
        {
            this.userVerificationService = userVerificationService;
            this.registrationService = registrationService;
            this.profileService = profileService;
            this.roleProvider = roleProvider;
            this.sessionService = sessionStateService;
            this.registerFactory = registerFactory;
        }

        [HttpGet]
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
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

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
                    var userProfile = this.profileService.GetProfileByUserName(model.UserName);

                    if (!userProfile.IsDisabled)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                        SetDisplayUserFirstLastName(userProfile);
                        var userRoles = this.roleProvider.GetRolesForUser(model.UserName);

                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }

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

                if ((userCandidate != null) && (userCandidate.DateAccessed == null))
                {
                    var register = this.registerFactory.BuildByUserVerification(userCandidate);
                    return View("Register", (Register)register);
                }

                return RedirectToAction("Index", "Home"); // send the hack attempt somewhere
            }

            return RedirectToAction("Index", "Home"); // send the hack attempt somewhere
        }


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            Guard.ArgumentNotNull(model, "model");
            Guard.ParameterGuidNotEmpty(model.Identifier, "identifier");

            var userVerification = this.userVerificationService.GetUserInviteNotice(model.Identifier);

            if ((userVerification == null) || (userVerification.DateAccessed != null))
            {
                //They probably came here manually... Send them back to home page
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                model.PersonId = userVerification.PersonId;
                var registerUserResult = this.registrationService.RegisterUser(model);

                if (registerUserResult.Success)
                {
                    this.userVerificationService.UpdateUserInviteNotice(userVerification);

                    //Log them in on success...
                    var loginModel = new LoginUser
                    {
                        UserName = model.UserName,
                        Password = model.Password,
                        RememberMe = false
                    };
                    return RedirectToAction("Login", "Account", new { model = loginModel, returnUrl = "" });
                }

                if (registerUserResult.MembershipCreateStatus == MembershipCreateStatus.DuplicateUserName)
                {
                    ModelState.AddModelError("", Messages.DuplicateUserName);
                    return View("Register", model);
                }
            }

            return View("Register", model);
        }

        private void SetDisplayUserFirstLastName(IProfile userProfile)
        {
            var displayname = string.Concat(userProfile.Person.FirstName, " ", userProfile.Person.LastName);
            this.sessionService.Add("DisplayName", displayname);
        }
    }
}