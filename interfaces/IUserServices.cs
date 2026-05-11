using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consoleApp.interfaces
{
    public interface IUserServices
    {

        // Tasks
        Task<GitHubUserDto?> GetGitHubUsers(string username);
        Task<IEnumerable<GitHubUserDto>> ExecuteParallelTaskForUsers(IEnumerable<Task<GitHubUserDto?>> tasks);


        // Data Processing
        IEnumerable<GitHubUserDto> GetAllUser(IEnumerable<GitHubUserDto> users);
        IEnumerable<GitHubUserDto> GetHireAblUsers(IEnumerable<GitHubUserDto> users);
        IEnumerable<GitHubUserDto> filterUsers(IEnumerable<GitHubUserDto> users, int numberOfFollowers);

        // Displaying Data function

    }
}