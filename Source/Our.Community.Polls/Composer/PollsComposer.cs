using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Community.Polls.CacheRefresher;
using Umbraco.Community.Polls.Models.Repositories;

namespace Umbraco.Community.Polls.Composer
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
