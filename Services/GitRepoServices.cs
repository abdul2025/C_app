using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using consoleApp.Interfaces;
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


        public async Task<List<GitHubUserRepoDto>>GetGitHubUsersRepos(string username, CancellationToken token)
        {
            try
            {
                _logger.LogInfo("Fetching repos", new { username });

                return await _userRepository.GetUserRepoFromGitHubAsync($"{username}/repos", token);

            }
            // Catch specific ERRORs as per what expecting from the above function ERRORS
            catch (Exception ex) when (
                    ex is HttpRequestException || ex is TaskCanceledException || ex is JsonException
                )
            {
                _logger.LogError("Error while fetching Repos", ex, new { username });
                // no throw to stopping the function needed here 
                // throw;
                return new List<GitHubUserRepoDto>();
            }

        }

            public async Task<IEnumerable<GitHubUserRepoDto>> AsyncTaskServiceForRepos(IEnumerable<Task<List<GitHubUserRepoDto>>> tasks)
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