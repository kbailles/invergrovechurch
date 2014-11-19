using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;

namespace InverGrove.Web.Controllers.api
{
    public class ContactFormController : ApiController
    {
        private readonly IEmailService mailService;
        private readonly IContactService contactService;

        public ContactFormController(IEmailService mailService, IContactService contactService)
        {
            this.mailService = mailService;
            this.contactService = contactService;
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
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
        public async Task<IHttpActionResult> Post(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }


            //bool hasSent = this.mailService.SendContactMail(contact);
            bool isAdded = this.contactService.AddContact(contact);

            return this.Ok(contact);
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