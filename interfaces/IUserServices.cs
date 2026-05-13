

namespace consoleApp.Interfaces
{
    public interface IUserServices
    {

        // Tasks
        Task<GitHubUserDto?> GetGitHubUsers(string username, CancellationToken token);
        Task<IEnumerable<GitHubUserDto>> AsyncTaskServiceForUsers(IEnumerable<Task<GitHubUserDto?>> tasks);


        // Data Processing
        IEnumerable<GitHubUserDto> GetAllUser(IEnumerable<GitHubUserDto> users);
        IEnumerable<GitHubUserDto> GetHireAblUsers(IEnumerable<GitHubUserDto> users);
        IEnumerable<GitHubUserDto> FilterUsersByNumOfFollowers(IEnumerable<GitHubUserDto> users, int numberOfFollowers);

    }
}