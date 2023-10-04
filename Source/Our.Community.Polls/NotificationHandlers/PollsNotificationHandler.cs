using Our.Community.Polls.PollConstants;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Our.Community.Polls.NotificationHandlers
{
    public class PollsNotificationHandler : INotificationHandler<ContentCacheRefresherNotification>
    {
        private readonly IAppPolicyCache _runtimeCache;
        public PollsNotificationHandler(AppCaches appCaches)
        {
            _runtimeCache = appCaches.RuntimeCache;
        }

        public void Handle(ContentCacheRefresherNotification notification)
        {
            _runtimeCache.ClearByKey(RuntimeCacheConstants.RuntimeCacheKeyPrefix);
        }
    }

}
