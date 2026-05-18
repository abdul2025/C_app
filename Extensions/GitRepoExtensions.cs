

namespace consoleApp.Extensions
{
    public static class GitRepoExtensions
    {
        public static IEnumerable<GitHubUserRepoDto> GetPublicRepos(
            this IEnumerable<GitHubUserRepoDto> repos)
            => repos.Where(repo => repo.Visibility == "public");

        public static IEnumerable<GitHubUserRepoDto> GetReposHasOpenIssuesOverFive(
            this IEnumerable<GitHubUserRepoDto> repos, int numberOfIssues)
            => repos.Where(repo => repo.OpenIssues >= numberOfIssues);
    }
}





