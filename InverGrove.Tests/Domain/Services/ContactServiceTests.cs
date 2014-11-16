﻿
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using InverGrove.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InverGrove.Tests.Domain.Services
{
    [TestClass]
    public class ContactServiceTests
    {
        private ContactService contactService;
        private Mock<IContactRepository> mockRepository;

        [TestInitialize]
        public void Setup()
        {
            this.mockRepository = new Mock<IContactRepository>();
            this.contactService = new ContactService(this.mockRepository.Object);
        }


        private Contact OneContact()
        {
            return new Contact {    Name = "Utagawa Kunisada",
                                    Email = "UtagawaKunisada@nowhere.com",
                                    
                                    Comments =  "Testing 1, 2 3"};
        }
    }
}
