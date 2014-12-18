using System.Net;
using System.Web.Mvc;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using InverGrove.Domain.Resources;

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
            return View("Index");
        }

        [HttpGet]
        public ActionResult ContactUs()
        {
            return PartialView("_ContactUs");
        }

        [HttpPost]
        public JsonResult ContactUs(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                this.ControllerContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.Json(contact, JsonRequestBehavior.AllowGet).AsCamelCaseResolverResult();
            }

            this.contactService.AddContact(contact);

            bool hasSent = this.mailService.SendContactMail(contact);

            if (!hasSent)
            {
                this.ControllerContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.Json(Messages.SendMailError, JsonRequestBehavior.AllowGet).AsCamelCaseResolverResult();
            }

            return this.Json(contact, JsonRequestBehavior.AllowGet).AsCamelCaseResolverResult(); ;
        }
    }
}