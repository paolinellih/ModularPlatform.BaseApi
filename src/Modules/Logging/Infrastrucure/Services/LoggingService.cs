using Microsoft.Extensions.Logging;
using Modules.Logging.Application.Interfaces;

namespace Modules.Logging.Infrastrucure.Services;

public class LoggingService<T> : ILoggingService<T>
{
    private readonly ILogger<T> _logger;

    public LoggingService(ILogger<T> logger)
    {
        _logger = logger;
    }

    public void LogInfo(string message) => _logger.LogInformation(message);
    public void LogWarning(string message) => _logger.LogWarning(message);
    public void LogError(string message, Exception? ex = null) =>
        _logger.LogError(ex, message);
    public void LogDebug(string message) => _logger.LogDebug(message);
}