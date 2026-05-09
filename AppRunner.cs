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
            "torvalds",
            "gaearon",
            "yyx990803",
            "tj",
            "mojombo",
            "defunkt",
            "pjhyett",
            "dhh",
            "getify",
            "substack",
            "isaacs"
        };


        /*
            START Github User Service.
            START Github User Service.
            START Github User Service.
        */

        // var tasks = listOfUsers
        //     .Select(user => _userService.GetGitHubUsers(user));


        // var collectedUsers = await _userService.ExecuteParallelTaskForUsers(tasks);

        // // var users = _userService.ShowAllUser(collectedUsers);
        // // var users = _userService.GetHireAblUsers(collectedUsers);
        // var users = _userService.filterUsers(collectedUsers, 1000);

        // _userService.ShowData(users);

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


        var semaphore = new SemaphoreSlim(5); // Concurrency limit to 5 --> meaning  5 operation run at  the same time .

        var tasksRepo = listOfUsers.Select(async user =>
        {
            await semaphore.WaitAsync();
            try
            {
                return await _gitRepoService.GetGitHubUsersRepos(user);
            }
            finally
            {
                semaphore.Release();
            }
        });

        var collectedUsersRepo = await _gitRepoService.ExecuteParallelTaskForRepos(tasksRepo);


  

        var repos = _gitRepoService.GetAllRepos(collectedUsersRepo);
        // var repos = _gitRepoService.ShowPublicRepos(collectedUsersRepo);
        // var repos = _gitRepoService.ShowReposHasOpenIssuesOverFive(collectedUsersRepo, 5);

        _gitRepoService.ShowData(repos);
        // foreach (var rep in repos)
        // {
        //     Console.WriteLine(rep.Name);
        // }


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