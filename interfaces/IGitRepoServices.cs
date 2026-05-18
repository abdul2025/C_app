using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consoleApp.Interfaces
{
    public interface IGitRepoServices
    {
        
        Task ProcessGitHubUsersRepos(List<string> username, CancellationToken token);
        Task<List<GitHubUserRepoDto>?> GetGitHubUsersRepos(string username, CancellationToken token);

        Task<IEnumerable<GitHubUserRepoDto>> AsyncTaskServiceForRepos(IEnumerable<Task<List<GitHubUserRepoDto>>> tasks);

    }
}