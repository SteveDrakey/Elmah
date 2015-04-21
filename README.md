# Elmah Tracer [![Build status](https://ci.appveyor.com/api/projects/status/a4pyokl9b3mq0v83?svg=true)](https://ci.appveyor.com/project/SteveDrakey/elmah)

A simple TraceListener for use with Elmah.

## Install

    Install-Package TrueNorth.Elmah 

### To Use

    ElmahWriterTraceListener.Register();

### And then

    using System.Diagnostics

    Trace.TraceError("Your error message");
    Trace.TraceInformation("Or maybe some info");
    Trace.TraceWarning("Or a warning");

### You can also

    Trace.Write("Hello");  // Will work the same as WriteLine
    Trace.WriteLine("World"); 

See http://truenorthit.co.uk/2015/04/17/trace-listener-for-elmah-asp-mvc-exception-logger/ for more details
