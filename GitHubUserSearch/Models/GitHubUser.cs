using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitHubUserSearch.Models
{
    public class GitHubUser
    {
        public string Login { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Avatar_Url { get; set; } = string.Empty;
        public string Repos_Url { get; set; } = string.Empty;
    }
}