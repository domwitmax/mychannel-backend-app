using application.Data;
using Application.Data.Entities;
using Application.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        MyChannelDbContext _context;
        public SubscriptionRepository(MyChannelDbContext context)
        {
            _context = context;
        }
        public bool AddSubscription(Subscription subscription)
        {
            Subscription? subscriptionFromDb = _context.Subscriptions.SingleOrDefault(x => x.UserId == subscription.UserId && x.SubscriptionUserId == subscription.SubscriptionUserId);
            if (subscriptionFromDb != null)
                return false;
            _context.Subscriptions.Add(subscription);
            _context.SaveChanges();
            return true;
        }
        public bool RemoveSubscription(Subscription subscription)
        {
            Subscription? subscriptionFromDb = _context.Subscriptions.SingleOrDefault(x => x.UserId == subscription.UserId && x.SubscriptionUserId == subscription.SubscriptionUserId);
            if(subscriptionFromDb == null)
                return false;
            subscription.Id = subscriptionFromDb.Id;
            _context.Subscriptions.Remove(subscriptionFromDb);
            _context.SaveChanges();
            return true;
        }
        public int GetSubscriptionCount(int userId)
        {
            int count = _context.Subscriptions.Count(x => x.UserId == userId);
            return count;
        }

        public IEnumerable<Subscription> GetSubscriptions(int userId)
        {
            IEnumerable<Subscription> subscriptions = _context.Subscriptions.Where(x => x.UserId == userId);
            return subscriptions;
        }
    }
}
