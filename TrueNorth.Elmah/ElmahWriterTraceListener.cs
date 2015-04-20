using Elmah;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TrueNorth.Elmah.TraceListener
{
    public class ElmahWriterTraceListener : System.Diagnostics.TraceListener
    {
        public static void Register()
        {
            System.Diagnostics.Trace.Listeners.Add(new ElmahWriterTraceListener());
        }
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            TraceEvent(eventCache, source, eventType, id, 
                    string.Join(",",data.Select( (s) => s.ToString())));
        }
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            TraceEvent(eventCache, source, eventType, id, data.ToString());
        }
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            TraceEvent(eventCache, source, eventType, id, string.Format(format, args));
        }
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message) //
        {
            trace(eventType, message);
        }

        private static void trace(TraceEventType eventType, string message)
        {
            Exception exception;
            switch (eventType)
            {
                case TraceEventType.Information:
                    exception = new TraceInformation(message);
                    break;
                case TraceEventType.Error:
                    exception = new TraceError(message);
                    break;
                case TraceEventType.Warning:
                    exception = new TraceWarning(message);
                    break;
                default:
                    exception = new TraceWrite(message);
                    break;
            }

            Trace(exception);
        }

        private static void Trace(Exception exception)
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null)
            {
                ErrorLog.GetDefault(null).Log(new Error(exception));
            }
            else
            {
                ErrorSignal.FromCurrentContext().Raise(exception);
            }
        }

        public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
        {
            base.TraceTransfer(eventCache, source, id, message, relatedActivityId);
        }
        public override void Write(string message)
        {
            Trace(new TraceWrite(message));
        }
        public override void WriteLine(string message)
        {
            Trace(new TraceWrite(message));
        }
    }
}
