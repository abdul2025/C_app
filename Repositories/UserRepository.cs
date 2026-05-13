
using consoleApp.Interfaces;
using consoleApp.ExternalClients;


namespace consoleApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GitHubApiClient _apiService;
        private readonly SemaphoreSlim _semaphore;

        public UserRepository(GitHubApiClient apiService, SemaphoreSlim semaphore)
        {
            _apiService = apiService;
            _semaphore = semaphore;
        }

        public async Task<GitHubUserDto?> GetUserFromGitHubAsync(string urlPath, CancellationToken token)
        {
            await _semaphore.WaitAsync();
            try
            {
                return await _apiService.GetReq<GitHubUserDto>(urlPath, token);
            }
            finally
            {
                _semaphore.Release();
            }
        }


        public async Task<List<GitHubUserRepoDto>?> GetUserRepoFromGitHubAsync(string urlPath, CancellationToken token)
        {
            await _semaphore.WaitAsync();

            try
            {
                return await _apiService.GetReq<List<GitHubUserRepoDto>>(urlPath, token);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}