using consoleApp.Interfaces;
using consoleApp.Models;
using Microsoft.Extensions.Options;


namespace consoleApp;

public class AppRunner
{
    private readonly IGitHubUserService _gitUserService;
    private readonly IGitRepoServices _gitRepoService;
    private readonly IOutputWriter _outputWriter;
    private readonly GitHubOptions _options;


    public AppRunner(IGitHubUserService gitUserService, IGitRepoServices gitRepoService, IOutputWriter outputWriter, IOptions<GitHubOptions> options)
    {
        _gitUserService = gitUserService;
        _gitRepoService = gitRepoService;
        _outputWriter = outputWriter;
        _options = options.Value;
    }

    public async Task RunAsync(CancellationToken token)
    {
        
        /*
            START Github User Service.
            START Github User Service.
        */

        await _gitUserService.ProcessGitHubUsers(_options.Usernames, token);

        /*
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
            START Github User's Repos Service.
            START Github User's Repos Service.
            START Github User's Repos Service.
        */


        // await _gitRepoService.ProcessGitHubUsersRepos(_options.Usernames, token);

 


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