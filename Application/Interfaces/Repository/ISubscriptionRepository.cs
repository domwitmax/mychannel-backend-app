using Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
