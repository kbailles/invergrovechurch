using System;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public bool AddContact(IContact contact)
        {
            return true;
        }
    }
}
