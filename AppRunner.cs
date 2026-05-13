using consoleApp.Interfaces;
using consoleApp.Models;
using Microsoft.Extensions.Options;


namespace consoleApp;

public class AppRunner
{
    private readonly IUserServices _userService;
    private readonly IGitRepoServices _gitRepoService;
    private readonly IOutputWriter _outputWriter;
    private readonly GitHubOptions _options;


    public AppRunner(IUserServices userService, IGitRepoServices gitRepoService, IOutputWriter outputWriter, IOptions<GitHubOptions> options)
    {
        _userService = userService;
        _gitRepoService = gitRepoService;
        _outputWriter = outputWriter;
        _options = options.Value;
    }

    public async Task RunAsync(CancellationToken token)
    {
        if (_options.Usernames?.Any() != true)
        {
            return;
        }
        var listOfUsers = _options.Usernames;


        /*
            START Github User Service.
            START Github User Service.
            START Github User Service.
        */


        var tasks = listOfUsers
            .Select(user => _userService.GetGitHubUsers(user, token));


        var collectedUsers = await _userService.AsyncTaskServiceForUsers(tasks);

        // var users = _userService.ShowAllUser(collectedUsers);
        // var users = _userService.GetHireAblUsers(collectedUsers);
        var users = _userService.FilterUsersByNumOfFollowers(collectedUsers, 1000);

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

        // var collectedUsersRepo = await _gitRepoService.AsyncTaskServiceForRepos(tasksRepo);


  

        // var repos = _gitRepoService.GetAllRepos(collectedUsersRepo);
        // var repos = _gitRepoService.ShowPublicRepos(collectedUsersRepo);
        // var repos = _gitRepoService.ShowReposHasOpenIssuesOverFive(collectedUsersRepo, 5);

        // _outputWriter.ShowData(repos);
 


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
            START Query & Aggregation Operation.
            START Query & Aggregation Operation.
            START Query & Aggregation Operation.
        */


        /*
            END Query & Aggregation Operation.
            END Query & Aggregation Operation.
            END Query & Aggregation Operation.
        */

        


        
    }
}