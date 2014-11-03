using System.Web.Security;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using InverGrove.Domain.Services;
using InverGrove.Domain.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InverGrove.Tests.Domain.Services
{
    [TestClass]
    public class RegistrationServiceTests
    {
        private Mock<IMembershipService> membershipService;
        private Mock<IPersonService> personService;
        private Mock<IProfileService> profileService;
        private RegistrationService registrationService;

        [TestInitialize]
        public void SetUp()
        {
            this.membershipService = new Mock<IMembershipService>();
            this.personService = new Mock<IPersonService>();
            this.profileService = new Mock<IProfileService>();
            this.registrationService = new RegistrationService(this.membershipService.Object, this.personService.Object, this.profileService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ParameterNullException))]
        public void RegisterUser_Should_Throw_If_RegisterViewModel_Is_Null()
        {
            this.registrationService.RegisterUser(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ParameterNullException))]
        public void RegisterUser_Should_Throw_If_RegisterViewModel_Person_Is_Null()
        {
            this.registrationService.RegisterUser(new Register());
        }

        [TestMethod]
        public void RegisterUser_Should_Call_CreateMembershipUser_On_MembershipService()
        {
            this.membershipService.Setup(
                m => m.CreateMembershipUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>(), false, MembershipPasswordFormat.Hashed)).Returns(new InverGrove.Domain.Models.Membership { MembershipId = 0, UserId = 0 });

            this.registrationService.RegisterUser(new Register { Person = new Person() });

            this.membershipService.Verify();
        }

        [TestMethod]
        public void RegisterUser_Should_Call_AddPerson_On_PersonService()
        {
            this.membershipService.Setup(
                m => m.CreateMembershipUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>(), false, MembershipPasswordFormat.Hashed)).Returns(new InverGrove.Domain.Models.Membership { MembershipId = 4, UserId = 1 });

            this.registrationService.RegisterUser(new Register { Person = new Person() });

            this.personService.Verify(p => p.AddPerson(It.IsAny<IPerson>()));
        }

        [TestMethod]
        public void RegisterUser_Should_Call_AddProfile_On_ProfileService()
        {
            this.membershipService.Setup(
                m => m.CreateMembershipUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>(), false, MembershipPasswordFormat.Hashed)).Returns(new InverGrove.Domain.Models.Membership { MembershipId = 4, UserId = 1 });

            this.personService.Setup(p => p.AddPerson(It.IsAny<IPerson>())).Returns(3);

            this.registrationService.RegisterUser(new Register { Person = new Person() });

            this.profileService.Verify(p => p.AddProfile(It.IsAny<int>(), It.IsAny<int>(), false, false, false, true));
        }
    }
}