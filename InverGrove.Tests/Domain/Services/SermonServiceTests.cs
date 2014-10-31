using System;
using System.Linq;
using System.Linq.Expressions;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using InverGrove.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InverGrove.Tests.Domain.Services
{
    [TestClass]
    public class SermonServiceTests
    {
        private SermonService sermonService;
        private Mock<ISermonRepository> sermonRepository;
        
        [TestInitialize]
        public void Setup()
        {
            this.sermonRepository = new Mock<ISermonRepository>();
            this.sermonService = new SermonService(this.sermonRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ParameterNullException), "A userId of null was inappropriately allowed.")]
        public void AddSermon_Should_Throw_If_Sermon_Is_Null()
        {
            this.sermonService.AddSermon(null);
        }

        [TestMethod]
        public void AddSermon_Should_Call_Add_On_SermonRepository_When_Sermon_Is_Not_Null()
        {
            var sermon = new Sermon
            {
                Date = DateTime.Now,
                SermonId = 1,
                SoundCloudId = 12345,
                Tags = "Tag1,Tag2",
                Title = "Sermon Test"
            };

            this.sermonRepository.Setup(s => s.Add(It.IsAny<ISermon>()));

            this.sermonService.AddSermon(sermon);

            this.sermonRepository.VerifyAll();
        }

        [TestMethod]
        public void GetSermons_Should_Call_Get_On_SermonRepository()
        {
            this.sermonService.GetSermons();

            this.sermonRepository.Verify(s => s.Get(It.IsAny<Expression<Func<Data.Entities.Sermon, bool>>>(), 
                It.IsAny<Func<IQueryable<Data.Entities.Sermon>, IOrderedQueryable<Data.Entities.Sermon>>>(), false, It.IsAny<string>()));
        }
    }
}