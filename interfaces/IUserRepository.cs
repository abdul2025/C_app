
namespace consoleApp.Interfaces
{
    public interface IUserRepository
    {
        Task<GitHubUserDto?> GetUserFromGitHubAsync(string urlPath, CancellationToken token);
        Task<List<GitHubUserRepoDto>> GetUserRepoFromGitHubAsync(string urlPath, CancellationToken token);        
    }
}