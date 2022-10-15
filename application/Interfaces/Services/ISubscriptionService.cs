using Application.Models;
using Application.Models.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ISubscriptionService
    {
        IEnumerable<SubscriptionDto> GetSubscriptions(int userId);
        int GetSubscriptionCount(int userId);
        bool AddSubscription(SubscriptionDto subscriptionDto);
        bool RemoveSubscription(SubscriptionDto subscriptionDto);
    }
}
