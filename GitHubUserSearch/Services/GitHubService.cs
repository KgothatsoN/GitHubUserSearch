using GitHubUserSearch.Interfaces;
using GitHubUserSearch.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace GitHubUserSearch.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly HttpClient _client;

        public GitHubService()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
        }

        public async Task<GitHubUser> GetUserAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username must not be empty.");

            try
            {
                var response = await _client.GetAsync($"https://api.github.com/users/{username}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("GitHub user not found.");
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GitHubUser>(json);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching data from GitHub.", ex);
            }
        }

        public async Task<List<GitHubRepo>> GetReposAsync(string reposUrl)
        {
            try
            {
                var response = await _client.GetAsync(reposUrl);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("GitHub repo not found.");
                }
                var json = await response.Content.ReadAsStringAsync();
                var repos = JsonConvert.DeserializeObject<List<GitHubRepo>>(json);
                return repos.OrderByDescending(r => r.Stargazers_Count).Take(5).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("Error fetching data from GitHub.", ex);
            }
        }
    }
}