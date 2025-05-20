using GitHubUserSearch.Controllers;
using GitHubUserSearch.Interfaces;
using GitHubUserSearch.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GitHubUserSearch.Factories
{
    public class CustomControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == typeof(GitHubController))
            {
                IGitHubService service = new GitHubService();
                return new GitHubController(service);
            }

            return base.GetControllerInstance(requestContext, controllerType);
        }
    }

}