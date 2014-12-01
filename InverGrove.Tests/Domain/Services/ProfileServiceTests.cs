using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InverGrove.Tests.Domain.Services
{
    [TestClass]
    public class ProfileServiceTests
    {
        private Mock<IProfileProvider> profileProvider;
        private Mock<ISessionStateService> sessionStateService;
        private Mock<IPersonRepository> personRepository;
        private Mock<IProfileRepository> profileRepository;
        private Mock<IUserRoleRepository> userRoleRepository;
        private ProfileService profileService;

        [TestInitialize]
        public void SetUp()
        {
            this.profileProvider = new Mock<IProfileProvider>();
            this.sessionStateService = new Mock<ISessionStateService>();
            this.personRepository = new Mock<IPersonRepository>();
            this.profileRepository = new Mock<IProfileRepository>();
            this.userRoleRepository = new Mock<IUserRoleRepository>();
            this.profileService = new ProfileService(this.profileProvider.Object, this.sessionStateService.Object, 
                this.personRepository.Object, this.profileRepository.Object, this.userRoleRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ParameterNullException))]
        public void AddPersonProfile_Should_Throw_When_Person_Is_Null()
        {
            this.profileService.AddPersonProfile(null, 5, true, true, true, true);
        }
    }
}