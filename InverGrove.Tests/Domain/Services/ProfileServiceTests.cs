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
        private Mock<IProfileRepository> profileRepository;
        private ProfileService profileService;

        [TestInitialize]
        public void SetUp()
        {
            this.profileProvider = new Mock<IProfileProvider>();
            this.sessionStateService = new Mock<ISessionStateService>();
            this.profileRepository = new Mock<IProfileRepository>();
            this.profileService = new ProfileService(this.profileProvider.Object, this.sessionStateService.Object, this.profileRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ParameterNullException))]
        public void AddPersonProfile_Should_Throw_When_Person_Is_Null()
        {
            this.profileService.AddPersonProfile(null, 5, true, true, true, true);
        }
    }
}