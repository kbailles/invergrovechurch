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
        private Mock<IEmailService> emailService;
        private Mock<IMembershipService> membershipService;
        private Mock<IProfileService> profileService;
        private Mock<IMaritalStatusRepository> maritalStatusRepository;
        private Mock<IPersonTypeRepository> personTypeRepository;
        private Mock<IRoleRepository> roleRepository;
        private Mock<IUserRoleRepository> userRoleRepository;

        private RegistrationService registrationService;

        [TestInitialize]
        public void SetUp()
        {
            this.emailService = new Mock<IEmailService>();
            this.membershipService = new Mock<IMembershipService>();
            this.profileService = new Mock<IProfileService>();
            this.maritalStatusRepository = new Mock<IMaritalStatusRepository>();
            this.personTypeRepository = new Mock<IPersonTypeRepository>();
            this.roleRepository = new Mock<IRoleRepository>();
            this.userRoleRepository =new Mock<IUserRoleRepository>();

            this.registrationService = new RegistrationService(this.emailService.Object, this.membershipService.Object, this.profileService.Object,
                this.maritalStatusRepository.Object, this.personTypeRepository.Object, this.roleRepository.Object, this.userRoleRepository.Object);
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
                    It.IsAny<string>(), It.IsAny<string>(), false, MembershipPasswordFormat.Hashed)).Returns(
                    new InverGrove.Domain.Models.Membership { MembershipId = 0, UserId = 0 });

            this.registrationService.RegisterUser(new Register { Person = new Person() });

            this.membershipService.Verify();
        }

        [TestMethod]
        public void RegisterUser_Should_Call_AddProfile_On_ProfileService()
        {
            var newMembership = new InverGrove.Domain.Models.Membership {MembershipId = 4, UserId = 1};
            var newUser = new Register {Person = new Person(), RoleId = 2};
            this.membershipService.Setup(
                m => m.CreateMembershipUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>(), false, MembershipPasswordFormat.Hashed)).Returns(newMembership);
            this.userRoleRepository.Setup(u => u.AddUserToRole(newMembership.UserId, newUser.RoleId));
            this.emailService.Setup(e => e.SendNewUserEmail(It.IsAny<IRegister>()));

            this.registrationService.RegisterUser(newUser);

            this.profileService.Verify(p => p.AddPersonProfile(It.IsAny<IPerson>(), It.IsAny<int>(), false, false, false, true));
        }

        [TestMethod]
        public void RegisterUser_Should_Call_AddUserToRole_On_UserRoleRepository()
        {
            var newMembership = new InverGrove.Domain.Models.Membership { MembershipId = 4, UserId = 1 };
            var newUser = new Register { Person = new Person(), RoleId = 2 };
            this.membershipService.Setup(
                m => m.CreateMembershipUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>(), false, MembershipPasswordFormat.Hashed)).Returns(newMembership);
            this.profileService.Setup(p => p.AddPersonProfile(It.IsAny<IPerson>(), It.IsAny<int>(), false, false, false, true)).Returns(true);
            this.userRoleRepository.Setup(u => u.AddUserToRole(newMembership.UserId, newUser.RoleId));
            this.emailService.Setup(e => e.SendNewUserEmail(It.IsAny<IRegister>()));

            this.registrationService.RegisterUser(newUser);

            this.userRoleRepository.VerifyAll();
        }

        [TestMethod]
        public void RegisterUser_Should_Call_SendNewUserEmail_On_EmailService()
        {
            var newMembership = new InverGrove.Domain.Models.Membership { MembershipId = 4, UserId = 1 };
            var newUser = new Register { Person = new Person(), RoleId = 2 };
            this.membershipService.Setup(
                m => m.CreateMembershipUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>(), false, MembershipPasswordFormat.Hashed)).Returns(newMembership);
            this.profileService.Setup(p => p.AddPersonProfile(It.IsAny<IPerson>(), It.IsAny<int>(), false, false, false, true)).Returns(true);
            this.userRoleRepository.Setup(u => u.AddUserToRole(newMembership.UserId, newUser.RoleId));
            this.emailService.Setup(e => e.SendNewUserEmail(It.IsAny<IRegister>()));

            this.registrationService.RegisterUser(newUser);

            this.emailService.VerifyAll();
        }
    }
}