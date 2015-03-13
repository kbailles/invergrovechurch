using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Utils;

namespace InverGrove.Domain.Factories
{
    public class PersonFactory : IPersonFactory
    {
        private readonly IPersonService personService;
        private readonly IRegistrationService registrationService;

        public PersonFactory(IPersonService personService, IRegistrationService registrationService)
        {
            this.personService = personService;
            this.registrationService = registrationService;
        }

        public bool Create(IPerson person)
        {
            Guard.ArgumentNotNull(person, "person");

            if (person.IsUser)
            {
                // if there is a user, we have to create a register viewmodel and set the properties from the person
                // username will have to be hard-coded.
            }
            else
            {
                // add personservice.addperson(person)
            }
            return false;
        }
    }
}