using Microsoft.Extensions.DependencyInjection;
using Our.Community.Polls.Interfaces;
using Our.Community.Polls.NotificationHandlers;
using Our.Community.Polls.Repositories;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace Our.Community.Polls.Composer
{
    public class PollsComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {

            builder.Services.AddSingleton<IQuestions, QuestionRepository>();
            builder.Services.AddSingleton<IAnswers, AnswerRepository>();
            builder.Services.AddSingleton<IResponses, ResponseRepository>();
            builder.Services.AddSingleton<IPollService, PollService>();
            builder.AddNotificationHandler<ServerVariablesParsingNotification, ServerVariablesParsingNotificationHandler>();
            builder.AddNotificationHandler<ContentCacheRefresherNotification, PollsNotificationHandler>();
        }
    }
}
