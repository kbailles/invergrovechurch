﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using InverGrove.Domain.Models;
using Microsoft.Ajax.Utilities;

namespace InverGrove.Web.Controllers.api
{
    public class ContactFormController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}


        // POST api/Trivia
        [ResponseType(typeof(Contact))]
        public async Task<IHttpActionResult> Post([FromBody]Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            return this.Ok<Contact>(contact);
            //answer.UserId = User.Identity.Name;

            //var isCorrect = await this.StoreAsync(answer);
            //return this.Ok<bool>(isCorrect);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}