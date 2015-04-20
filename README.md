# Elmah Tracer

A simple TraceListener for use with Elmah.

# To Use

ElmahWriterTraceListener.Register();

And then

    using System.Diagnostics

    Trace.TraceError("Your error message");
    Trace.TraceInformation("Or maybe some info");
    Trace.TraceWarning("Or a warning");

You can also

	Trace.Write("Hello");  // Will work the same as WriteLine
    Trace.WriteLine("World"); 





 