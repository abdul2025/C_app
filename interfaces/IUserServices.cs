using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consoleApp.interfaces
{
    public interface IUserServices
    {
        Task<GitHubUserDto?> GetGitHubUsers(string username);

        IEnumerable<GitHubUserDto>  ShowAllUser(IEnumerable<GitHubUserDto> users);
        IEnumerable<GitHubUserDto>  GetHireAblUsers(IEnumerable<GitHubUserDto> users);
        IEnumerable<GitHubUserDto> filterUsers(IEnumerable<GitHubUserDto> users, int numberOfFollowers);

        
        Task<List<GitHubUserRepoDto>> GetGitHubUsersRepos(string username);
        Task<List<GitHubUserRepoDto>> ShowAllRepos(Task<List<GitHubUserRepoDto>> repo);


    }
}