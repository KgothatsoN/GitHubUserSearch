using GitHubUserSearch.Interfaces;
using GitHubUserSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GitHubUserSearch.Controllers
{
    public class GitHubController : Controller
    {
        private readonly IGitHubService _gitHubService;
        public GitHubController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }
        public ActionResult Index()
        {
            return View(new GitHubViewModel());
        }
        [HttpPost]
        public async Task<ActionResult> SearchAsync(GitHubViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.User.Name))
            {
                model.ErrorMessage = "Please enter a GitHub username.";
                return View("Index", model);
            }

            try
            {
                var user = await _gitHubService.GetUserAsync(model.User.Name);
                if (user == null)
                {
                    return View("Result", new GitHubViewModel { ErrorMessage = "User not found." });
                }

                var repos = await _gitHubService.GetReposAsync(user.Repos_Url);
                return View("Result", new GitHubViewModel
                {
                    User = user,
                    TopRepositories = repos,
                    ErrorMessage = repos.Any() ? null : "No repositories found."
                });
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                return View("Index", model);
            }
            
        }
    }
}