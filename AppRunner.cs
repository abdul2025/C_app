using consoleApp.ExternalClients;
using consoleApp.interfaces;
using System.Text.Json;

namespace consoleApp;

public class AppRunner
{
    private readonly IUserServices _userService;
    private readonly IGitRepoServices _gitRepoService;
    private readonly IOutputWriter _outputWriter;


    public AppRunner(IUserServices userService, IGitRepoServices gitRepoService, IOutputWriter outputWriter)
    {
        _userService = userService;
        _gitRepoService = gitRepoService;
        _outputWriter = outputWriter;
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

        var tasks = listOfUsers
            .Select(user => _userService.GetGitHubUsers(user));


        var collectedUsers = await _userService.ExecuteParallelTaskForUsers(tasks);

        // var users = _userService.ShowAllUser(collectedUsers);
        // var users = _userService.GetHireAblUsers(collectedUsers);
        var users = _userService.filterUsers(collectedUsers, 1000);

        _outputWriter.ShowData(users);


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


        // var tasksRepo = listOfUsers.Select(user =>  _gitRepoService.GetGitHubUsersRepos(user));

        // var collectedUsersRepo = await _gitRepoService.ExecuteParallelTaskForRepos(tasksRepo);


  

        // var repos = _gitRepoService.GetAllRepos(collectedUsersRepo);
        // var repos = _gitRepoService.ShowPublicRepos(collectedUsersRepo);
        // var repos = _gitRepoService.ShowReposHasOpenIssuesOverFive(collectedUsersRepo, 5);

        // _outputWriter.ShowData(repos);
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