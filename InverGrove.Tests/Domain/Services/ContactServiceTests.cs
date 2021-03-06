﻿
using System;
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
        private Contact oneContact;

        [TestInitialize]
        public void Setup()
        {
            this.mockRepository = new Mock<IContactRepository>();
            this.contactService = new ContactService(this.mockRepository.Object);
            this.oneContact = this.OneContact();
        }

        [TestMethod]
        public void AddContact_CallsRepository_ExpectsVerify()
        {
            this.mockRepository.Setup(r => r.Add(It.IsAny<Contact>())).Verifiable();
            this.contactService.AddContact(this.oneContact);

            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void AddContact_RepositoryThrowsError_ExpectsFalse()
        {
            this.mockRepository.Setup(r => r.Add(It.IsAny<Contact>())).Throws(new ApplicationException());
            var result = this.contactService.AddContact(this.oneContact);

            Assert.IsFalse(result);
        }

        private Contact OneContact()
        {
            return new Contact {    Name = "Utagawa Kunisada",
                                    Email = "UtagawaKunisada@nowhere.com",
                                    
                                    Comments =  "Testing 1, 2 3"};
        }
    }
}
