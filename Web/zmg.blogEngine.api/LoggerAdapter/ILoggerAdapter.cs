using Microsoft.Extensions.Logging;
using System;

namespace zmg.blogEngine.api.LoggerAdapter
{
    public interface ILoggerAdapter<T>
    {
        // add just the logger methods your app uses
        void LogInformation(string message, params object[] args);
        void LogError(Exception ex, string message, params object[] args);
        void LogDebug(string message, params object[] args);
    }

    public class LoggerAdapter<T> : ILoggerAdapter<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerAdapter(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogError(Exception ex, string message, params object[] args)
        {
            _logger.LogError(ex, DateTime.UtcNow.ToString() + message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(DateTime.UtcNow.ToString() + message, args);
        }

        public void LogDebug(string message, params object[] args)
        {
            _logger.LogDebug(DateTime.UtcNow.ToString() + message, args);
        }
    }
}
