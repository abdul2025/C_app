using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using consoleApp.interfaces;


namespace consoleApp.Services
{
    public class UserService : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger _logger;



        public UserService(IUserRepository userRepository, IAppLogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }


        public async Task<GitHubUserDto?> GetGitHubUsers(string username)
        {
            try
            {
                return await _userRepository.GetUserFromGitHubAsync(username);

            }

            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                _logger.LogError("Timeout while fetching repos", ex, new { username });
                return new GitHubUserDto();
            }
        }


        public async Task<IEnumerable<GitHubUserDto>> ExecuteParallelTaskForUsers(IEnumerable<Task<GitHubUserDto?>> tasks)
        {
            return (await Task.WhenAll(tasks))
                .Where(u => u != null)
                .Select(u => u!)
                .ToList();
        }

        public IEnumerable<GitHubUserDto> GetAllUser(IEnumerable<GitHubUserDto> users)
        {
            return users;
        }

        public IEnumerable<GitHubUserDto> GetHireAblUsers(IEnumerable<GitHubUserDto> users)
        {
            return users.Where(user => user.Hireable == true);
        }

        public IEnumerable<GitHubUserDto> filterUsers(IEnumerable<GitHubUserDto> users, int numberOfFollowers)
        {
            return users.Where(user => user.Followers > numberOfFollowers);
        } 


    }
}