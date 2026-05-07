using consoleApp.ExternalClients;
using consoleApp.interfaces;
using System.Text.Json;

namespace consoleApp;

public class AppRunner
{
    private readonly IUserServices _userService;
    private readonly IGitRepoServices _gitRepoService;

    public AppRunner(IUserServices userService, IGitRepoServices gitRepoService)
    {
        _userService = userService;
        _gitRepoService = gitRepoService;
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
            "substack",
            "isaacs"
        };


        /*
            START Github User Service.
            START Github User Service.
            START Github User Service.
        */

        var tasks = listOfUsers
            .Select(user => _userService.GetGitHubUsers(user));


        var collectedUsers = await _userService.ExecuteParallelTaskForUsers(tasks);

        var users = _userService.ShowAllUser(collectedUsers);
        // var users = _userService.GetHireAblUsers(collectedUsers);
        // var users = _userService.filterUsers(collectedUsers, 1000);

        _userService.ShowData(users);

        /*
            END Github User Service.
            END Github User Service.
            END Github User Service.
        */

        /*
            ####################
            ####################
            ####################
            ####################
            ####################
        */

        /*
            START Github Repos Service.
            START Github Repos Service.
            START Github Repos Service.
        */


        var tasksRepo = listOfUsers
            .Select(user => _gitRepoService.GetGitHubUsersRepos(user));

        var collectedUsersRepo = (await Task.WhenAll(tasksRepo))
            .Where(u => u != null)
            .SelectMany(u => u!) // Flat list as All Users's Repos are in ONE list (To simulate DB relation later on (Relation in join, grouping, Sorting, UNION ...etc ))
            .ToList();

  

        var repos = _gitRepoService.ShowAllRepos(collectedUsersRepo);
        // var repos = _gitRepoService.ShowPublicRepos(collectedUsersRepo);
        // var repos = _gitRepoService.ShowReposHasOpenIssuesOverFive(collectedUsersRepo, 5);

        Console.WriteLine($"Number of Repo returned : {repos.Count()}");

        foreach (var repo in repos)
        {
            var json = JsonSerializer.Serialize(repo, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Console.WriteLine(json);            
        }


        /*
            END Github Repos Service.
            END Github Repos Service.
            END Github Repos Service.
        */

        /*
            ####################
            ####################
            ####################
            ####################
            ####################
        */


        /*
            START SQL Operation.
            START SQL Operation.
            START SQL Operation.
        */


        /*
            END SQL Operation.
            END SQL Operation.
            END SQL Operation.
        */

        


        
    }
}