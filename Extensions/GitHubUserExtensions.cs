

namespace consoleApp.Extensions
{
    public static class GitHubUserExtensions
    {
        public static IEnumerable<GitHubUserDto> GetHireable(this IEnumerable<GitHubUserDto> users)
        => users.Where(u => u.Hireable == true);

        public static IEnumerable<GitHubUserDto> FilterByFollowers(this IEnumerable<GitHubUserDto> users, int minFollowers)
            => users.Where(u => u.Followers > minFollowers);
    }
    
}
