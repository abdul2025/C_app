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

        public void ShowData(IEnumerable<GitHubUserRepoDto> data)
        {
            Console.WriteLine($"Number of Repos returned : {data.Count()}");

            // foreach (var user in data)
            // {
            //     var json = JsonSerializer.Serialize(user, new JsonSerializerOptions
            //     {
            //         WriteIndented = true
            //     });

            //     Console.WriteLine(json);            
            // }
        }
        
    }
}