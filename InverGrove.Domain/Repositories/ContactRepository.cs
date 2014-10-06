using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;

namespace InverGrove.Domain.Repositories
{
    public class ContactRepository : IContactRepository
    {
        public ContactRepository()
        {

        }

        public IEnumerable<Contact> GetAllContacts()
        {
            List<Contact> contacts = new List<Contact>();
            return contacts;
        }
    }
}
