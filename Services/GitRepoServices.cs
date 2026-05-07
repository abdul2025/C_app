using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using consoleApp.interfaces;


namespace consoleApp.Services
{
    public class GitRepoServices : IGitRepoServices
    {
        private readonly IUserRepository _userRepository;


        public GitRepoServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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





        public IEnumerable<GitHubUserRepoDto> ShowAllRepos(IEnumerable<GitHubUserRepoDto> repos)
        {
            return repos;
        }

        public IEnumerable<GitHubUserRepoDto> ShowPublicRepos(IEnumerable<GitHubUserRepoDto> repos)
        {
            return repos.Where(repo => repo.Visibility == "public");

        }

        public IEnumerable<GitHubUserRepoDto> ShowReposHasOpenIssuesOverFive(IEnumerable<GitHubUserRepoDto> repos, int numberOfIssues)
        {
            return repos.Where(repo => repo.OpenIssues >= numberOfIssues);
        }
        
    }
}