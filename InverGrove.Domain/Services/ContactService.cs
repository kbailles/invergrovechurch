using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Utils;

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
            Guard.ParameterNotNull(contact, "contact");

            try
            {
                this.contactRepository.Add(contact);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
