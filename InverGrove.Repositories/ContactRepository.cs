using System;
using System.Collections.Generic;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IDataContextFactory context;

        public ContactRepository(IDataContextFactory dataContext)
        {
            this.context = dataContext;
        }

        public IEnumerable<Domain.Models.Contact> GetAllContacts()
        {
            throw new NotImplementedException();
        }
    }
}
