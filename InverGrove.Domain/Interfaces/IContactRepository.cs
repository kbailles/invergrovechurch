using System;
using System.Collections.Generic;
using InverGrove.Domain.Models;


namespace InverGrove.Domain.Interfaces
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetAllContacts();
        int Add(IContact contact);
    }
}
