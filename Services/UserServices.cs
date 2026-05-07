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


        public async Task<IEnumerable<GitHubUserDto>> ExecuteParallelTaskForUsers(IEnumerable<Task<GitHubUserDto?>> tasks)
        {
            return (await Task.WhenAll(tasks))
                .Where(u => u != null)
                .Select(u => u!)
                .ToList();
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



        public void ShowData(IEnumerable<GitHubUserDto> data)
        {
            Console.WriteLine($"Number of users returned : {data.Count()}");

            foreach (var user in data)
            {
                var json = JsonSerializer.Serialize(user, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                Console.WriteLine(json);            
            }
        }











        


    }
}