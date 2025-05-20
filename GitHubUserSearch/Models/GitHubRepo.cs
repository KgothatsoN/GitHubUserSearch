using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitHubUserSearch.Models
{
    public class GitHubRepo
    {
        public string Name { get; set; }
        public string Html_Url { get; set; }
        public string Description { get; set; }
        public int Stargazers_Count { get; set; }
    }
}