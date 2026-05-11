using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consoleApp
{
    public interface IAppLogger
    {
        void LogInfo(string message, object? context = null);
        void LogWarning(string message, object? context = null);
        void LogError(string message, Exception ex, object? context = null);
    }
}