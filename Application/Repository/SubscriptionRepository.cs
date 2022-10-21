using Application.Data;
using Application.Data.Entities;
using Application.Interfaces.Repository;

namespace Application.Repository
{
    public class SubscriptionRepository: ISubscriptionRepository
    {
        private readonly MyChannelDbContext _context;
        public SubscriptionRepository(MyChannelDbContext context)
        {
            _context = context;
        }
        public bool AddSubscription(Subscription subscription)
        {
            Subscription? subscriptionFromDb = _context.Subscriptions.SingleOrDefault(x => x.UserName == subscription.UserName && x.SubscriptionUserName == subscription.SubscriptionUserName);
            if (subscriptionFromDb != null)
                return false;
            _context.Subscriptions.Add(subscription);
            _context.SaveChanges();
            return true;
        }
        public bool RemoveSubscription(Subscription subscription)
        {
            Subscription? subscriptionFromDb = _context.Subscriptions.SingleOrDefault(x => x.UserName == subscription.UserName && x.SubscriptionUserName == subscription.SubscriptionUserName);
            if(subscriptionFromDb == null)
                return false;
            subscription.Id = subscriptionFromDb.Id;
            _context.Subscriptions.Remove(subscriptionFromDb);
            _context.SaveChanges();
            return true;
        }
        public int GetSubscriptionCount(string userName)
        {
            int count = _context.Subscriptions.Count(x => x.SubscriptionUserName == userName);
            return count;
        }

        public IEnumerable<Subscription> GetSubscriptions(string userName)
        {
            IEnumerable<Subscription> subscriptions = _context.Subscriptions.Where(x => x.UserName == userName);
            return subscriptions;
        }
    }
}
