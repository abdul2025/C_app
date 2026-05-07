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


        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<GitHubUserDto> ShowAllUser(IEnumerable<GitHubUserDto> users)
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





        public async Task<GitHubUserDto?> GetGitHubUsers(string username)
        {
            try
            {
                return await _userRepository.GetUserFromGitHubAsync(username);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR MESSAGE FROM API: {ex.Message}");
                return null;
            }
            

        }


        public async Task<List<GitHubUserRepoDto>>GetGitHubUsersRepos(string username)
        {
            try
            {
                return await _userRepository.GetUserRepoFromGitHubAsync($"{username}/repos");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR MESSAGE FROM API: {ex.Message}");
                return [];
            }
        }





        public Task<List<GitHubUserRepoDto>> ShowAllRepos(Task<List<GitHubUserRepoDto>> repo)
        {
            return repo;
        }
    }
}