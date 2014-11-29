using InverGrove.Domain.Resources;
using InverGrove.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace InverGrove.Tests.Domain.Services
{
    [TestClass]
    public class LogServiceTests
    {
        private const string FakeMessage = "Buy Stocks!";
        private LogService logService;
        private string eventFired;

        [TestInitialize]
        public void SetUp()
        {
            this.logService = new LogService("", false);
        }


        [TestMethod]
        public void FatalLogMessageRecorded_Is_Raised_When_Fatal_Message_Is_Logged()
        {
            this.logService.FatalLogMessageRecorded += (p, e) => this.eventFired = "FatalLogMessageRecorded";
            this.logService.WriteToFatalLog("TestMethod");
            Assert.AreEqual("FatalLogMessageRecorded", this.eventFired);
        }


        [TestMethod]
        public void FatalLogMessageRecorded_Is_Raised_When_Fatal_Message_Is_Logged_Even_If_Message_Is_Null_Or_Empty()
        {
            string bogus = null;
            this.logService.FatalLogMessageRecorded += (p, e) => this.eventFired = e.Message;
            // ReSharper disable ExpressionIsAlwaysNull
            this.logService.WriteToFatalLog(bogus);
            // ReSharper restore ExpressionIsAlwaysNull
            Assert.IsTrue(this.eventFired.Contains(Messages.FatalLogMessageEmpty));
        }

        [TestMethod]
        public void ErrorLogMessageRecorded_Is_Raised_When_Error_Message_Is_Logged()
        {
            this.logService.ErrorLogMessageRecorded += (p, e) => this.eventFired = "ErrorLogMessageRecorded";
            this.logService.WriteToErrorLog("TestMethod");
            Assert.AreEqual("ErrorLogMessageRecorded", this.eventFired);
        }

        [TestMethod]
        public void WarnLogMessageRecorded_Is_Raised_When_Warning_Is_Logged()
        {
            this.logService.WarnLogMessageRecorded += (p, e) => this.eventFired = "WarnLogMessageRecorded";
            this.logService.WriteToWarnLog("TestMethod");
            Assert.AreEqual("WarnLogMessageRecorded", this.eventFired);
        }

        [TestMethod]
        public void InfoLogMessageRecorded_Is_Raised_When_Informational_Message_Is_Logged()
        {
            this.logService.InfoLogMessageRecorded += (p, e) => this.eventFired = "InfoLogMessageRecorded";
            this.logService.WriteToLog("TestMethod");
            Assert.AreEqual("InfoLogMessageRecorded", this.eventFired);
        }

        [TestMethod]
        public void WriteToDebugLog_With_Null_Exception_Object_Returns_False()
        {
            Exception e = null;
            // ReSharper disable ExpressionIsAlwaysNull
            bool actual = this.logService.WriteToDebugLog(e);
            // ReSharper restore ExpressionIsAlwaysNull
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void WriteToDebugLog_With_Valid_Exception_Object_Returns_True()
        {
            Exception e = new Exception(FakeMessage);
            bool actual = this.logService.WriteToDebugLog(e);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void WriteToDebugLog_With_Message_And_Params_And_Valid_Logger_And_Valid_Message_Returns_True()
        {
            object[] o = new object[0];
            bool actual = this.logService.WriteToDebugLog(FakeMessage, o);
            Assert.AreEqual(true, actual);
        }
    }
}
