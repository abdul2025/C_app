using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using consoleApp.Interfaces;


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


        public async Task<GitHubUserDto?> GetGitHubUsers(string username, CancellationToken token)
        {
            try
            {
                return await _userRepository.GetUserFromGitHubAsync(username, token);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error while fetching Users", ex, new { username });
                // no throw stopping the function needed here 
                // throw;
                return null;
            }
        }


        public async Task<IEnumerable<GitHubUserDto>> AsyncTaskServiceForUsers(IEnumerable<Task<GitHubUserDto?>> tasks)
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

        public IEnumerable<GitHubUserDto> FilterUsersByNumOfFollowers(IEnumerable<GitHubUserDto> users, int numberOfFollowers)
        {
            return users.Where(user => user.Followers > numberOfFollowers);
        } 


    }
}