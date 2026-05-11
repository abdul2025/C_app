using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consoleApp.interfaces
{
    public interface IGitRepoServices
    {
        Task<List<GitHubUserRepoDto>> GetGitHubUsersRepos(string username);

        Task<IEnumerable<GitHubUserRepoDto>> ExecuteParallelTaskForRepos(IEnumerable<Task<List<GitHubUserRepoDto>>> tasks);
        IEnumerable<GitHubUserRepoDto> GetAllRepos(IEnumerable<GitHubUserRepoDto> repos);

        IEnumerable<GitHubUserRepoDto> GetPublicRepos(IEnumerable<GitHubUserRepoDto> repos);

        IEnumerable<GitHubUserRepoDto> GetReposHasOpenIssuesOverFive(IEnumerable<GitHubUserRepoDto> repos, int numberOfIssues);



    }
}