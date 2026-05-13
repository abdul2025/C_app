using System.Text.Json;
using consoleApp.Interfaces;

namespace consoleApp.Infrastructure
{
    public class ConsoleWriter : IOutputWriter
    {
        private readonly IAppLogger _logger;

        public ConsoleWriter(IAppLogger logger)
        {
            _logger = logger;
        }
        public async void ShowData<T>(IEnumerable<T> data)
        {
            if (data == null)
            {
                _logger.LogWarning("No data.");
                return;
            }

            var list = data.ToList(); 

            _logger.LogInfo(
                "Collection Counted",
                new
                {
                    Count = list.Count,
                    Type = typeof(T).Name
                });

            foreach (var item in list)
            {
                var json = JsonSerializer.Serialize(item, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                Console.WriteLine(json);

            }
        }
    }
}