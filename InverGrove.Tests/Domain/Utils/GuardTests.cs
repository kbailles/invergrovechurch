using InverGrove.Domain.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InverGrove.Tests.Domain.Utils
{
    [TestClass]
    public class GuardTests
    {
        //[TestMethod]  this was just here to verify what the exception message was
        public void ParameterNotNullOrEmpty_Should_Throw_ParameterNullException_With_Calling_Method_Name()
        {
            this.TestThrow(null);
        }

        private void TestThrow(string testValue)
        {
            Guard.ParameterNotNullOrEmpty(testValue, "testValue");
        }
    }
}