using consoleApp;
using consoleApp.ExternalClients;
using consoleApp.Infrastructure;
using consoleApp.interfaces;
using consoleApp.Repositories;
using consoleApp.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHttpClient<GitHubApiClient>();

        // IAppLogger Service
        services.AddSingleton<IAppLogger, ConsoleLogger>();
        services.AddSingleton<IOutputWriter, ConsoleWriter>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserServices, UserService>();
        services.AddScoped<IGitRepoServices, GitRepoServices>();




        // ✅ register your runner
        services.AddScoped<AppRunner>();
    })
    .Build();

// ✅ create scope and run app
using var scope = host.Services.CreateScope();

var app = scope.ServiceProvider.GetRequiredService<AppRunner>();

await app.RunAsync();