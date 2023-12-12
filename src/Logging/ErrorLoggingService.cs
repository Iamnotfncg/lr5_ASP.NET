using System.Text;

namespace pr5.src.Logging
{
    public class ErrorLoggingService : IMiddleware
    {
        private readonly ILogger<ErrorLoggingService> _logger;
        private readonly string _logFilePath;

        public ErrorLoggingService(ILogger<ErrorLoggingService> logger, string logFilePath)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logFilePath = logFilePath ?? throw new ArgumentNullException(nameof(logFilePath));

            if (!File.Exists(_logFilePath))
                File.Create(_logFilePath).Close();
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void LogException(Exception ex)
        {
            try
            {
                string logMessage = $"[{DateTime.Now}] Exception: {ex.Message}\nStackTrace: {ex.StackTrace}\n\n";
                File.AppendAllText(_logFilePath, logMessage, Encoding.UTF8);
            }
            catch (Exception logEx)
            {
                _logger.LogError(logEx, "Failed to log exception");
            }
        }
    }
}