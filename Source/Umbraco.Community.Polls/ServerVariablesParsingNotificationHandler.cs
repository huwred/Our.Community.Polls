using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Community.Polls.Controllers.ApiControllers;
using Umbraco.Extensions;

namespace Umbraco.Community.Polls
{
    internal class ServerVariablesParsingNotificationHandler : INotificationHandler<ServerVariablesParsingNotification>
    {
        private readonly LinkGenerator _linkGenerator;

        public ServerVariablesParsingNotificationHandler(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }


        public void Handle(ServerVariablesParsingNotification notification)
        {
            var urlDictionairy = new Dictionary<string, object>
            {
                { "getOverview", _linkGenerator.GetPathByAction(nameof(OverviewApiController.Get), ControllerExtensions.GetControllerName<OverviewApiController>()) },
                { "getQuestions", _linkGenerator.GetPathByAction(nameof(QuestionApiController.Get), ControllerExtensions.GetControllerName<QuestionApiController>()) },
                { "getQuestionById", _linkGenerator.GetPathByAction(nameof(QuestionApiController.GetById), ControllerExtensions.GetControllerName<QuestionApiController>()) },
                { "saveQuestion", _linkGenerator.GetPathByAction(nameof(QuestionApiController.Post), ControllerExtensions.GetControllerName<QuestionApiController>()) },
                { "deleteQuestion", _linkGenerator.GetPathByAction(nameof(QuestionApiController.Delete), ControllerExtensions.GetControllerName<QuestionApiController>()) },
                { "getQuestionAnswersById", _linkGenerator.GetPathByAction(nameof(QuestionApiController.GetAnswers), ControllerExtensions.GetControllerName<QuestionApiController>()) },
                { "postQuestionAnswer", _linkGenerator.GetPathByAction(nameof(QuestionApiController.PostAnswer), ControllerExtensions.GetControllerName<QuestionApiController>()) },
                { "getQuestionResponsesById", _linkGenerator.GetPathByAction(nameof(QuestionApiController.GetResponses), ControllerExtensions.GetControllerName<QuestionApiController>()) },

            };
            notification.ServerVariables.Add("polllinks", urlDictionairy);
        }
    }
}
