using System.Linq.Expressions;
using System.Web.Security;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InverGrove.Tests.Domain.Services
{
    [TestClass]
    public class MembershipServiceTests
    {
        private Mock<IMembershipFactory> membershipFactory;
        private Mock<IMembershipRepository> membershipRepository;
        private MembershipService membershipService;

        [TestInitialize]
        public void SetUp()
        {
            this.membershipFactory = new Mock<IMembershipFactory>();
            this.membershipRepository = new Mock<IMembershipRepository>();
            this.membershipService = new MembershipService(this.membershipFactory.Object, this.membershipRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ParameterNullException))]
        public void CreateMembershipUser_Should_Throw_When_UserName_Is_Null_Or_Empty()
        {
            this.membershipService.CreateMembershipUser(null, "password", "emailAddress", "passwordQuestion", 
                "passwordAnswer", true, MembershipPasswordFormat.Hashed);
        }
    }
}