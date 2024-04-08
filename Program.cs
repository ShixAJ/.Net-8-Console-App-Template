using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.EventLog;
using System.Diagnostics;
using System.Runtime.InteropServices;

// change sourcename value
const string EVENTSOURCE_SOURCENAME = "SourceNameExample";
const string EVENTSOURCE_LOGNAME = "Application";

bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfigurationRoot configuration = configBuilder.Build();

#pragma warning disable CA1416 // Validate platform compatibility
if (isWindows && !EventLog.SourceExists(EVENTSOURCE_SOURCENAME))
{
    EventLog.CreateEventSource(EVENTSOURCE_SOURCENAME, EVENTSOURCE_LOGNAME);
}

ILoggerFactory loggingFactory = LoggerFactory.Create(builder =>
{
    LogLevel defaultLogLevel = LogLevel.Information;
    Enum.TryParse(configuration["Logging:LogLevel:Default"], out defaultLogLevel);
    builder.SetMinimumLevel(defaultLogLevel);

    builder.AddConsole();
    LogLevel consoleLogLevel = defaultLogLevel;
    if (!Enum.TryParse(configuration["Logging:Console:LogLevel:Default"], out consoleLogLevel))
    {
        consoleLogLevel = defaultLogLevel;
    }
    builder.AddFilter<ConsoleLoggerProvider>(level => level >= consoleLogLevel);

    if (isWindows)
    {
        builder.AddEventLog(new EventLogSettings()
        {
            LogName = EVENTSOURCE_LOGNAME,
            SourceName = EVENTSOURCE_SOURCENAME
        });
        LogLevel eventLogLevel = defaultLogLevel;
        if (!Enum.TryParse(configuration["Logging:EventLog:LogLevel:Default"], out eventLogLevel))
        {
            eventLogLevel = defaultLogLevel;
        }
        builder.AddFilter<EventLogLoggerProvider>(level => level >= eventLogLevel);
    }
});
#pragma warning restore CA1416 // Validate platform compatibility

ILogger logger = loggingFactory.CreateLogger("Program");

// Main Code
