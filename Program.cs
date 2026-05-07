using consoleApp;
using consoleApp.ExternalClients;
using consoleApp.interfaces;
using consoleApp.Repositories;
using consoleApp.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHttpClient<GitHubApiClient>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserServices, UserService>();

        // ✅ register your runner
        services.AddScoped<AppRunner>();
    })
    .Build();

// ✅ create scope and run app
using var scope = host.Services.CreateScope();

var app = scope.ServiceProvider.GetRequiredService<AppRunner>();

await app.RunAsync();