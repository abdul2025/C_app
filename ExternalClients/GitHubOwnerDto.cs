using System.Text.Json.Serialization;


namespace consoleApp
{

public class GitHubOwnerDto
{
    [JsonPropertyName("login")]
    public string? Login { get; set; }

    [JsonPropertyName("html_url")]
    public string? HtmlUrl { get; set; }
}
}