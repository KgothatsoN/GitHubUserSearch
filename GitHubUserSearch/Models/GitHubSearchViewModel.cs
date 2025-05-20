using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitHubUserSearch.Models
{
    public class GitHubSearchViewModel
    {
        public string Username { get; set; }

        public GitHubUser User { get; set; }

        public string ErrorMessage { get; set; }
    }

}