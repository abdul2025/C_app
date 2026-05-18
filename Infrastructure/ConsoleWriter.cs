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
            
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            foreach (var item in list.Take(3))
            {
                var json = JsonSerializer.Serialize(item, options);

                Console.WriteLine(json);

            }
        }
    }
}