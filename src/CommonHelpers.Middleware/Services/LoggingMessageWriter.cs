using CommonHelpers.Middleware.Interfaces;

namespace CommonHelpers.Middleware.Services;

public class LoggingMessageWriter : IMessageWriter
{
    private readonly ILogger<LoggingMessageWriter> logger;

    public LoggingMessageWriter(ILogger<LoggingMessageWriter> logger)
    {
        this.logger = logger;
    }

    public void Write(string message)
    {
        logger.LogInformation(message);
    }

    public void Write(string message, params object[] extraParameters)
    {
        logger.LogInformation(message, extraParameters);
    }
}