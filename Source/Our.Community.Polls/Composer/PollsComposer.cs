using Microsoft.Extensions.DependencyInjection;
using Our.Community.Polls.CacheRefresher;
using Our.Community.Polls.Models.Repositories;
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
