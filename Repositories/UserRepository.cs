
using consoleApp.interfaces;
using consoleApp.ExternalClients;






namespace consoleApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GitHubApiClient _apiService;

        public UserRepository(GitHubApiClient apiService)
        {
            _apiService = apiService;
        }

        public async Task<GitHubUserDto?> GetUserFromGitHubAsync(string urlPath)
        {
            return await _apiService.GetReq<GitHubUserDto>(urlPath);
        }


        public async Task<List<GitHubUserRepoDto>> GetUserRepoFromGitHubAsync(string urlPath)
        {
            return await _apiService.GetReq<List<GitHubUserRepoDto>>(urlPath);
        }
    }
}