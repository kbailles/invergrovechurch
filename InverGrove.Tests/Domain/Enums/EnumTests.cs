using System;
using InverGrove.Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog.Targets;

namespace InverGrove.Tests.Domain.Enums
{
    [TestClass]
    public class EnumTests
    {
        [TestInitialize]
        public void SetUp()
        {
            // stubbed
        }
        [TestMethod]
        public void PhoneNumberType_ValueToKey()
        {
            var expected = "Mobile";
            var result = Enum.GetName(typeof(PhoneNumberType), 2);

            Assert.AreEqual(expected, result);
        }

    }
}
