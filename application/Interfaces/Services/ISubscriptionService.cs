using Application.Models.Subscription;

namespace Application.Interfaces.Services
{
    public interface ISubscriptionService
    {
        IEnumerable<SubscriptionDto> GetSubscriptions(string userName);
        int GetSubscriptionCount(string userName);
        bool AddSubscription(SubscriptionDto subscriptionDto);
        bool RemoveSubscription(SubscriptionDto subscriptionDto);
    }
}
