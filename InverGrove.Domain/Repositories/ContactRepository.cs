using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Text;
using InverGrove.Data;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using InverGrove.Domain.Utils;

namespace InverGrove.Domain.Repositories
{
    public class ContactRepository : EntityRepository<Data.Entities.Contact, int>, IContactRepository
    {
        private readonly ILogService logService;
        private readonly object syncRoot = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="logService">The log service.</param>
        public ContactRepository(IInverGroveContext dataContext, ILogService logService)
            : base(dataContext)
        {
            this.logService = logService;
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

            using (TimedLock.Lock(this.syncRoot))
            {
                try
                {
                    this.Save();
                }
                catch (SqlException sql)
                {
                    this.logService.WriteToErrorLog("Error occurred in attempting to add Contact with name: " +
                                                   contact.Name + " email:" + contact.Email + " with message: " + sql.Message);
                }
                catch (DbEntityValidationException dbe)
                {
                    var sb = new StringBuilder();
                    foreach (var error in dbe.EntityValidationErrors)
                    {
                        foreach (var ve in error.ValidationErrors)
                        {
                            sb.Append(ve.ErrorMessage + ", ");
                        }
                    }

                    this.logService.WriteToErrorLog("Error occurred in attempting to add Contact with name: " +
                                                   contact.Name + " email:" + contact.Email + " with message: " + sb.ToString());
                }
            }

            return newEntityContact.ContactsId;
        }
    }
}
