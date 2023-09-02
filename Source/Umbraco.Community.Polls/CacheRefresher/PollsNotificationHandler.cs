using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Community.Polls.PollConstants;

namespace Umbraco.Community.Polls.CacheRefresher
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
            _runtimeCache.ClearByKey(CacheRefresherConstants.PollsCacheRefreshId);
        }
    }

}
