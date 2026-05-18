using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using consoleApp.Interfaces;
using System.Text.Json;
using consoleApp.Extensions;




namespace consoleApp.Services
{
    public class GitRepoServices : IGitRepoServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger _logger;
        private readonly IOutputWriter _outputWriter;




        public GitRepoServices(IUserRepository userRepository, IAppLogger logger, IOutputWriter outputWriter)
        {
            _userRepository = userRepository;
            _logger = logger;
            _outputWriter = outputWriter;

        }


        public async Task ProcessGitHubUsersRepos(List<string> username, CancellationToken token)
        {
            var tasksRepo = username.Select(user =>  GetGitHubUsersRepos(user, token));

            var collectedUsersRepo = await AsyncTaskServiceForRepos(tasksRepo);

            // var repos = collectedUsersRepo.GetPublicRepos();
            // var repos = collectedUsersRepo.GetReposHasOpenIssuesOverFive(5);
            var repos = collectedUsersRepo.GetPublicRepos().GetReposHasOpenIssuesOverFive(5);

            _outputWriter.ShowData(repos);
        }



        public async Task<List<GitHubUserRepoDto>?>GetGitHubUsersRepos(string username, CancellationToken token)
        {
            try
            {

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
                return null;
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

    }
}