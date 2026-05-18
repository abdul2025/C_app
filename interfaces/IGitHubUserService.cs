

namespace consoleApp.Interfaces
{
    public interface IGitHubUserService
    {

        // Tasks
        Task ProcessGitHubUsers(List<string> usernames, CancellationToken token);
        Task<GitHubUserDto?> GetGitHubUsers(string username, CancellationToken token);
        Task<IEnumerable<GitHubUserDto>> AsyncTaskServiceForUsers(IEnumerable<Task<GitHubUserDto?>> tasks);

    }
}