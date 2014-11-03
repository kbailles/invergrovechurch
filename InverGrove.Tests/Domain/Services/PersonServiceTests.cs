using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using InverGrove.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InverGrove.Tests.Domain.Services
{
    [TestClass]
    public class PersonServiceTests
    {
        private Mock<IPersonRepository> personRepository;
        private PersonService personService;

        [TestInitialize]
        public void SetUp()
        {
            this.personRepository = new Mock<IPersonRepository>();
            this.personService = new PersonService(this.personRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ParameterNullException))]
        public void AddPerson_Should_Throw_When_Person_Is_Null()
        {
            this.personService.AddPerson(null);
        }

        [TestMethod]
        public void AddPerson_Should_Call_Add_On_PersonRepository()
        {
            this.personService.AddPerson(new Person());

            this.personRepository.Verify(p => p.Add(It.IsAny<IPerson>()));
        }
    }
}