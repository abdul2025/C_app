using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using consoleApp;


namespace consoleApp
{
    public class ConsoleLogger : IAppLogger
    {

        public void LogInfo(string message, object? context = null)
        => Write("INFO", message, context);

        public void LogWarning(string message, object? context = null)
            => Write("WARN", message, context);

        public void LogError(string message, Exception ex, object? context = null)
            => Write("ERROR", $"{message} | {ex.Message}", context);

        private void Write(string level, string message, object? context)
        {
            var log = new
            {
                Timestamp = DateTime.UtcNow,
                Level = level,
                Message = message,
                Context = context
            };

            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(log));
        }
        
    }
}