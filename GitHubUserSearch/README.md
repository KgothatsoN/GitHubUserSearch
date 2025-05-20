GitHub User Search - README

## Overview
This ASP.NET MVC (.NET Framework) application allows you to search for GitHub users by
username.
It retrieves and displays their profile information and top 5 repositories based on star count.

## Features
- Search GitHub users using the GitHub REST API
- Display user name, location, avatar
- List top 5 repositories sorted by stargazer count
- Bootstrap styling with responsive layout
- Unit tests with Moq
-Basic error handling for API responses

## Setup Instructions
1. Open the solution in Visual Studio.
2. Make sure your main project targets .NET Framework 4.7.2 or 4.8.
3. Restore NuGet packages if prompted.
4. Set `Index` in `GitHubController` as the start page (Right-click > Set as Start Page).
5. Run the application (Ctrl + F5).


## Running Unit Tests
1. Ensure you have a test project named `GitHubUserSearch.Tests`.
2. Open Test Explorer (Test > Test Explorer).
3. Click 'Run All' to execute tests.
4. Make sure both main and test projects use the same version of `System.Web.Mvc`.


## Notes
- No third-party API libraries are used.
- GitHub API is accessed using `HttpClient`.
- Make sure you have an internet connection to access GitHub's API.
- Basic error handling is included for invalid usernames, no repos, and API errors.