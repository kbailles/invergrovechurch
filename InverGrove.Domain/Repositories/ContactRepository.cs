using System;
using System.Collections.Generic;
using InverGrove.Data;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;

namespace InverGrove.Domain.Repositories
{
    public class ContactRepository : EntityRepository<Data.Entities.Contact, int>, IContactRepository
    {
        public ContactRepository(IInverGroveContext dataContext)
            : base(dataContext)
        {
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            List<Contact> contacts = new List<Contact>();
            return contacts;
        }

        public int Add(IContact contact)
        {
            if (contact == null)
            {
                throw new ParameterNullException("contact");
            }

            var newEntityContact = new Data.Entities.Contact
            {
                Name = contact.Name,
                Email =  contact.Email,
                Comments = contact.Comments,
                DateSubmitted = DateTime.Now
            };

            this.Insert(newEntityContact);

            this.dataContext.Commit();

            return newEntityContact.ContactsId;
        }
    }
}
