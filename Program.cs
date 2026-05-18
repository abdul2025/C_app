using consoleApp;
using consoleApp.ExternalClients;
using consoleApp.Infrastructure;
using consoleApp.Interfaces;
using consoleApp.Models;
using consoleApp.Repositories;
using consoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders(); // Strop the dotnet C# defualet log come up in the terminal.
    })
    .ConfigureServices((context, services) =>
    {

        


        // ## Config from AppSettings.json
        var config = context.Configuration;
        services.AddOptions<GitHubOptions>() 
            .Bind(config.GetSection("GitHub"))
            .Validate(o => o.Usernames?.Any() == true, "Usernames required") // Ensure the collected data is available in the appsettings.json
            .ValidateOnStart();

        services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<GitHubOptions>>().Value;
            return new SemaphoreSlim(options.MaxConcurrentRequests); 
            // Setting up Max Concurrency for SemaphoreSlim with the use of AddSingleton to keep the whole app using one
            // shared gate to the External GitHub APIs which controlled by the MAX Concurrency
            // in Case of muilt APIs we should use AddSingleton but with helper function for further distinguish and avoid multi APIs Concurrency blocking each other
        });



        // Retrial and Exponential backoff
        services.AddSingleton(sp =>
        {
            var logger = sp.GetRequiredService<IAppLogger>();
            return RetryPolicyFactory.CreateHttpRetryPolicy(logger, 3);
        });
        


        services.AddHttpClient<GitHubApiClient>();

        // IAppLogger Service
        services.AddSingleton<IAppLogger, ConsoleLogger>();
        // Write Json Service
        services.AddSingleton<IOutputWriter, ConsoleWriter>();

        // App Services
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGitHubUserService, GitHubUserService>();
        services.AddScoped<IGitRepoServices, GitRepoServices>();


        // Main Runner Service
        services.AddScoped<AppRunner>();
    }).Build();

// create scope and run app
using var scope = host.Services.CreateScope();

var app = scope.ServiceProvider.GetRequiredService<AppRunner>();


var cts = new CancellationTokenSource();
// Setting up a notice for Ctrl+C from terminal 
Console.CancelKeyPress += (s, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

await app.RunAsync(cts.Token);