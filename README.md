# ConsoleApp - GitHub User and Repository Fetcher

A .NET console application that fetches GitHub user profiles and their repositories using the GitHub API. It demonstrates dependency injection, asynchronous programming, and data processing in C#.

## Project Overview

This console application interacts with the GitHub API to retrieve user information and repository data for specified usernames. It processes the data in parallel, applies filters, and displays the results in JSON format.

### Key Features

- **Asynchronous API Calls**: Fetches user and repository data concurrently for multiple users
- **Dependency Injection**: Uses Microsoft.Extensions for service registration and resolution
- **Data Processing**: Filters users by hireability status and follower count
- **Repository Filtering**: Filters repositories by visibility and open issues count
- **JSON Serialization**: Displays data in formatted JSON output

## Architecture

The application follows a layered architecture with separation of concerns:

- **Program.cs**: Entry point with dependency injection setup
- **AppRunner.cs**: Main application logic orchestrator
- **Services/**: Business logic layer (UserServices, GitRepoServices)
- **Repositories/**: Data access layer (UserRepository)
- **ExternalClients/**: API client for GitHub integration (GitHubApiClient)
- **DTO/**: Data transfer objects for API responses
- **interfaces/**: Contracts for services and repositories

## Setup Instructions

### Prerequisites

- .NET 10.0 SDK (download from [Microsoft .NET](https://dotnet.microsoft.com/download))
- A GitHub Personal Access Token (for API authentication)

### Installation

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd consoleApp
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build
   ```

## Environment Configuration

### GitHub Token Setup

The application requires a GitHub Personal Access Token to access the GitHub API. You can obtain one from [GitHub Settings > Developer settings > Personal access tokens](https://github.com/settings/tokens).

Set the token as an environment variable:

**On macOS/Linux:**
```bash
export GITHUB_TOKEN="your_github_token_here"
```

**On Windows:**
```cmd
set GITHUB_TOKEN=your_github_token_here
```

Alternatively, use the provided `run.sh` script which sets the token automatically.

## Running the Application

### Using the Shell Script

Make the script executable and run it:
```bash
chmod +x run.sh
./run.sh
```

### Manual Execution

1. Set the GitHub token environment variable
2. Run the application:
   ```bash
   dotnet run
   ```

## Functions and Features

### User Services (`IUserServices`)

- `GetGitHubUsers(string username)`: Fetches a single GitHub user's profile
- `ExecuteParallelTaskForUsers(IEnumerable<Task<GitHubUserDto?>> tasks)`: Executes multiple user fetch tasks in parallel
- `ShowAllUser(IEnumerable<GitHubUserDto> users)`: Returns all users (no filtering)
- `GetHireAblUsers(IEnumerable<GitHubUserDto> users)`: Filters users who are hireable
- `FilterUsers(IEnumerable<GitHubUserDto> users, int numberOfFollowers)`: Filters users by minimum follower count
- `ShowData(IEnumerable<GitHubUserDto> data)`: Displays user data in JSON format

### Repository Services (`IGitRepoServices`)

- `GetGitHubUsersRepos(string username)`: Fetches all repositories for a user
- `ShowAllRepos(IEnumerable<GitHubUserRepoDto> repos)`: Returns all repositories (no filtering)
- `ShowPublicRepos(IEnumerable<GitHubUserRepoDto> repos)`: Filters repositories by public visibility
- `ShowReposHasOpenIssuesOverFive(IEnumerable<GitHubUserRepoDto> repos, int numberOfIssues)`: Filters repositories by minimum open issues count

### Data Models

#### GitHubUserDto
Represents a GitHub user profile with properties like:
- Login, Name, Bio
- Follower/Following counts
- Hireable status
- Company, Location, Blog
- Repository and gist counts

#### GitHubUserRepoDto
Represents a GitHub repository with properties like:
- Name, Description, Language
- Star, Watcher, Fork counts
- Open issues count
- Visibility (public/private)
- Creation/Update timestamps
- Owner information

## Dependencies

The project uses the following NuGet packages:

- `Microsoft.Extensions.DependencyInjection` (v10.0.7): For dependency injection
- `Microsoft.Extensions.Hosting` (v10.0.7): For generic host setup
- `Microsoft.Extensions.Http` (v10.0.7): For HTTP client factory

## Configuration

The application is configured in `Program.cs` using the Generic Host builder:

- Registers `HttpClient` for the GitHub API client
- Registers scoped services for repositories and services
- Registers the main application runner

## Sample Output

The application processes a predefined list of GitHub usernames and displays:

1. User profile information in JSON format
2. Repository information for each user in JSON format
3. Summary counts of users and repositories returned

## Error Handling

The application includes error handling for API calls:
- Catches exceptions during API requests
- Logs error messages to the console
- Continues processing other users/repos if one fails

## Development Notes

- Built with .NET 10.0 and C# 12 features
- Uses `System.Text.Json` for serialization
- Implements async/await pattern throughout
- Follows SOLID principles with interface segregation