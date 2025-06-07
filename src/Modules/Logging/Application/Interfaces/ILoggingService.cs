namespace Modules.Logging.Application.Interfaces;

public interface ILoggingService<T>
{
    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message, Exception? ex = null);
    void LogDebug(string message);
}