using System.Text.Json.Serialization;
namespace consoleApp
{

    public class GitHubUserDto
    {

        public string? Login { get; set; }

        public int Id { get; set; }

        [JsonPropertyName("node_id")]
        public string? NodeId { get; set; }

        [JsonPropertyName("avatar_url")]
        public string? AvatarUrl { get; set; }

        [JsonPropertyName("gravatar_id")]
        public string? GravatarId { get; set; }

        public string? Url { get; set; }

        [JsonPropertyName("html_url")]
        public string? HtmlUrl { get; set; }

        [JsonPropertyName("followers_url")]
        public string? FollowersUrl { get; set; }

        [JsonPropertyName("following_url")]
        public string? FollowingUrl { get; set; }

        [JsonPropertyName("gists_url")]
        public string? GistsUrl { get; set; }

        [JsonPropertyName("starred_url")]
        public string? StarredUrl { get; set; }

        [JsonPropertyName("subscriptions_url")]
        public string? SubscriptionsUrl { get; set; }

        [JsonPropertyName("organizations_url")]
        public string? OrganizationsUrl { get; set; }

        [JsonPropertyName("repos_url")]
        public string? ReposUrl { get; set; }

        [JsonPropertyName("events_url")]
        public string? EventsUrl { get; set; }

        [JsonPropertyName("received_events_url")]
        public string? ReceivedEventsUrl { get; set; }

        public string? Type { get; set; }

        [JsonPropertyName("user_view_type")]
        public string? UserViewType { get; set; }

        [JsonPropertyName("site_admin")]
        public bool SiteAdmin { get; set; }

        public string? Name { get; set; }

        public string? Company { get; set; }

        public string? Blog { get; set; }

        public string? Location { get; set; }

        public string? Email { get; set; }

        public bool? Hireable { get; set; }

        public string? Bio { get; set; }

        [JsonPropertyName("twitter_username")]
        public string? TwitterUsername { get; set; }

        [JsonPropertyName("public_repos")]
        public int PublicRepos { get; set; }

        [JsonPropertyName("public_gists")]
        public int PublicGists { get; set; }

        public int Followers { get; set; }

        public int Following { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}