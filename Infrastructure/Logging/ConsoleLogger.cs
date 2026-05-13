
using System.Text.Json;
using consoleApp.Interfaces;


namespace consoleApp.Infrastructure
{
    public class ConsoleLogger : IAppLogger
    {

        public void LogInfo(string message, object? context = null)
        {
            WriteLog("INFO", message, null, context);
        }

        public void LogWarning(string message, object? context = null)
        {
            WriteLog("WARNING", message, null, context);
        }

        public void LogError(string message, Exception ex, object? context = null)
        {
            WriteLog("ERROR", message, ex, context);
        }


        private void WriteLog(
            string level,
            string message,
            Exception? ex = null,
            object? context = null)
        {
            var log = new
            {
                Timestamp = DateTime.UtcNow,
                Level = level,
                Message = message,
                Exception = ex?.Message,
                StackTrace = ex?.StackTrace,
                Context = context
            };

            Console.WriteLine(
                JsonSerializer.Serialize(log, new JsonSerializerOptions
                {
                    WriteIndented = true
                }));
        }
    }
        
}