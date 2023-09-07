using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace Our.Community.Polls.Controllers
{
    public class PollSurfaceController : SurfaceController
    {

        private readonly IPollService _pollService;
        public PollSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider,
            IPollService pollService) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _pollService = pollService;
        }        
        
        public IActionResult Get(int id)
        {
            var poll = _pollService.GetQuestion(id);
            return this.View(poll);
        }
        [HttpPost]
        public IActionResult Poll(IFormCollection form)
        {
            var questionId = form["questionId"];
            var answerId = form["answerId"];

            var poll = _pollService.Vote(Convert.ToInt32(questionId), Convert.ToInt32(answerId));
            TempData["Question"] = poll;
            return CurrentUmbracoPage();
        }

    }
}
