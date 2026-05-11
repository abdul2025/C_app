using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using consoleApp.interfaces;
using System.Text.Json;



namespace consoleApp.Services
{
    public class GitRepoServices : IGitRepoServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger _logger;



        public GitRepoServices(IUserRepository userRepository, IAppLogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;

        }




        public async Task<List<GitHubUserRepoDto>>GetGitHubUsersRepos(string username)
        {
            try
            {
                _logger.LogInfo("Fetching repos", new { username });

                return await _userRepository.GetUserRepoFromGitHubAsync($"{username}/repos");

            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                _logger.LogError("Timeout while fetching repos", ex, new { username });
                return new List<GitHubUserRepoDto>();
            }

        }

            public async Task<IEnumerable<GitHubUserRepoDto>> ExecuteParallelTaskForRepos(IEnumerable<Task<List<GitHubUserRepoDto>>> tasks)
            {
                var results = await Task.WhenAll(tasks);

                return results
                    .Where(list => list != null) // discard the Null list of list 
                    .SelectMany(list => list) // Flat list as All Users's Repos are in ONE list (To simulate DB relation later on (Relation in join, grouping, Sorting, UNION ...etc ))
                    .ToList();
            }





        public IEnumerable<GitHubUserRepoDto> GetAllRepos(IEnumerable<GitHubUserRepoDto> repos)
        {
            return repos;
        }

        public IEnumerable<GitHubUserRepoDto> GetPublicRepos(IEnumerable<GitHubUserRepoDto> repos)
        {
            return repos.Where(repo => repo.Visibility == "public");

        }

        public IEnumerable<GitHubUserRepoDto> GetReposHasOpenIssuesOverFive(IEnumerable<GitHubUserRepoDto> repos, int numberOfIssues)
        {
            return repos.Where(repo => repo.OpenIssues >= numberOfIssues);
        }

        
        
    }
}