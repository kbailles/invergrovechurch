using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Security;
using InverGrove.Data.Entities;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Services;
using InverGrove.Domain.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Person = InverGrove.Domain.Models.Person;

namespace InverGrove.Tests.Domain.Services
{
    [TestClass]
    public class RegistrationServiceTests
    {
        private Mock<IMembershipService> membershipService;
        private Mock<IProfileService> profileService;
        private Mock<IRoleRepository> roleRepository;
        private Mock<IUserRoleRepository> userRoleRepository;

        private RegistrationService registrationService;

        [TestInitialize]
        public void SetUp()
        {
            this.membershipService = new Mock<IMembershipService>();
            this.profileService = new Mock<IProfileService>();
            this.roleRepository = new Mock<IRoleRepository>();
            this.userRoleRepository =new Mock<IUserRoleRepository>();            

            this.registrationService = new RegistrationService(this.membershipService.Object, this.profileService.Object,
                 this.roleRepository.Object, this.userRoleRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ParameterNullException))]
        public void RegisterUser_Should_Throw_If_RegisterViewModel_Is_Null()
        {
            this.registrationService.RegisterUser(null);
        }

        [TestMethod]
        public void RegisterUser_Should_Call_CreateMembershipUser_On_MembershipService()
        {
            var newUser = this.GetTestRegister();

            this.userRoleRepository.Setup(u => u.Get(It.IsAny<Expression<Func<UserRole, bool>>>(),
                It.IsAny<Func<IQueryable<UserRole>, IOrderedQueryable<UserRole>>>(), It.IsAny<bool>(),
                It.IsAny<string>())).Returns(new List<UserRole>());
            this.membershipService.Setup(
                m => m.CreateMembershipUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>(), false, MembershipPasswordFormat.Hashed)).Returns(
                    new InverGrove.Domain.Models.Membership { MembershipId = 0, UserId = 0 });

            this.registrationService.RegisterUser(newUser);

            this.membershipService.Verify();
        }

        [TestMethod]
        public void RegisterUser_Should_Call_AddProfile_On_ProfileService()
        {
            var newMembership = new InverGrove.Domain.Models.Membership {MembershipId = 4, UserId = 1};
            var newUser = this.GetTestRegister();

            this.userRoleRepository.Setup(u => u.Get(It.IsAny<Expression<Func<UserRole, bool>>>(),
                It.IsAny<Func<IQueryable<UserRole>, IOrderedQueryable<UserRole>>>(), It.IsAny<bool>(),
                It.IsAny<string>())).Returns(new List<UserRole>());
            this.membershipService.Setup(
                m => m.CreateMembershipUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>(), false, MembershipPasswordFormat.Hashed)).Returns(newMembership);
            this.userRoleRepository.Setup(u => u.AddUserToRole(newMembership.UserId, newUser.RoleId));

            this.registrationService.RegisterUser(newUser);

            this.profileService.Verify(p => p.AddProfile(It.IsAny<int>(), It.IsAny<int>(), true, true, true));
        }

        [TestMethod]
        public void RegisterUser_Should_Call_AddUserToRole_On_UserRoleRepository()
        {
            var newMembership = new InverGrove.Domain.Models.Membership { MembershipId = 4, UserId = 1 };
            var newUser = this.GetTestRegister(); 

            this.userRoleRepository.Setup(u => u.Get(It.IsAny<Expression<Func<UserRole, bool>>>(),
                It.IsAny<Func<IQueryable<UserRole>, IOrderedQueryable<UserRole>>>(), It.IsAny<bool>(),
                It.IsAny<string>())).Returns(new List<UserRole>());
            this.membershipService.Setup(
                m => m.CreateMembershipUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>(), false, MembershipPasswordFormat.Hashed)).Returns(newMembership);
            this.profileService.Setup(p => p.AddProfile(It.IsAny<int>(), It.IsAny<int>(), false, false, true)).Returns(5);
            this.userRoleRepository.Setup(u => u.AddUserToRole(newMembership.UserId, newUser.RoleId));

            this.registrationService.RegisterUser(newUser);

            this.userRoleRepository.VerifyAll();
        }

        [TestMethod]
        public void RegisterUser_Should_Call_Get_On_UserRoleRepository()
        {
            var newUser = this.GetTestRegister();

            this.userRoleRepository.Setup(u => u.Get(It.IsAny<Expression<Func<UserRole, bool>>>(),
                It.IsAny<Func<IQueryable<UserRole>, IOrderedQueryable<UserRole>>>(), It.IsAny<bool>(),
                It.IsAny<string>())).Returns(new List<UserRole> 
                { 
                    new UserRole { User = new User{UserId = 1, UserName = "AlreadyHere" }
                }});
            
            this.registrationService.RegisterUser(newUser);

            this.userRoleRepository.VerifyAll();
        }

        private IRegister GetTestRegister()
        {
            return new Register
            {
                PersonId = 4,
                RoleId = 2,
                UserName = "AlreadyHere",
                Password = "password343",
                UserEmail = "user@email.com"
            };
        }
    }
}