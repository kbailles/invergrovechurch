using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InverGrove.Domain.Interfaces
{
    public interface IContactRepository
    {
        IEnumerable<Domain.Models.Contact> GetAllContacts();
    }
}
