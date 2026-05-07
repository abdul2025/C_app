
namespace consoleApp.interfaces
{
    public interface IUserRepository
    {
        Task<GitHubUserDto?> GetUserFromGitHubAsync(string urlPath);
        Task<List<GitHubUserRepoDto>> GetUserRepoFromGitHubAsync(string urlPath);        
    }
}