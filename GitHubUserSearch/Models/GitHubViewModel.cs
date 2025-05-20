using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitHubUserSearch.Models
{
    public class GitHubViewModel
    {
        public GitHubUser User { get; set; }
        public List<GitHubRepo> TopRepositories { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}