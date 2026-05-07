using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consoleApp.interfaces
{
    public interface IGitRepoServices
    {
        Task<List<GitHubUserRepoDto>> GetGitHubUsersRepos(string username);
        IEnumerable<GitHubUserRepoDto> ShowAllRepos(IEnumerable<GitHubUserRepoDto> repos);

        IEnumerable<GitHubUserRepoDto> ShowPublicRepos(IEnumerable<GitHubUserRepoDto> repos);

        IEnumerable<GitHubUserRepoDto> ShowReposHasOpenIssuesOverFive(IEnumerable<GitHubUserRepoDto> repos, int numberOfIssues);
    }
}