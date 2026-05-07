using consoleApp.ExternalClients;
using consoleApp.interfaces;
using System.Text.Json;

namespace consoleApp;

public class AppRunner
{
    private readonly IUserServices _userService;

    public AppRunner(IUserServices userService)
    {
        _userService = userService;
    }

    public async Task RunAsync()
    {
        var listOfUsers = new List<string>
        {
            // "torvalds",
            // "gaearon",
            // "yyx990803",
            // "tj",
            // "mojombo",
            // "defunkt",
            // "pjhyett",
            // "dhh",
            "getify",
            // "substack",
            // "isaacs"
        };

        var tasks = listOfUsers
            .Select(user => _userService.GetGitHubUsers(user));

        var collectedUsers = (await Task.WhenAll(tasks))
            .Where(u => u != null)
            .Select(u => u!);
        

        var users = _userService.ShowAllUser(collectedUsers);
        // var users = _userService.GetHireAblUsers(collectedUsers);
        // var users = _userService.filterUsers(collectedUsers, 1000);

        Console.WriteLine(users.Count());

        foreach (var user in users)
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            Console.WriteLine(json);            
        }



        var tasksRepo = listOfUsers
            .Select(user => _userService.GetGitHubUsersRepos(user));

        var collectedUsersRepo = (await Task.WhenAll(tasksRepo))
            .Where(u => u != null)
            .Select(u => u!);



        var repos = _userService.ShowAllRepos(collectedUsers);
        Console.WriteLine(repos.Count());

        foreach (var repo in repos)
        {
            var json = JsonSerializer.Serialize(repo, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            Console.WriteLine(json);            
        }


        
    }
}