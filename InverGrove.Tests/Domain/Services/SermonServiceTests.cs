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
        [ExpectedException(typeof(ParameterNullException))]
        public void AddSermon_Should_Throw_If_Sermon_Is_Null()
        {
            this.sermonService.AddSermon(null);
        }

        [TestMethod]
        public void AddSermon_Should_Call_Add_On_SermonRepository_When_Sermon_Is_Not_Null()
        {
            var sermon = new Sermon
            {
                SermonDate = DateTime.Now,
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
        [ExpectedException(typeof(ParameterOutOfRangeException))]
        public void DeleteSermon_Should_Throw_When_SermonId_Is_Less_Than_Or_Equal_To_Zero()
        {
            this.sermonService.DeleteSermon(0);
        }

        [TestMethod]
        public void DeleteSermon_Should_Call_Delete_On_SermonRepository()
        {
            var sermonId = 1;

            this.sermonService.DeleteSermon(sermonId);

            this.sermonRepository.Verify(s => s.Delete(sermonId));
        }

        [TestMethod]
        public void GetSermon_Should_Call_GetById_On_SermonRepository()
        {
            this.sermonService.GetSermon(1);

            this.sermonRepository.Verify(s => s.GetById(It.IsAny<int>()));
        }

        [TestMethod]
        [ExpectedException(typeof(ParameterOutOfRangeException))]
        public void GetSermon_Should_Throw_When_SermonId_Is_Less_Than_Or_Equal_To_Zero()
        {
            this.sermonService.GetSermon(0);
        }

        [TestMethod]
        public void GetSermons_Should_Call_Get_On_SermonRepository()
        {
            this.sermonService.GetSermons();

            this.sermonRepository.Verify(s => s.Get(It.IsAny<Expression<Func<Data.Entities.Sermon, bool>>>(),
                It.IsAny<Func<IQueryable<Data.Entities.Sermon>, IOrderedQueryable<Data.Entities.Sermon>>>(), false, It.IsAny<string>()));
        }

        [TestMethod]
        [ExpectedException(typeof(ParameterNullException))]
        public void UpdateSermon_Should_Throw_When_Sermon_Is_Null()
        {
            this.sermonService.UpdateSermon(null);
        }

        [TestMethod]
        public void UpdateSermon_Should_Call_Update_On_SermonRepostiroy()
        {
            var sermon = new Sermon
            {
                SermonDate = DateTime.Now,
                SermonId = 1,
                SoundCloudId = 12345,
                Tags = "Tag1,Tag2",
                Title = "Sermon Test"
            };

            this.sermonService.UpdateSermon(sermon);

            this.sermonRepository.Verify(s => s.Update(sermon));
        }

        [TestMethod]
        public void UpdateSermon_Should_Return_False_When_Update_On_SermonRepository_Throws_Exception()
        {
            this.sermonRepository.Setup(s => s.Update(It.IsAny<ISermon>())).Throws(new ApplicationException());

            var result = this.sermonService.UpdateSermon(new Sermon());

            Assert.IsFalse(result);
        }
    }
}