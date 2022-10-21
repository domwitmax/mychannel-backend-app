using Application.Data.Entities;

namespace Application.Interfaces.Repository
{
    public interface ISubscriptionRepository
    {
        bool AddSubscription(Subscription subscription);
        bool RemoveSubscription(Subscription subscription);
        int GetSubscriptionCount(string userName);
        IEnumerable<Subscription> GetSubscriptions(string userName);
    }
}
