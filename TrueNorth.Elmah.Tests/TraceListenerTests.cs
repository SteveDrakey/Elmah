using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrueNorth.Elmah.TraceListener;
using Elmah;
using System.Linq;

namespace TrueNorth.Elmah.Tests
{
    /// <summary>
    /// Summary description for TraceListenerTests
    /// </summary>
    [TestClass]
    public class TraceListenerTests
    {
        public TraceListenerTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void RegisterAndTrace()
        {
            ElmahWriterTraceListener.Register();

            System.Diagnostics.Trace.TraceError("ErrorMessage {0},{1},{2}",1,2,3);
            System.Diagnostics.Trace.TraceInformation("TraceInfomation");
            System.Diagnostics.Trace.TraceWarning("TraceWarning");

            System.Diagnostics.Trace.Write("!Write!"); 
            System.Diagnostics.Trace.WriteLine("!WriteLine!"); 

            List<ErrorLogEntry> errors = new List<ErrorLogEntry>();
            var logs = ErrorLog.Default.GetErrors(0, 10, errors);

            Assert.AreEqual(5, logs);
            Assert.AreEqual("!WriteLine!", errors.First().Error.Message);
            Assert.AreEqual("ErrorMessage 1,2,3", errors.Last().Error.Message);

        }
    }
}
