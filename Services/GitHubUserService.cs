using consoleApp.Extensions;
using consoleApp.Interfaces;



namespace consoleApp.Services
{
    public class GitHubUserService : IGitHubUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger _logger;
        private readonly IOutputWriter _outputWriter;


        public GitHubUserService(IUserRepository userRepository, IAppLogger logger, IOutputWriter outputWriter)
        {
            _userRepository = userRepository;
            _logger = logger;
            _outputWriter = outputWriter;
        }


        public async Task ProcessGitHubUsers(List<string> username, CancellationToken token)
        {
            var tasks = username
            .Select(user => GetGitHubUsers(user, token));


            var collectedUsers = await AsyncTaskServiceForUsers(tasks);

            // var users = collectedUsers.GetHireable();
            // var users = collectedUsers.FilterByFollowers(1000);

            var users = collectedUsers.GetHireable().FilterByFollowers(1000);


            _outputWriter.ShowData(users);
        }


        public async Task<GitHubUserDto?> GetGitHubUsers(string username, CancellationToken token)
        {
            try
            {
                return await _userRepository.GetUserFromGitHubAsync(username, token);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error while fetching Users", ex, new { username });
                // no throw stopping the function required here 
                // throw;
                return null;
            }
        }

        // WhenAll: Waiting for all task to finish and shaping out filtered data , another as WhenAny finish for streaming.
        public async Task<IEnumerable<GitHubUserDto>> AsyncTaskServiceForUsers(IEnumerable<Task<GitHubUserDto?>> tasks)
        {
            return (await Task.WhenAll(tasks))
                .Where(u => u != null)
                .Select(u => u!)
                .ToList();
        }

    }
}