
using consoleApp.interfaces;
using consoleApp.ExternalClients;






namespace consoleApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GitHubApiClient _apiService;
        private static readonly SemaphoreSlim _semaphore = new(5); // we use SemaphoreSlim to handle concurrent operation with only 5 run together to avoid overwhelming or throttling the API


        public UserRepository(GitHubApiClient apiService)
        {
            _apiService = apiService;
        }

        public async Task<GitHubUserDto?> GetUserFromGitHubAsync(string urlPath)
        {
            await _semaphore.WaitAsync();
            try
            {
                return await _apiService.GetReq<GitHubUserDto>(urlPath);
            }
            finally
            {
                _semaphore.Release();
            }
        }


        public async Task<List<GitHubUserRepoDto>> GetUserRepoFromGitHubAsync(string urlPath)
        {
            await _semaphore.WaitAsync();
            try
            {
                return await _apiService.GetReq<List<GitHubUserRepoDto>>(urlPath);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}