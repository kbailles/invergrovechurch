using System;
using System.Web.Mvc;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Utils;
using InverGrove.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace InverGrove.Web.Areas.Member.Controllers
{
    public class MemberController : Controller
    {
        private readonly IPersonService personService;

        public MemberController(IPersonService personService)
        {
            this.personService = personService;
        }

        [HttpGet]
        public ActionResult Directory()
        {
            var people = this.personService.GetAll();

            return View("_Directory", (object)JsonConvert.SerializeObject(people, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }

        [Authorize(Roles = "MemberAdmin, SiteAdmin")]
        [HttpGet]
        public ActionResult ManageMembers()
        {
            var people = this.personService.GetAll();

            return View("_ManageMembers", (object)JsonConvert.SerializeObject(people, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }

        [Authorize(Roles = "MemberAdmin, SiteAdmin")]
        [HttpGet]
        public ActionResult AddMember()
        {
            return PartialView("_AddMember");
        }

        [Authorize(Roles = "MemberAdmin, SiteAdmin")]
        [HttpGet]
        public ActionResult DeleteMember()
        {
            return PartialView("_DeleteMember");
        }

        [Authorize(Roles = "MemberAdmin, SiteAdmin")]
        [HttpGet]
        public ActionResult GetAllUsers()
        {
            var people = this.personService.GetAll();
            return this.Json(people, JsonRequestBehavior.AllowGet).AsCamelCaseResolverResult();
        }

        [Authorize(Roles = "MemberAdmin, SiteAdmin")]
        [HttpPost]
        public ActionResult Add(Person person)
        {
            Guard.ArgumentNotNull(person, "person");

            var requestUrl = this.Request.Url;
            var domainHost = "";

            if (requestUrl != null)
            {
                domainHost = requestUrl.GetLeftPart(UriPartial.Authority);
            }

            person.ModifiedByUserId = this.Profile.UserId();
            var personAdded = this.personService.AddPerson(person, domainHost);
            return this.Json(personAdded, JsonRequestBehavior.AllowGet).AsCamelCaseResolverResult();
        }

        [Authorize(Roles = "MemberAdmin, SiteAdmin")]
        [HttpGet]
        public ActionResult EditUser()
        {
            return PartialView("_EditUser");
        }

        // ---------------------------------------


        [Authorize(Roles = "MemberAdmin, SiteAdmin")]
        [HttpPost]
        public ActionResult Edit(Person person)
        {
            Guard.ArgumentNotNull(person, "person");

            person.ModifiedByUserId = this.Profile.UserId();
            var personUpdated = this.personService.Edit(person);
            return this.Json(personUpdated, JsonRequestBehavior.AllowGet).AsCamelCaseResolverResult();
        }


        // ---------------------------------------

        [Authorize(Roles = "MemberAdmin, SiteAdmin")]
        [HttpPost]
        public ActionResult Delete(Person person)
        {
            Guard.ArgumentNotNull(person, "person:");

            person.ModifiedByUserId = this.Profile.UserId();
            var personUpdated = this.personService.Delete(person);
            return this.Json(personUpdated, JsonRequestBehavior.AllowGet).AsCamelCaseResolverResult();
        }
    }
}