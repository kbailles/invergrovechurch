﻿using System.Web.Mvc;
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
            return View("_Directory");
        }

        [HttpGet]
        public ActionResult ManageMembers()
        {
            var people = this.personService.GetAll();

            return View("_ManageMembers", (object)JsonConvert.SerializeObject(people, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }

        [HttpGet]
        public ActionResult AddMember()
        {
            return PartialView("_AddMember");
        }

        [HttpGet]
        public ActionResult DeleteMember()
        {
            return PartialView("_DeleteMember");
        }

        [HttpGet]
        public ActionResult GetAllUsers()
        {
            var people = this.personService.GetAll();
            return this.Json(people, JsonRequestBehavior.AllowGet).AsCamelCaseResolverResult();
        }

        [HttpPost]
        public ActionResult Add(Person person)
        {
            Guard.ArgumentNotNull(person, "person");

            var personAdded = this.personService.AddPerson(person);
            return this.Json(personAdded, JsonRequestBehavior.AllowGet).AsCamelCaseResolverResult();
        }

        [HttpPost]
        public ActionResult Delete(Person person)
        {
            Guard.ArgumentNotNull(person, "person:");

            var personAdded = this.personService.Delete(person);
            return this.Json(personAdded, JsonRequestBehavior.AllowGet).AsCamelCaseResolverResult();
        }
    }
}