using System.Net;
using System.Web.Mvc;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using InverGrove.Domain.Resources;
using InverGrove.Domain.ViewModels;

namespace InverGrove.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IEmailService mailService;
        private readonly IContactService contactService;
        public ContactController(IEmailService mailService, IContactService contactService)
        {
            this.mailService = mailService;
            this.contactService = contactService;
        }

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

            return View("_ContactUs", new ContactForm());
        }

        [HttpPost]
        public ActionResult ContactUs(ContactForm model)
        {
            model.SuccessfullySentMessage = false;
            model.MessageSentFailure = false;

            if (!ModelState.IsValid)
            {
                return View("_ContactUs", model);
            }

            this.contactService.AddContact(model);

            bool hasSent = this.mailService.SendContactMail(model);

            if (!hasSent)
            {
                model.MessageSentFailure = true;
                return this.View("_ContactUs", model);
            }
            else
            {
                ModelState.Clear();
                return this.View("_ContactUs", new ContactForm {SuccessfullySentMessage = true});
            }
        }
    }
}