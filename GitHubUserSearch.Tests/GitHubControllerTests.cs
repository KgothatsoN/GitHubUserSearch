using GitHubUserSearch.Controllers;
using GitHubUserSearch.Interfaces;
using GitHubUserSearch.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GitHubUserSearch.Tests
{
    [TestClass]
    public class GitHubControllerTests
    {
        [TestMethod]
        public async Task Search_ReturnsIndexWithError_WhenUsernameIsEmpty()
        {
            var mockService = new Mock<IGitHubService>();
            var controller = new GitHubController(mockService.Object);

            var inputModel = new GitHubViewModel
            {
                User = new GitHubUser { Name = "" }
            };

            var result = await controller.SearchAsync(inputModel) as ViewResult;
            var model = result.Model as GitHubViewModel;

            Assert.AreEqual("Index", result.ViewName);
            Assert.AreEqual("Please enter a GitHub username.", model.ErrorMessage);
        }

        [TestMethod]
        public async Task Search_ReturnsUserView_WhenValidUser()
        {
            var mockService = new Mock<IGitHubService>();
            mockService.Setup(s => s.GetUserAsync("octocat"))
                .ReturnsAsync(new GitHubUser { Login = "octocat", Name = "The Octo", Repos_Url = "..." });

            mockService.Setup(s => s.GetReposAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<GitHubRepo>
                {
            new GitHubRepo { Name = "TestRepo", Stargazers_Count = 5 }
                });

            var controller = new GitHubController(mockService.Object);

            var inputModel = new GitHubViewModel
            {
                User = new GitHubUser { Name = "octocat" }
            };

            var result = await controller.SearchAsync(inputModel) as ViewResult;
            var model = result.Model as GitHubViewModel;

            Assert.IsNotNull(model);
            Assert.AreEqual("octocat", model.User.Login);
        }
        [TestMethod]
        public async Task Search_ReturnsResultWithError_WhenUserNotFound()
        {
            var mockService = new Mock<IGitHubService>();
            mockService.Setup(s => s.GetUserAsync("nonexistent")).ReturnsAsync((GitHubUser)null);

            var controller = new GitHubController(mockService.Object);
            var inputModel = new GitHubViewModel
            {
                User = new GitHubUser { Name = "nonexistent" }
            };

            var result = await controller.SearchAsync(inputModel) as ViewResult;
            var model = result.Model as GitHubViewModel;

            Assert.AreEqual("Result", result.ViewName);
            Assert.AreEqual("User not found.", model.ErrorMessage);
        }
        [TestMethod]
        public async Task Search_ReturnsResultWithMessage_WhenNoReposFound()
        {
            var mockService = new Mock<IGitHubService>();
            mockService.Setup(s => s.GetUserAsync("norepos"))
                .ReturnsAsync(new GitHubUser { Login = "norepos", Name = "No Repos", Repos_Url = "..." });

            mockService.Setup(s => s.GetReposAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<GitHubRepo>());

            var controller = new GitHubController(mockService.Object);
            var inputModel = new GitHubViewModel
            {
                User = new GitHubUser { Name = "norepos" }
            };

            var result = await controller.SearchAsync(inputModel) as ViewResult;
            var model = result.Model as GitHubViewModel;

            Assert.AreEqual("Result", result.ViewName);
            Assert.AreEqual("No repositories found.", model.ErrorMessage);
        }
        [TestMethod]
        public async Task Search_ReturnsIndexWithError_WhenServiceThrowsException()
        {
            var mockService = new Mock<IGitHubService>();
            mockService.Setup(s => s.GetUserAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception("GitHub API error"));

            var controller = new GitHubController(mockService.Object);
            var inputModel = new GitHubViewModel
            {
                User = new GitHubUser { Name = "erroruser" }
            };

            var result = await controller.SearchAsync(inputModel) as ViewResult;
            var model = result.Model as GitHubViewModel;

            Assert.AreEqual("Index", result.ViewName);
            Assert.AreEqual("GitHub API error", model.ErrorMessage);
        }

    }
}