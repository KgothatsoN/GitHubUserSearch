using GitHubUserSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubUserSearch.Interfaces
{
    public interface IGitHubService
    {
        Task<GitHubUser> GetUserAsync(string username);
        Task<List<GitHubRepo>> GetReposAsync(string reposUrl);
    }
}
