using System.Text.Json;
using consoleApp.interfaces;

namespace consoleApp.Infrastructure
{
    public class ConsoleWriter : IOutputWriter
    {
        private readonly IAppLogger _logger;

        public ConsoleWriter(IAppLogger logger)
        {
            _logger = logger;
        }
        public void ShowData<T>(IEnumerable<T> data)
        {
            if (data == null)
            {
                _logger.LogWarning("No data.");
                return;
            }

            var list = data.ToList(); 

            _logger.LogInfo($"Count: {list.Count}");


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